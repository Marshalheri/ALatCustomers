using AlatCustomer.Middleware.Core.DTOs;
using AlatCustomer.Middleware.Core.DTOs.Customer;
using AlatCustomer.Middleware.Core.Models;
using AlatCustomer.Middleware.Core.Processors;
using AlatCustomer.Middleware.Core.Repository;
using AlatCustomer.Middleware.Core.Services;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlatCustomer.Middleware.Core.Implementations
{
    public class CustomerService : ICustomerService
    {

        private readonly SystemSettings _settings;
        private readonly IMessageProvider _messageProvider;
        private readonly IOtpProcessor _otpProcessor;
        private readonly IUnitOfWork _unitOfWork;
        public CustomerService(IOptions<SystemSettings> settings, IMessageProvider messageProvider, IOtpProcessor otpProcessor, IUnitOfWork unitOfWork)
        {
            _settings = settings.Value;
            _messageProvider = messageProvider;
            _otpProcessor = otpProcessor;
            _unitOfWork = unitOfWork;
        }
        public async Task<BasicResponse> OnboardCustomerAsync(OnboardCustomerRequestDTO request)
        {
            BasicResponse response = new(false);

            var state = _settings.States.FirstOrDefault(x => x.Code == request.StateOfResidenceCode);
            if (state == null)
            {
                return ErrorResponse.Create<BasicResponse>(
                    FaultMode.REQUESTED_ENTITY_NOT_FOUND,
                    ResponseCodes.NO_STATE_FOUND,
                    _messageProvider.GetMessage(ResponseCodes.NO_STATE_FOUND));
            }
            var lga = _settings.LocalGovernments.FirstOrDefault(x => x.Code == request.LgaOfResidenceCode && x.StateCode == state.Code);
            if (lga == null)
            {
                return ErrorResponse.Create<BasicResponse>(
                    FaultMode.REQUESTED_ENTITY_NOT_FOUND,
                    ResponseCodes.NO_LGA_FOUND,
                    _messageProvider.GetMessage(ResponseCodes.NO_LGA_FOUND));
            }
            var validateOtpServiceResponse = await _otpProcessor.ValidateOtpAsync(request.PhoneNumber, OtpPurpose.Onboarding, request.Otp);
            if (!validateOtpServiceResponse.IsSuccessful)
            {
                return validateOtpServiceResponse;
            }

            byte[] passwordByte = Encoding.ASCII.GetBytes(request.NewPassword.Password);
            var newCustomer = new Customer
            {
                Name = request.Name,
                EmailAddress = request.EmailAddress,
                PhoneNumber = request.PhoneNumber,
                StateOfResidence = state.Code,
                LocalGovernmentOfResidence = lga.Code,
                DateCreated = DateTime.Now,
                Password = passwordByte
            };

            await _unitOfWork.CustomerRepository.AddAsync(newCustomer);
            await _unitOfWork.SaveAsync();
            response.IsSuccessful = true;
            return response;
        }

        public async Task<PayloadResponse<GetAllCustomersDetailsResponseDTO>> GetAllOnboardedCustomerAsync()
        {
            PayloadResponse<GetAllCustomersDetailsResponseDTO> response = new(false);
            var customers = await _unitOfWork.CustomerRepository.GetAllAsync();
            if (!customers.Any())
            {
                return ErrorResponse.Create<PayloadResponse<GetAllCustomersDetailsResponseDTO>>(
                    FaultMode.REQUESTED_ENTITY_NOT_FOUND,
                    ResponseCodes.NO_CUSTOMER,
                    _messageProvider.GetMessage(ResponseCodes.NO_CUSTOMER));
            }
            response.SetPayload(new GetAllCustomersDetailsResponseDTO
            {
                Customers = customers.Select(x => new CustomersDetails
                {
                    Name = x.Name,
                    PhoneNumber = x.PhoneNumber,
                    EmailAddress = x.EmailAddress,
                    StateOfResidence = x.StateOfResidence,
                    LocalGovernmentOfResidence = x.LocalGovernmentOfResidence,
                    DateCreated = x.DateCreated.ToString("D")
                })
            });
            response.IsSuccessful = true;
            return response;
        }
    }
}
