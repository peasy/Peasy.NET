using Orders.com.Domain;
using Peasy.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.BLL
{
    public interface ICustomerService : IService<Customer, long>
    {
    }
}
