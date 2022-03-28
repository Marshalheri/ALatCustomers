using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlatCustomer.Middleware.Core.Repository
{
    public interface IUnitOfWork
    {
        ICustomerRepository CustomerRepository { get; }
        IOtpRepository OtpRepository { get; }
        Task BeginTransactionAsync();
        Task SaveAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
