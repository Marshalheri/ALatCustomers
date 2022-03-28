using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlatCustomer.Middleware.Core.Models
{
    public class Customer : BaseModel
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] Password { get; set; }
        public string EmailAddress { get; set; }
        public string StateOfResidence { get; set; }
        public string LocalGovernmentOfResidence { get; set; }
    }
}
