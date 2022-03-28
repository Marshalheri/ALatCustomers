using AlatCustomer.Middleware.Core.DTOs;
using AlatCustomer.Middleware.Core.Models;
using System.Threading.Tasks;

namespace AlatCustomer.Middleware.Core.Processors
{
    public interface IOtpProcessor
    {
        Task<BasicResponse> SendOtpAsync(string phoneNumber, OtpPurpose otpPurpose, string emailAddress);
        Task<BasicResponse> ValidateOtpAsync(string phoneNumber, OtpPurpose otpPurpose, string otp);
    }
}
