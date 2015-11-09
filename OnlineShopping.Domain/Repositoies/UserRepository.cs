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
    public class UserRepository : IUsers
    {
        private readonly ShoppingCartEntities shoppingCardDB;
        private bool disposed = false;
        public UserRepository()
        {
            shoppingCardDB = new ShoppingCartEntities();
        }
        public int Create(User user)
        {
            shoppingCardDB.Users.Add(user);
           
            Save();
           
            return user.PKUserId;
        }
        public void Update(User user)
        {
            shoppingCardDB.Entry(user).State = EntityState.Modified;
            Save();
        }
        public void Delet(int pkUserId)
        {
            User user = shoppingCardDB.Users.Find(pkUserId);
            shoppingCardDB.Users.Remove(user);
            Save();
        }
        public IEnumerable<User> GetAllUsers()
        {
            return shoppingCardDB.Users.ToList();
        }

        
        public User GetByEmail(string email)
        {
            return shoppingCardDB.Users.Where(x=>x.EmailId==email).SingleOrDefault();
        }
        public void Save()
        {
            shoppingCardDB.SaveChanges();
        }
        public bool VerifyEmail(string email)
        {
            IEnumerable<User> userList = GetAllUsers();

            foreach (Data.User u in userList)
            {

                if (email == u.EmailId)
                {

                    return true;
                }
            }
            return false;
           
        }
        public bool ValidateUser(string userName,string password)
        {
            int count = shoppingCardDB.Users.Where(x => x.EmailId == userName && x.Password == password).Count();
            if (count != 0)
            {
                return true;
            }
            else
            {
                return false;
                    
            }
        }
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
        ~UserRepository()
    {
        Dispose(false);
    }
        //public  void Dispose()
        //{
        //    if (shoppingCardDB!=null)
        //    {
        //        shoppingCardDB.Dispose();
        //    }
        //    GC.SuppressFinalize(this);
        //}

    }
}

