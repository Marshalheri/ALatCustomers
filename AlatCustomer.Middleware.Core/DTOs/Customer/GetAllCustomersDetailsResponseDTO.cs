using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlatCustomer.Middleware.Core.DTOs.Customer
{
    public class GetAllCustomersDetailsResponseDTO
    {
        public IEnumerable<CustomersDetails> Customers { get; set; }
    }

    public class CustomersDetails
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string StateOfResidence { get; set; }
        public string LocalGovernmentOfResidence { get; set; }
        public string DateCreated { get; set; }

    }
}
