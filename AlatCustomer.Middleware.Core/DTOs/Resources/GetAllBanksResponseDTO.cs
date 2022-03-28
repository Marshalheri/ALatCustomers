using AlatCustomer.Middleware.Core.Processors.Implementations.BankProcessor;
using System.Collections.Generic;

namespace AlatCustomer.Middleware.Core.DTOs.Resources
{
    public class GetAllBanksResponseDTO
    {
        public IEnumerable<Bank> Banks { get; set; }
    }
}
