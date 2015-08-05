using AutoMapper;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using Orders.com.Core.Extensions;
using Orders.com.Core.QueryData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
                        new Order() { OrderID = 1, CustomerID = 1, OrderDate = DateTime.Now.AddMonths(-3), OrderStatusID = 1 }
                    };
                }
                return _orders;
            }
        }

        public IEnumerable<OrderInfo> GetAll(int start, int pageSize)
        {
            var orders = GetAll();
            var customers = new CustomerRepository().GetAll().ToDictionary(c => c.CustomerID);
            var orderItems = new OrderItemRepository().GetAll().ToArray();
            var results = orders.Skip(start)
                                .Take(pageSize)
                                .Select(o => new OrderInfo()
                                { 
                                    OrderID = o.OrderID,
                                    OrderDate = o.OrderDate,
                                    CustomerName = customers[o.CustomerID].Name,
                                    CustomerID = o.CustomerID,
                                    Total = orderItems.Where(i => i.OrderID == o.OrderID).Sum(i => i.Amount * i.Quantity.Value),
                                    StatusID = o.OrderStatusID,
                                    Status = o.OrderStatus().Name
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
            var order = Orders.First(c => c.ID == id);
            return Mapper.Map(order, new Order());
        }

        public Order Insert(Order entity)
        {
            Thread.Sleep(1000);
            Debug.WriteLine("INSERTING order into database");
            var nextID = Orders.Any() ? Orders.Max(c => c.ID) + 1 : 1;
            entity.ID = nextID;
            Orders.Add(Mapper.Map(entity, new Order()));
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

        public Order Submit(long orderID, DateTime submittedOn)
        {
            var existing = Orders.First(c => c.ID == orderID);
            existing.OrderStatus().SetSubmittedState();
            existing.SubmittedDate = submittedOn;
            return Mapper.Map(existing, new Order());
        }

        public Order Ship(long orderID, DateTime shippedOn)
        {
            var existing = Orders.First(c => c.ID == orderID);
            existing.OrderStatus().SetShippedState();
            existing.ShippedDate = shippedOn;
            return Mapper.Map(existing, new Order());
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

        public Task<Order> SubmitAsync(long orderID, DateTime submittedOn)
        {
            return Task.Run(() => Submit(orderID, submittedOn));
        }

        public Task<Order> ShipAsync(long orderID, DateTime shippedOn)
        {
            return Task.Run(() => Ship(orderID, shippedOn));
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
