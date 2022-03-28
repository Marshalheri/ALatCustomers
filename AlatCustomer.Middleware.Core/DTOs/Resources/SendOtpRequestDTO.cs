using AlatCustomer.Middleware.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlatCustomer.Middleware.Core.DTOs.Resources
{
    public class SendOtpRequestDTO : BaseRequestValidatorDTO
    {
        public OtpPurpose OtpPurpose { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Name { get; set; }

        public override bool IsValid(out string problemSource)
        {
            problemSource = string.Empty;

            if (string.IsNullOrEmpty(PhoneNumber))
            {
                problemSource = "Phone Number";
                return false;
            }
            if (string.IsNullOrEmpty(Name))
            {
                problemSource = "Name";
                return false;
            }
            if (!Enum.IsDefined(typeof(OtpPurpose), OtpPurpose))
            {
                problemSource = "Otp Purpose";
                return false;
            }
            return true;
        }
    }
}
