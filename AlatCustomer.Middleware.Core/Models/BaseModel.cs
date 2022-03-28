using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlatCustomer.Middleware.Core.Models
{
    public class BaseModel
    {
        public long Id { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
