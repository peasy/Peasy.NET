using AutoMapper;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.com.DAL.Mock
{
    public class CustomerRepository : ICustomerDataProxy
    {
        private object _lockObject = new object();

        public CustomerRepository()
        {
            Mapper.CreateMap<Customer, Customer>();
        }

        private static List<Customer> _customers;

        private static List<Customer> Customers
        {
            get
            {
                if (_customers == null)
                {
                    _customers = new List<Customer>()
                    {
                        new Customer() { CustomerID = 1, Name = "Jimi Hendrix" },
                        new Customer() { CustomerID = 2, Name = "David Gilmour" },
                        new Customer() { CustomerID = 3, Name = "James Page" }
                    };
                }
                return _customers;
            }
        }
        public IEnumerable<Customer> GetAll()
        {
            Debug.WriteLine("Executing EF Customer.GetAll");
            // Simulate a SELECT against a database
            return Customers.Select(Mapper.Map<Customer, Customer>).ToArray();
        }

        public Customer GetByID(long id)
        {
            Debug.WriteLine("Executing EF Customer.GetByID");
            var customer = Customers.First(c => c.ID == id);
            return Mapper.Map(customer, new Customer());
        }

        public Customer Insert(Customer entity)
        {
            lock (_lockObject)
            {
                Debug.WriteLine("INSERTING customer into database");
                var nextID = Customers.Max(c => c.ID) + 1;
                entity.ID = nextID;
                Customers.Add(Mapper.Map(entity, new Customer()));
                return entity;
            }
        }

        public Customer Update(Customer entity)
        {
            Debug.WriteLine("UPDATING customer in database");
            var existing = Customers.First(c => c.ID == entity.ID);
            Mapper.Map(entity, existing);
            return entity;
        }

        public void Delete(long id)
        {
            Debug.WriteLine("DELETING customer in database");
            var customer = Customers.First(c => c.ID == id);
            Customers.Remove(customer);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return GetAll();
        }

        public async Task<Customer> GetByIDAsync(long id)
        {
            return GetByID(id);
        }

        public async Task<Customer> InsertAsync(Customer entity)
        {
            return Insert(entity);
        }

        public async Task<Customer> UpdateAsync(Customer entity)
        {
            return Update(entity);
        }

        public async Task DeleteAsync(long id)
        {
            Delete(id);
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
