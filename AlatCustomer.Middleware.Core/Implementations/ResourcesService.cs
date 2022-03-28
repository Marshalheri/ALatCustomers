using AlatCustomer.Middleware.Core.DTOs;
using AlatCustomer.Middleware.Core.DTOs.Resources;
using AlatCustomer.Middleware.Core.Processors;
using AlatCustomer.Middleware.Core.Services;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace AlatCustomer.Middleware.Core.Implementations
{
    public class ResourcesService : IResourcesService
    {

        private readonly SystemSettings _settings;
        private readonly IMessageProvider _messageProvider;
        private readonly IOtpProcessor _otpProcessor;
        private readonly IBankProcessor _bankProcessor;
        public ResourcesService(IOptions<SystemSettings> settings, IMessageProvider messageProvider, IOtpProcessor otpProcessor, IBankProcessor bankProcessor)
        {
            _settings = settings.Value;
            _messageProvider = messageProvider;
            _otpProcessor = otpProcessor;
            _bankProcessor = bankProcessor;
        }

        public async Task<PayloadResponse<GetAllBanksResponseDTO>> GetBanksAsync()
        {
            PayloadResponse<GetAllBanksResponseDTO> response = new(false);
            var serviceResponse = await _bankProcessor.GetAllBanksAsync();
            if (!serviceResponse.IsSuccessful)
            {
                return ErrorResponse.Create<PayloadResponse<GetAllBanksResponseDTO>>(
                      FaultMode.CLIENT_INVALID_ARGUMENT, serviceResponse.Error.ErrorCode,
                      serviceResponse.Error.Description);
            }
            var banks = serviceResponse.GetPayload();
            if (!banks.Any())
            {
                return ErrorResponse.Create<PayloadResponse<GetAllBanksResponseDTO>>(
                    FaultMode.REQUESTED_ENTITY_NOT_FOUND,
                    ResponseCodes.NO_BANK,
                    _messageProvider.GetMessage(ResponseCodes.NO_BANK));
            }

            response.SetPayload(new GetAllBanksResponseDTO
            {
                Banks = banks.Select(x => new Processors.Implementations.BankProcessor.Bank
                {
                    Code = x.Code,
                    Name = x.Name
                })
            });
            response.IsSuccessful = true;
            return response;
        }

        public async Task<PayloadResponse<GetLocalGovernmentsResponse>> GetLocalGovernmentsAsync(string stateCode)
        {
            var response = new PayloadResponse<GetLocalGovernmentsResponse>(false);
            var localGovernments = _settings.LocalGovernments.Where(x => x.StateCode == stateCode);
            if (!localGovernments.Any())
            {
                return ErrorResponse.Create<PayloadResponse<GetLocalGovernmentsResponse>>(
                    FaultMode.REQUESTED_ENTITY_NOT_FOUND,
                    ResponseCodes.NO_LGA_FOUND,
                    _messageProvider.GetMessage(ResponseCodes.NO_LGA_FOUND));
            }

            response.SetPayload(new GetLocalGovernmentsResponse
            {
                LocalGovernments = localGovernments.Select(x => new LocalGovernment
                {
                    StateCode = x.StateCode,
                    Code = x.Code,
                    Name = x.Name
                })
            });
            response.IsSuccessful = true;
            return response;
        }

        public async Task<PayloadResponse<GetStatesResponse>> GetStatesAsync()
        {
            var response = new PayloadResponse<GetStatesResponse>(false);
            var states = _settings.States;
            if (!states.Any())
            {
                return ErrorResponse.Create<PayloadResponse<GetStatesResponse>>(
                    FaultMode.REQUESTED_ENTITY_NOT_FOUND,
                    ResponseCodes.NO_STATE_FOUND,
                    _messageProvider.GetMessage(ResponseCodes.NO_STATE_FOUND));
            }

            response.SetPayload(new GetStatesResponse
            {
                States = states.Select(x => new State
                {
                    Code = x.Code,
                    Name = x.Name
                })
            });
            response.IsSuccessful = true;
            return response;
        }

        public async Task<BasicResponse> SendOtpAsync(SendOtpRequestDTO request)
        {
            var response = new BasicResponse(false);
            var serviceResponse = await _otpProcessor.SendOtpAsync(request.PhoneNumber, request.OtpPurpose, request.EmailAddress);
            if (!serviceResponse.IsSuccessful)
            {
                return ErrorResponse.Create<BasicResponse>(FaultMode.CLIENT_INVALID_ARGUMENT, 
                        serviceResponse.Error.ErrorCode, serviceResponse.Error.Description);
            }

            response.IsSuccessful = true;
            return response;
        }
    }
}
