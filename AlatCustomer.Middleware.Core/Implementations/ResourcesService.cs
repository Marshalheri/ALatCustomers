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
        public ResourcesService(IOptions<SystemSettings> settings, IMessageProvider messageProvider, IOtpProcessor otpProcessor)
        {
            _settings = settings.Value;
            _messageProvider = messageProvider;
            _otpProcessor = otpProcessor;
        }
        public async Task<PayloadResponse<GetLocalGovernmentsResponse>> GetLocalGovernmentsAsync(string stateCode)
        {
            var response = new PayloadResponse<GetLocalGovernmentsResponse>(false);
            var localGovernments = _settings.LocalGovernments.Where(x => x.StateCode == stateCode);
            if (!localGovernments.Any())
            {
                return ErrorResponse.Create<PayloadResponse<GetLocalGovernmentsResponse>>(
                    FaultMode.CLIENT_INVALID_ARGUMENT,
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
                    FaultMode.CLIENT_INVALID_ARGUMENT,
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
