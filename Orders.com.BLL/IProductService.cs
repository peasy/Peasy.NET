using System.Collections.Generic;
using Orders.com.Core.Domain;
using Peasy.Core;

namespace Orders.com.BLL
{
    public interface IProductService : IService<Product, long>
    {
        ICommand<IEnumerable<Product>> GetByCategoryCommand(long categoryID);
    }
}