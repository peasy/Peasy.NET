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

namespace Orders.com.DAL.EF
{
    public class InventoryItemRepository : IInventoryItemDataProxy 
    {
        public InventoryItemRepository()
        {
            Mapper.CreateMap<InventoryItem, InventoryItem>();
        }

        private List<InventoryItem> _inventoryItems;

        private List<InventoryItem> InventoryItems
        {
            get
            {
                if (_inventoryItems == null)
                {
                    _inventoryItems = new List<InventoryItem>() 
                    {
                        new InventoryItem() { InventoryItemID = 1, ProductID = 1, QuantityOnHand = 10 },
                        new InventoryItem() { InventoryItemID = 2, ProductID = 2, QuantityOnHand = 1 },
                        new InventoryItem() { InventoryItemID = 3, ProductID = 3, QuantityOnHand = 4 },
                        new InventoryItem() { InventoryItemID = 4, ProductID = 4, QuantityOnHand = 3 },
                        new InventoryItem() { InventoryItemID = 5, ProductID = 5, QuantityOnHand = 20 },
                        new InventoryItem() { InventoryItemID = 6, ProductID = 6, QuantityOnHand = 54 },
                        new InventoryItem() { InventoryItemID = 7, ProductID = 7, QuantityOnHand = 12 },
                        new InventoryItem() { InventoryItemID = 8, ProductID = 8, QuantityOnHand = 10 },
                        new InventoryItem() { InventoryItemID = 9, ProductID = 9, QuantityOnHand = 34 },
                        new InventoryItem() { InventoryItemID = 10, ProductID = 10, QuantityOnHand = 11 }
                    };
                }
                return _inventoryItems;
            }
        }
        public IEnumerable<InventoryItem> GetAll()
        {
            Debug.WriteLine("Executing EF InventoryItem.GetAll");
            // Simulate a SELECT against a database
            return InventoryItems.Select(Mapper.Map<InventoryItem, InventoryItem>).ToArray();
        }

        public InventoryItem GetByID(long id)
        {
            Debug.WriteLine("Executing EF InventoryItem.GetByID");
            var inventoryItem = InventoryItems.First(c => c.ID == id);
            return Mapper.Map(inventoryItem, new InventoryItem());
        }

        public InventoryItem GetByProduct(long productID)
        {
            Debug.WriteLine("Executing EF InventoryItem.GetByProduct");
            var inventoryItem = InventoryItems.First(c => c.ProductID == productID);
            return Mapper.Map(inventoryItem, new InventoryItem());
        }

        public InventoryItem Insert(InventoryItem entity)
        {
            Thread.Sleep(1000);
            Debug.WriteLine("INSERTING inventoryItem into database");
            var nextID = _inventoryItems.Max(c => c.ID) + 1;
            entity.ID = nextID;
            InventoryItems.Add(Mapper.Map(entity, new InventoryItem()));
            return entity;
        }

        public InventoryItem Update(InventoryItem entity)
        {
            Thread.Sleep(1000);
            Debug.WriteLine("UPDATING inventoryItem in database");
            var existing = InventoryItems.First(c => c.ID == entity.ID);
            Mapper.Map(entity, existing);
            return entity;
        }

        public void Delete(long id)
        {
            Debug.WriteLine("DELETING inventoryItem in database");
            var inventoryItem = InventoryItems.First(c => c.ID == id);
            InventoryItems.Remove(inventoryItem);
        }

        public Task<IEnumerable<InventoryItem>> GetAllAsync()
        {
            return Task.Run(() => GetAll());
        }

        public Task<InventoryItem> GetByIDAsync(long id)
        {
            return Task.Run(() => GetByID(id));
        }

        public Task<InventoryItem> GetByProductAsync(long productID)
        {
            return Task.Run(() => GetByProduct(productID));
        }

        public Task<InventoryItem> InsertAsync(InventoryItem entity)
        {
            return Task.Run(() => Insert(entity));
        }

        public Task<InventoryItem> UpdateAsync(InventoryItem entity)
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
