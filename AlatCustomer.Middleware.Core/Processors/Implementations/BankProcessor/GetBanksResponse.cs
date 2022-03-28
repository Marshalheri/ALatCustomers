using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlatCustomer.Middleware.Core.Processors.Implementations.BankProcessor
{
    public class GetBanksResponse : BaseResponse
    {
        public IEnumerable<Bank> Result { get; set; }
    }
}
