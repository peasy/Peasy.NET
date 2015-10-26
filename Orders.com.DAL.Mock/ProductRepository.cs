using AutoMapper;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace Orders.com.DAL.Mock
{
    public class ProductRepository : OrdersDotComMockBase<Product>, IProductDataProxy
    {
        protected override IEnumerable<Product> SeedDataProxy()
        {
            yield return new Product() { ProductID = 1, Name = "Led Zeppelin I", Description = "Best album ever", Price = 10, CategoryID = 3 };
            yield return new Product() { ProductID = 2, Name = "Fender Strat", Description = "Blue guitar", Price = 1000, CategoryID = 1 };
            yield return new Product() { ProductID = 3, Name = "Laptop", Price = 1000, CategoryID = 2 };
            yield return new Product() { ProductID = 4, Name = "Led Zeppelin II", Description = "Second best album ever", Price = 11, CategoryID = 3 };
            yield return new Product() { ProductID = 5, Name = "Gibson Les Paul", Description = "Red guitar", Price = 3000, CategoryID = 1 };
            yield return new Product() { ProductID = 6, Name = "Desktop", Price = 500.5M, CategoryID = 2 };
            yield return new Product() { ProductID = 7, Name = "Led Zeppelin III", Description = "Third best album ever", Price = 12, CategoryID = 3 };
            yield return new Product() { ProductID = 8, Name = "PRS Solid Body", Description = "Orange guitar", Price = 2000, CategoryID = 1 };
            yield return new Product() { ProductID = 9, Name = "Led Zeppelin IV", Description = "Fourth best album ever", Price = 11, CategoryID = 3 };
            yield return new Product() { ProductID = 10, Name = "PRS Hollow Body", Description = "Green guitar", Price = 2500, CategoryID = 1 };
        }

        public IEnumerable<Product> GetByCategory(long categoryID)
        {
            Debug.WriteLine("Executing EF Product.GetByCategory");
            return Data.Values.Where(p => p.CategoryID == categoryID)
                              .Select(Mapper.Map<Product, Product>).ToArray();
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(long categoryID)
        {
            return GetByCategory(categoryID);
        }
    }
}
