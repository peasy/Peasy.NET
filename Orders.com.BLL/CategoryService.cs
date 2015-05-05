using Facile;
using Orders.com.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.BLL
{
    public class CategoryService : BusinessServiceBase<Category, int>
    {
        public CategoryService(IServiceDataProxy<Category, int> dataProxy) : base(dataProxy)
        {
        }
    }
}
