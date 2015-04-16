using Facile;
using Orders.com.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.Core.DataProxy
{
    public interface IProductDataProxy : IDataProxy<Product, int>
    {
    }
}
