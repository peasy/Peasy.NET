using Facile.Core;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.com.BLL.Rules
{
    public class CanDeleteCategoryRule : RuleBase
    {
        private long _categoryID;
        private ProductService _productService;

        public CanDeleteCategoryRule(long categoryID, ProductService productService)
        {
            _categoryID = categoryID;
            _productService = productService;
        }

        protected override void OnValidate()
        {
            var products = _productService.GetByCategoryCommand(_categoryID).Execute().Value;
            if (products.Any())
            {
                Invalidate("This category is associated with one or more products and cannot be deleted.");
            }
        }

        protected override async Task OnValidateAsync()
        {
            var products = await _productService.GetByCategoryCommand(_categoryID).ExecuteAsync();
            if (products.Value.Any())
            {
                Invalidate("This category is associated with one or more products and cannot be deleted.");
            }
        }
    }
}
