using AutoMapper;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Orders.com.DAL.EF
{
    public class CategoryRepository : ICategoryDataProxy 
    {
        public CategoryRepository()
        {
            Mapper.CreateMap<Category, Category>();
        }

        private List<Category> _categories;

        private List<Category> Categories
        {
            get
            {
                if (_categories == null)
                {
                    _categories = new List<Category>() 
                    {
                        new Category() { CategoryID = 1, Name = "Guitars" },
                        new Category() { CategoryID = 2, Name = "Computers" },
                        new Category() { CategoryID = 3, Name = "Albums" }
                    };
                }
                return _categories;
            }
        }
        public IEnumerable<Category> GetAll()
        {
            Debug.WriteLine("Executing EF Category.GetAll");
            // Simulate a SELECT against a database
            return Categories.Select(Mapper.Map<Category, Category>).ToArray();
        }

        public Category GetByID(long id)
        {
            Debug.WriteLine("Executing EF Category.GetByID");
            var category = Categories.First(c => c.ID == id);
            return Mapper.Map(category, new Category());
        }

        public Category Insert(Category entity)
        {
            Thread.Sleep(1000);
            Debug.WriteLine("INSERTING category into database");
            var nextID = _categories.Max(c => c.ID) + 1;
            entity.ID = nextID;
            Categories.Add(Mapper.Map(entity, new Category()));
            return entity;
        }

        public Category Update(Category entity)
        {
            Thread.Sleep(1000);
            Debug.WriteLine("UPDATING category in database");
            var existing = Categories.First(c => c.ID == entity.ID);
            Mapper.Map(entity, existing);
            return entity;
        }

        public void Delete(long id)
        {
            Debug.WriteLine("DELETING category in database");
            var category = Categories.First(c => c.ID == id);
            Categories.Remove(category);
        }

        public Task<IEnumerable<Category>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }

        public Task<Category> GetByIDAsync(long id)
        {
            return Task.Run(() => GetByID(id));
        }

        public Task<Category> InsertAsync(Category entity)
        {
            return Task.Run(() => Insert(entity));
        }

        public Task<Category> UpdateAsync(Category entity)
        {
            return Task.Run(() => Update(entity));
        }

        public Task DeleteAsync(long id)
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
