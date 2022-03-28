using AlatCustomer.Middleware.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AlatCustomer.Middleware.Core.Repository
{
    public class OtpRepository : Repository<Otp>, IOtpRepository
    {

        public OtpRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
