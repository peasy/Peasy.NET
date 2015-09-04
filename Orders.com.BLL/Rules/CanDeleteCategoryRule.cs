using Facile.Core;
using System.Linq;

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
    }
}
