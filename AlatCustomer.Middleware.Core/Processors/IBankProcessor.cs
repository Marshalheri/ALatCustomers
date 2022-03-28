using AlatCustomer.Middleware.Core.DTOs;
using AlatCustomer.Middleware.Core.Processors.Implementations.BankProcessor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlatCustomer.Middleware.Core.Processors
{
    public interface IBankProcessor
    {
        Task<PayloadResponse<IEnumerable<Bank>>> GetAllBanksAsync();
    }
}
