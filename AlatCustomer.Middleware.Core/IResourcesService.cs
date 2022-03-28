using AlatCustomer.Middleware.Core.DTOs;
using AlatCustomer.Middleware.Core.DTOs.Resources;
using System.Threading.Tasks;

namespace AlatCustomer.Middleware.Core
{
    public interface IResourcesService
    {
        Task<PayloadResponse<GetStatesResponse>> GetStatesAsync();
        Task<PayloadResponse<GetLocalGovernmentsResponse>> GetLocalGovernmentsAsync(string stateCode);
        Task<BasicResponse> SendOtpAsync(SendOtpRequestDTO request);
        Task<PayloadResponse<GetAllBanksResponseDTO>> GetBanksAsync();
    }
}
