using Orders.com.DataProxy;
using Orders.com.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.DAL.Http
{
    public class ProductsHttpServiceProxy : HttpServiceProxyBase<Product, long>, IProductDataProxy
    {
        protected override string RequestUri
        {
            get { return "http://localhost:53534/api/products"; }
        }

        public IEnumerable<Product> GetByCategory(long categoryID)
        {
            return base.GET<IEnumerable<Product>>($"{RequestUri}?categoryid={categoryID}");
        }

        public Task<IEnumerable<Product>> GetByCategoryAsync(long categoryID)
        {
            return base.GETAsync<IEnumerable<Product>>($"{RequestUri}?categoryid={categoryID}");
        }
    }
}
