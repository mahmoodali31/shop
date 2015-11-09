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
   public class OrderDetailRepository:IOrderDetail
    {
          private readonly ShoppingCartEntities shoppingCardDB;

          public OrderDetailRepository()
        {
            shoppingCardDB = new ShoppingCartEntities();
        }
        public void Create(OrderDetail orderDetail)
        {
            shoppingCardDB.OrderDetails.Add(orderDetail);
            Save();
        }
        public void Update(OrderDetail orderDetail)
        {
            shoppingCardDB.Entry(orderDetail).State = EntityState.Modified;
            Save();
        }
        public void Delete(int pkOrderId)
        {
            OrderDetail orderDetail = shoppingCardDB.OrderDetails.Find(pkOrderId);
            shoppingCardDB.OrderDetails.Remove(orderDetail);
            Save();
        }
        public OrderDetail GetOrderDetailByID(int pkDetailId)
        {
           return shoppingCardDB.OrderDetails.Find(pkDetailId);
        }
        public IEnumerable<OrderDetail> GetAllOrderDetails()
        {
            return shoppingCardDB.OrderDetails.ToList();
        }
        public IEnumerable<OrderDetail> GetOrderDetailByFKOrderId(int FKOrderId)
        {
            return shoppingCardDB.OrderDetails.Where(od => od.FKOrderId == FKOrderId);
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
        ~OrderDetailRepository()
    {
        Dispose(false);
    }
    }
}
