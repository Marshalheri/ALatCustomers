using AlatCustomer.Middleware.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AlatCustomer.Middleware.Core.Repository
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Otp> Otps { get; set; }
    }
}
