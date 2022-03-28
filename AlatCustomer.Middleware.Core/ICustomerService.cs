using AlatCustomer.Middleware.Core.DTOs;
using AlatCustomer.Middleware.Core.DTOs.Customer;
using System.Threading.Tasks;

namespace AlatCustomer.Middleware.Core
{
    public interface ICustomerService
    {
        Task<BasicResponse> OnboardCustomerAsync(OnboardCustomerRequestDTO request);
    }
}
