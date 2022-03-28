using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlatCustomer.Middleware.Core.DTOs.Resources
{
    public class GetLocalGovernmentsResponse
    {
        public IEnumerable<LocalGovernment> LocalGovernments { get; set; }
    }
}
