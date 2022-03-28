using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlatCustomer.Middleware.Core
{
    public class SystemSettings
    {
        public bool UseSwagger { get; set; }
        public bool UseFake { get; set; }
        public IEnumerable<State> States { get; set; }
        public IEnumerable<LocalGovernment> LocalGovernments { get; set; }
    }

    public class State
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class LocalGovernment
    {
        public string StateCode { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
