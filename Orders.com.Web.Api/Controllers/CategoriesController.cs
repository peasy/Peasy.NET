using Orders.com.BLL;
using Orders.com.Domain;

namespace Orders.com.Web.Api.Controllers
{
    public class CategoriesController : ApiControllerBase<Category, long>
    {
        public CategoriesController(ICategoryService categoryService)
        {
            _businessService = categoryService;
        }
    }
}