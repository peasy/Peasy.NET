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
    public class OrderStatusRepository : IOrderStatusDataProxy 
    {
        public OrderStatusRepository()
        {
            Mapper.CreateMap<OrderStatus, OrderStatus>();
        }

        private List<OrderStatus> _orderStatuses;

        private List<OrderStatus> OrderStatuses
        {
            get
            {
                if (_orderStatuses == null)
                {
                    _orderStatuses = new List<OrderStatus>() 
                    {
                        new OrderStatus() { OrderStatusID = 1, Name = "Pending" },
                        new OrderStatus() { OrderStatusID = 2, Name = "Submitted" },
                        new OrderStatus() { OrderStatusID = 3, Name = "Shipped" }
                    };
                }
                return _orderStatuses;
            }
        }
        public IEnumerable<OrderStatus> GetAll()
        {
            Debug.WriteLine("Executing EF OrderStatus.GetAll");
            // Simulate a SELECT against a database
            return OrderStatuses.Select(Mapper.Map<OrderStatus, OrderStatus>).ToArray();
        }

        public OrderStatus GetByID(long id)
        {
            Debug.WriteLine("Executing EF OrderStatus.GetByID");
            var orderStatus = OrderStatuses.First(c => c.ID == id);
            return Mapper.Map(orderStatus, new OrderStatus());
        }

        public OrderStatus Insert(OrderStatus entity)
        {
            Debug.WriteLine("INSERTING orderStatus into database");
            var nextID = OrderStatuses.Max(c => c.ID) + 1;
            entity.ID = nextID;
            OrderStatuses.Add(Mapper.Map(entity, new OrderStatus()));
            return entity;
        }

        public OrderStatus Update(OrderStatus entity)
        {
            Debug.WriteLine("UPDATING orderStatus in database");
            var existing = OrderStatuses.First(c => c.ID == entity.ID);
            Mapper.Map(entity, existing);
            return entity;
        }

        public void Delete(long id)
        {
            Debug.WriteLine("DELETING orderStatus in database");
            var orderStatus = OrderStatuses.First(c => c.ID == id);
            OrderStatuses.Remove(orderStatus);
        }

        public Task<IEnumerable<OrderStatus>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }

        public Task<OrderStatus> GetByIDAsync(long id)
        {
            return Task.Run(() => GetByID(id));
        }

        public Task<OrderStatus> InsertAsync(OrderStatus entity)
        {
            return Task.Run(() => Insert(entity));
        }

        public Task<OrderStatus> UpdateAsync(OrderStatus entity)
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
