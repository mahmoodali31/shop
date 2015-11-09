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
    public class CategoryRepository:ICategory
    {
        private readonly ShoppingCartEntities shoppingCardDB;

        public CategoryRepository()
        {
            shoppingCardDB = new ShoppingCartEntities();
        }
        public IEnumerable<Category> GetAllCategory()
        {
            return shoppingCardDB.Categories.ToList();
        }
        public void Create(Category category)
        {
            shoppingCardDB.Categories.Add(category);
            Save();
        }
        public void Update(Category category)
        {
            shoppingCardDB.Entry(category).State = EntityState.Modified;
            Save();
        }
        public void Delete(int pkCategoryId)
        {
            Category category = shoppingCardDB.Categories.Find(pkCategoryId);
            shoppingCardDB.Categories.Remove(category);
            Save();
        }
        public Category GetById(int pkCategoryId)
        {
            return shoppingCardDB.Categories.Find(pkCategoryId);
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
        ~CategoryRepository()
    {
        Dispose(false);
    }
    }
}
