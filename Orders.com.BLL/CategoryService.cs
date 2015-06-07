using Facile;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.BLL
{
    public class CategoryService : OrdersDotComServiceBase<Category> 
    {
        public CategoryService(ICategoryDataProxy dataProxy) : base(dataProxy)
        {
        }
    }
}
