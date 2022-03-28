using AlatCustomer.Middleware.Core.DTOs;
using AlatCustomer.Middleware.Core.Models;
using AlatCustomer.Middleware.Core.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlatCustomer.Middleware.Core.Fakes
{
    public class FakeOtpProcessor : IOtpProcessor
    {
        public Task<BasicResponse> SendOtpAsync(string phoneNumber, OtpPurpose otpPurpose, string emailAddress)
        {
            return Task.FromResult(new BasicResponse(true));
        }

        public Task<BasicResponse> ValidateOtpAsync(string phoneNumber, OtpPurpose otpPurpose, string otp)
        {
            return Task.FromResult(new BasicResponse(true));
        }
    }
}
