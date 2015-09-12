using System.Collections.Generic;
using Facile.Core;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using Orders.com.BLL.Rules;

namespace Orders.com.BLL
{
    public class CategoryService : OrdersDotComServiceBase<Category> 
    {
        private ProductService _productService;

        public CategoryService(ICategoryDataProxy dataProxy, ProductService productService) : base(dataProxy)
        {
            _productService = productService;
        }

        protected override IEnumerable<IRule> GetBusinessRulesForDelete(long id, ExecutionContext<Category> context)
        {
            yield return base.GetBusinessRulesForDelete(id, context)
                             .IfAllValidThenValidate
                             (
                                new CanDeleteCategoryRule(id, _productService)
                             );
        }
    }
}
