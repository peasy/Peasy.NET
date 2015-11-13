using System.Collections.Generic;
using Peasy.Core;
using Orders.com.DataProxy;
using Orders.com.Domain;
using Orders.com.BLL.Rules;
using System.Threading.Tasks;

namespace Orders.com.BLL
{
    public class CategoryService : OrdersDotComServiceBase<Category>, ICategoryService
    {
        private IProductDataProxy _productsDataProxy;

        public CategoryService(ICategoryDataProxy dataProxy, IProductDataProxy productsDataProxy) : base(dataProxy)
        {
            _productsDataProxy = productsDataProxy;
        }

        protected override IEnumerable<IRule> GetBusinessRulesForDelete(long id, ExecutionContext<Category> context)
        {
            yield return base.GetBusinessRulesForDelete(id, context)
                             .IfAllValidThenValidate
                             (
                                new CanDeleteCategoryRule(id, _productsDataProxy)
                             );
        }

        protected override async Task<IEnumerable<IRule>> GetBusinessRulesForDeleteAsync(long id, ExecutionContext<Category> context)
        {
            var baseRules = await base.GetBusinessRulesForDeleteAsync(id, context);
            return baseRules.IfAllValidThenValidate(new CanDeleteCategoryRule(id, _productsDataProxy))
                            .ToArray();
        }
    }
}
