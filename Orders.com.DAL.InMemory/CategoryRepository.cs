using Orders.com.DataProxy;
using Orders.com.Domain;
using System.Collections.Generic;

namespace Orders.com.DAL.InMemory
{
    public class CategoryRepository : OrdersDotComMockBase<Category>, ICategoryDataProxy
    {
        protected override IEnumerable<Category> SeedDataProxy()
        {
            yield return new Category() { CategoryID = 1, Name = "Guitars" };
            yield return new Category() { CategoryID = 2, Name = "Computers" };
            yield return new Category() { CategoryID = 3, Name = "Albums" };
        }
    }
}
