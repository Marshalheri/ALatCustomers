using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlatCustomer.Middleware.Core.DTOs.Customer
{
    public class NewPasswordDTO
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool IsValid(out string problemSource)
        {
            problemSource = string.Empty;

            if (string.IsNullOrEmpty(Password) || !(Password.Equals(ConfirmPassword)))
            {
                problemSource = "New Password";
                return false;
            }
            return true;
        }
    }
}
