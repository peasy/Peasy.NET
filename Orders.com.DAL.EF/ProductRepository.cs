using AutoMapper;
using Facile;
using Orders.com.Core.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Orders.com.DAL.EF
{
    public class ProductRepository : IServiceDataProxy<Product, int>
    {
        public ProductRepository()
        {
            Mapper.CreateMap<Product, Product>();
        }

        private List<Product> _products;

        private List<Product> Products
        {
            get
            {
                if (_products == null)
                {
                    _products = new List<Product>() 
                    {
                        new Product() { ProductID = 1, Name = "Led Zeppeling IV", Description = "Best album ever", Price=10 },
                        new Product() { ProductID = 2, Name = "PRS Hollow Body", Description = "Blue guitar", Price=2500 },
                        new Product() { ProductID = 3, Name = "Laptop", Price=1000 }
                    };
                }
                return _products;
            }
        }
        public IEnumerable<Product> GetAll()
        {
            Debug.WriteLine("Executing EF Product.GetAll");
            // Simulate a SELECT against a database
            return Products.Select(Mapper.Map<Product, Product>).ToArray();
        }

        public Product GetByID(int id)
        {
            Debug.WriteLine("Executing EF Product.GetByID");
            return Products.First(c => c.ID == id);
        }

        public Product Insert(Product entity)
        {
            Thread.Sleep(1000);
            Debug.WriteLine("INSERTING product into database");
            var nextID = _products.Max(c => c.ID) + 1;
            entity.ID = nextID;
            Products.Add(entity);
            return entity;
        }

        public Product Update(Product entity)
        {
            Thread.Sleep(1000);
            Debug.WriteLine("UPDATING product in database");
            var existing = Products.First(c => c.ID == entity.ID);
            Mapper.Map(entity, existing);
            return entity;
        }

        public void Delete(int id)
        {
            Debug.WriteLine("DELETING product in database");
            var product = Products.First(c => c.ID == id);
            Products.Remove(product);
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }

        public Task<Product> GetByIDAsync(int id)
        {
            return Task.Run(() => GetByID(id));
        }

        public Task<Product> InsertAsync(Product entity)
        {
            return Task.Run(() => Insert(entity));
        }

        public Task<Product> UpdateAsync(Product entity)
        {
            return Task.Run(() => Update(entity));
        }

        public Task DeleteAsync(int id)
        {
            return Task.Run(() => Delete(id));
        }

        public bool SupportsTransactions
        {
            get { return true; }
        }

        public bool IsLatencyProne
        {
            get { return false; }
        }
    }
}
