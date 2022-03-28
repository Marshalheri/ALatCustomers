using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlatCustomer.Middleware.Core.Processors.Implementations.BankProcessor
{
    public class BaseResponse
    {
        public string[] ErrorMessages { get; set; }
        public string ErrorMessage { get; set; }
        public bool HasError { get; set; }
        public string TimeGenerated { get; set; }

        public bool IsSuccessful()
        {
            return HasError;
        }
    }
}
