using System.Collections.Generic;
using Peasy.Core;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using Orders.com.BLL.Rules;
using System.Threading.Tasks;

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

        protected override async Task<IEnumerable<IRule>> GetBusinessRulesForDeleteAsync(long id, ExecutionContext<Category> context)
        {
            var baseRules = await base.GetBusinessRulesForDeleteAsync(id, context);
            return baseRules.IfAllValidThenValidate(new CanDeleteCategoryRule(id, _productService))
                            .ToArray();
        }
    }
}
