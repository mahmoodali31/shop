using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using System.Data.Entity;
using OnlineShopping.Domain.Interfaces;

namespace OnlineShopping.Domain.Repositoies
{
   public class ShippingAddressRepository:IShippingAddress
    {
        //IEnumerable<UserAddress> GetAllAddress();
        private bool disposed = false;
        ShoppingCartEntities shoppingCardDB = new ShoppingCartEntities();
        public UserAddress GetAddressByPkId(int pkAddId)
        {
            return shoppingCardDB.UserAddresses.Where(x => x.PKShippingAddressId == pkAddId).SingleOrDefault();
        }
        public void Create(UserAddress UserAddress)
        {

            shoppingCardDB.UserAddresses.Add(UserAddress);
            Save();
        }
        public void Update(UserAddress UserAddress)
        {
          
            shoppingCardDB.Entry(UserAddress).State = EntityState.Modified;
            Save();
        }
        public void Delete(int PkAddId)
        {
            UserAddress UserAddress = shoppingCardDB.UserAddresses.Find(PkAddId);
            shoppingCardDB.UserAddresses.Remove(UserAddress);
            Save();
        }
        public UserAddress GetAddressByUserId(int userId)
        {
            return shoppingCardDB.UserAddresses.Where(x => x.FKUserId == userId).SingleOrDefault();
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
        ~ShippingAddressRepository()
    {
        Dispose(false);
    }
    }
}
