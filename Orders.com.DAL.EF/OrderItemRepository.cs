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

namespace Orders.com.DAL.EF
{
    public class OrderItemRepository : IOrderItemDataProxy 
    {
        public OrderItemRepository()
        {
            Mapper.CreateMap<OrderItem, OrderItem>();
        }

        private List<OrderItem> _orderItems;

        private List<OrderItem> OrderItems
        {
            get
            {
                if (_orderItems == null)
                {
                    _orderItems = new List<OrderItem>()
                    {
                        new OrderItem() { ID = 1, OrderID = 1, Amount = 10, ProductID = 1, Quantity = 3 }
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
            return OrderItems.Where(i => i.OrderID == orderID)
                             .Select(Mapper.Map<OrderItem, OrderItem>)
                             .ToArray();
        }

        public OrderItem Insert(OrderItem entity)
        {
            Thread.Sleep(1000);
            Debug.WriteLine("INSERTING orderItem into database");
            var nextID = OrderItems.Any() ? OrderItems.Max(c => c.ID) + 1 : 1;
            entity.ID = nextID;
            OrderItems.Add(Mapper.Map(entity, new OrderItem()));
            return entity;
        }

        public OrderItem Update(OrderItem entity)
        {
            Thread.Sleep(1000);
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

        public Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }

        public Task<IEnumerable<OrderItem>> GetByOrderAsync(long orderID)
        {
            return Task.Run(() => GetByOrder(orderID));
        }

        public Task<OrderItem> GetByIDAsync(long id)
        {
            return Task.Run(() => GetByID(id));
        }

        public Task<OrderItem> InsertAsync(OrderItem entity)
        {
            return Task.Run(() => Insert(entity));
        }

        public Task<OrderItem> UpdateAsync(OrderItem entity)
        {
            return Task.Run(() => Update(entity));
        }

        public Task DeleteAsync(long id)
        {
            return Task.Run(() => Delete(id));
        }

        public OrderItem Ship(long orderItemID, DateTime shippedOn)
        {
            var existing = OrderItems.First(c => c.ID == orderItemID);
            existing.OrderStatus().SetShippedState();
            existing.ShippedDate = shippedOn;
            return Mapper.Map(existing, new OrderItem());
        }

        public Task<OrderItem> ShipAsync(long orderItemID, DateTime shippedOn)
        {
            return Task.Run(() => Ship(orderItemID, shippedOn));
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
