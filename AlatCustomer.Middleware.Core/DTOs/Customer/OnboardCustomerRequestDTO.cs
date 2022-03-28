namespace AlatCustomer.Middleware.Core.DTOs.Customer
{
    public class OnboardCustomerRequestDTO : BaseRequestValidatorDTO
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string StateOfResidenceCode { get; set; }
        public string LgaOfResidenceCode { get; set; }
        public string Otp { get; set; }
        public NewPasswordDTO NewPassword { get; set; }

        public override bool IsValid(out string problemSource)
        {
            problemSource = string.Empty;
            if (string.IsNullOrEmpty(Name))
            {
                problemSource = "Name";
                return false;
            }
            if (string.IsNullOrEmpty(PhoneNumber))
            {
                problemSource = "Phone Number";
                return false;
            }
            if (string.IsNullOrEmpty(EmailAddress))
            {
                problemSource = "Email Address";
                return false;
            }
            if (string.IsNullOrEmpty(StateOfResidenceCode))
            {
                problemSource = "State Of Residence Code";
                return false;
            }
            if (string.IsNullOrEmpty(LgaOfResidenceCode))
            {
                problemSource = "Lga Of Residence Code";
                return false;
            }
            if (string.IsNullOrEmpty(Name))
            {
                problemSource = "Name";
                return false;
            }
            if (string.IsNullOrEmpty(Otp))
            {
                problemSource = "Otp";
                return false;
            }
            if (!NewPassword.IsValid(out problemSource))
            {
                return false;
            }
            return true;
        }
    }
}
