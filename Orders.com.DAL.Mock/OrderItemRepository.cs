using AutoMapper;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using Orders.com.Core.Extensions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace Orders.com.DAL.Mock
{
    public class OrderItemRepository : IOrderItemDataProxy 
    {
        private int _counter;
        private object _lock = new object();

        public OrderItemRepository()
        {
            Mapper.CreateMap<OrderItem, OrderItem>();
        }

        private static List<OrderItem> _orderItems;

        private static List<OrderItem> OrderItems
        {
            get
            {
                if (_orderItems == null)
                {
                    _orderItems = new List<OrderItem>()
                    {
                    };
                }
                return _orderItems;
            }
        }
        public IEnumerable<OrderItem> GetAll()
        {
            Debug.WriteLine("Executing EF OrderItem.GetAll");
            // Simulate a SELECT against a database
            return OrderItems.Select(Mapper.Map<OrderItem, OrderItem>).ToArray();
        }

        public OrderItem GetByID(long id)
        {
            Debug.WriteLine("Executing EF OrderItem.GetByID");
            var orderItem = OrderItems.First(c => c.ID == id);
            return Mapper.Map(orderItem, new OrderItem());
        }

        public IEnumerable<OrderItem> GetByOrder(long orderID)
        {
            Debug.WriteLine("Executing EF OrderItem.GetByOrder");
            return OrderItems.Where(i => i.OrderID == orderID)
                             .Select(Mapper.Map<OrderItem, OrderItem>)
                             .ToArray();
        }

        public OrderItem Insert(OrderItem entity)
        {
            Debug.WriteLine("INSERTING orderItem into database");
            lock (_lock)
            {
                _counter++;
            }
            entity.ID = _counter;
            OrderItems.Add(Mapper.Map(entity, new OrderItem()));
            return entity;
        }

        public OrderItem Update(OrderItem entity)
        {
            Debug.WriteLine("UPDATING orderItem in database");
            var existing = OrderItems.First(c => c.ID == entity.ID);
            Mapper.Map(entity, existing);
            return entity;
        }

        public void Delete(long id)
        {
            Debug.WriteLine("DELETING orderItem in database");
            var orderItem = OrderItems.First(c => c.ID == id);
            OrderItems.Remove(orderItem);
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            return GetAll();
        }

        public async Task<IEnumerable<OrderItem>> GetByOrderAsync(long orderID)
        {
            return GetByOrder(orderID);
        }

        public async Task<OrderItem> GetByIDAsync(long id)
        {
            return GetByID(id);
        }

        public async Task<OrderItem> InsertAsync(OrderItem entity)
        {
            return Insert(entity);
        }

        public async Task<OrderItem> UpdateAsync(OrderItem entity)
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
