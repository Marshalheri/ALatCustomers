using AlatCustomer.Middleware.Core.DTOs;
using AlatCustomer.Middleware.Core.DTOs.Resources;
using AlatCustomer.Middleware.Core.Processors;
using AlatCustomer.Middleware.Core.Processors.Implementations.BankProcessor;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlatCustomer.Middleware.Core.Fakes
{
    public class FakeBankProcessor : IBankProcessor
    {
        public Task<PayloadResponse<IEnumerable<Bank>>> GetAllBanksAsync()
        {
            PayloadResponse<IEnumerable<Bank>> response = new(true);
            response.SetPayload(Banks()?.Select(x => new Bank
            {
                Code = x.Code,
                Name = x.Name
            }));

            return Task.FromResult(response);
        }

        private IEnumerable<Bank> Banks()
        {
            List<Bank> banks = new()
            {
                new Bank
                {
                    Name = "Access Bank Plc",
                    Code = "044"
                },
                new Bank
                {
                    Name = "Aso Savings and Loans Plc",
                    Code = "401"
                },
                new Bank
                {
                    Name = "Citi bank",
                    Code = "023"
                },
                new Bank
                {
                    Name = "Wema Bank Plc",
                    Code = "035"
                },
                new Bank
                {
                    Name = "Fidelity Bank Plc",
                    Code = "070"
                },
                new Bank
                {
                    Name = "Guaranty Trust Bank Plc",
                    Code = "058"
                },
                new Bank
                {
                    Name = "StanbicIBTC Bank Plc",
                    Code = "221"
                },
            };
            return banks;
        }
    }
}
