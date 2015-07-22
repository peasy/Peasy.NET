using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;

namespace Orders.com.BLL
{
    public class ProductService : OrdersDotComServiceBase<Product>
    {
        public ProductService(IProductDataProxy dataProxy) : base(dataProxy)
        {
        }
    }
}
