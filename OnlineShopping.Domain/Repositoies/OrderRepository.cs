using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShopping.Domain.Interfaces;
using Data;
using System.Data.Entity;

namespace OnlineShopping.Domain.Repositoies
{
    public class OrderRepository : IOrder
    {
          private readonly ShoppingCartEntities shoppingCardDB;

          public OrderRepository()
        {
            shoppingCardDB = new ShoppingCartEntities();
        }

          
        public int Create(Order order)
        {
            shoppingCardDB.Orders.Add(order);
            Save();

            return order.PKOrderId;
        }
        public void Update(Order order)
        {
            shoppingCardDB.Entry(order).State = EntityState.Modified;
            Save();
        }
        public void Delete(int pkOrderId)
        {
            Order order = shoppingCardDB.Orders.Find(pkOrderId);
            shoppingCardDB.Orders.Remove(order);
            Save();
        }
        public IEnumerable<Order> GetAllOrders()
        {
            return shoppingCardDB.Orders.ToList();
        }
        public IEnumerable<Order> GetOrdersByFKUserId(int FKUserId)
        {
            
            return shoppingCardDB.Orders.Where(o => o.FKUserId == FKUserId).ToList();
        }
        public void Save()
        {
            shoppingCardDB.SaveChanges();
        }
        //public void Dispose()
        //{
        //    if (shoppingCardDB != null)
        //    {
        //        shoppingCardDB.Dispose();
        //    }
        //    GC.SuppressFinalize(this);
        //}
         private bool disposed = false;
        protected virtual void Dispose(bool isManuallyDisposing)
        {
            if (!this.disposed)
            {
                if (isManuallyDisposing)
                    shoppingCardDB.Dispose();
            }

            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~OrderRepository()
    {
        Dispose(false);
    }
    }
}
