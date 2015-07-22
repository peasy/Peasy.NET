using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;

namespace Orders.com.BLL
{
    public class CategoryService : OrdersDotComServiceBase<Category> 
    {
        public CategoryService(ICategoryDataProxy dataProxy) : base(dataProxy)
        {
        }
    }
}
