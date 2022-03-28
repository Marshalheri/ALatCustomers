using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlatCustomer.Middleware.Core.Processors.Implementations.BankProcessor
{
    public class BankProcessorSettings
    {
        public string BaseUrl { get; set; }
        public string GetBanksPath { get; set; }
        public string Subscriptionkey { get; set; }
    }
}
