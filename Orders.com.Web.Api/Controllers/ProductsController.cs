using Orders.com.BLL;
using Orders.com.Domain;
using System.Collections.Generic;
using System.Web.Http;

namespace Orders.com.Web.Api.Controllers
{
    public class ProductsController : ApiControllerBase<Product, long>
    {
        public ProductsController(IProductService productService)
        {
            _businessService = productService;
        }

        [HttpGet]
        /// GET api/products?categoryid=123
        public IEnumerable<Product> GetByCategory(long categoryID)
        {
            var products = (_businessService as IProductService).GetByCategoryCommand(categoryID).Execute().Value;
            return products;
        }
    }
}