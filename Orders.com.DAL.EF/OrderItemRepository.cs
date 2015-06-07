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
    public class OrderItemRepository : IServiceDataProxy<OrderItem, int>
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
                    _orderItems = new List<OrderItem>();
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

        public OrderItem GetByID(int id)
        {
            Debug.WriteLine("Executing EF OrderItem.GetByID");
            return OrderItems.First(c => c.ID == id);
        }

        public OrderItem Insert(OrderItem entity)
        {
            Thread.Sleep(1000);
            Debug.WriteLine("INSERTING orderItem into database");
            var nextID = OrderItems.Any() ? OrderItems.Max(c => c.ID) + 1 : 1;
            entity.ID = nextID;
            OrderItems.Add(entity);
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

        public void Delete(int id)
        {
            Debug.WriteLine("DELETING orderItem in database");
            var orderItem = OrderItems.First(c => c.ID == id);
            OrderItems.Remove(orderItem);
        }

        public Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }

        public Task<OrderItem> GetByIDAsync(int id)
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
