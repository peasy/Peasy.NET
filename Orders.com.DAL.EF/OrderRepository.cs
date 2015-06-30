using AutoMapper;
using Facile;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Orders.com.Core.Extensions;
using Orders.com.Core.QueryData;

namespace Orders.com.DAL.EF
{
    public class OrderRepository : IOrderDataProxy
    {
        public OrderRepository()
        {
            Mapper.CreateMap<Order, Order>();
        }

        private List<Order> _orders;

        private List<Order> Orders
        {
            get
            {
                if (_orders == null)
                {
                    _orders = new List<Order>()
                    {
                        new Order() { OrderID = 1, CustomerID = 1, OrderDate = DateTime.Now.AddMonths(-3), Total = 455 }
                    };
                }
                return _orders;
            }
        }

        public IEnumerable<OrderInfo> GetAll(int start, int pageSize)
        {
            var orders = GetAll();
            var customers = new CustomerRepository().GetAll().ToDictionary(c => c.CustomerID);
            var results = orders.Skip(start)
                                .Take(pageSize)
                                .Select(o => new OrderInfo()
                                { 
                                    OrderID = o.OrderID,
                                    OrderDate = o.OrderDate,
                                    CustomerName = customers[o.CustomerID].Name,
                                    Total = o.Total
                                });
            return results.ToArray();
        }

        public IEnumerable<Order> GetAll()
        {
            Debug.WriteLine("Executing EF Order.GetAll");
            // Simulate a SELECT against a database
            return Orders.Select(Mapper.Map<Order, Order>).ToArray();
        }

        public Order GetByID(long id)
        {
            Debug.WriteLine("Executing EF Order.GetByID");
            return Orders.First(c => c.ID == id);
        }

        public Order Insert(Order entity)
        {
            Thread.Sleep(1000);
            Debug.WriteLine("INSERTING order into database");
            var nextID = Orders.Any() ? Orders.Max(c => c.ID) + 1 : 1;
            entity.ID = nextID;
            Orders.Add(entity);
            return entity;
        }

        public Order Update(Order entity)
        {
            Thread.Sleep(1000);
            Debug.WriteLine("UPDATING order in database");
            var existing = Orders.First(c => c.ID == entity.ID);
            Mapper.Map(entity, existing);
            return entity;
        }

        public void Delete(long id)
        {
            Debug.WriteLine("DELETING order in database");
            var order = Orders.First(c => c.ID == id);
            Orders.Remove(order);
        }

        public Task<IEnumerable<Order>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }

        public Task<IEnumerable<OrderInfo>> GetAllAsync(int start, int pageSize)
        {
            return Task.Run(() => GetAll(start, pageSize));
        }

        public Task<Order> GetByIDAsync(long id)
        {
            return Task.Run(() => GetByID(id));
        }

        public Task<Order> InsertAsync(Order entity)
        {
            return Task.Run(() => Insert(entity));
        }

        public Task<Order> UpdateAsync(Order entity)
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
