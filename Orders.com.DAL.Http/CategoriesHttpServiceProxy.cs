using Orders.com.DataProxy;
using Orders.com.Domain;

namespace Orders.com.DAL.Http
{
    public class CategoriesHttpServiceProxy : OrdersDotComHttpProxyBase<Category, long>, ICategoryDataProxy
    {
        protected override string RequestUri
        {
            get { return $"{BaseAddress}/categories"; }
        }
    }
}
