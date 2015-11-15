using Orders.com.BLL;
using Orders.com.Domain;

namespace Orders.com.Web.Api.Controllers
{
    public class ProductsController : ApiControllerBase<Product, long>
    {
        public ProductsController(IProductService productService)
        {
            _businessService = productService;
        }
    }
}