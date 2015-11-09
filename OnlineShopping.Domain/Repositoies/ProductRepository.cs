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
   public class ProductRepository:IProduct
    {
        private readonly ShoppingCartEntities shoppingCardDB;

        public ProductRepository()
        {
            shoppingCardDB = new ShoppingCartEntities();
        }


        public IEnumerable<Product> GetAllProduct()
        {
            return shoppingCardDB.Products.ToList();
        }
        public void Create(Product product)
        {
            shoppingCardDB.Products.Add(product);
            Save();
        }
        public void Update(Product product)
        {
            shoppingCardDB.Entry(product).State = EntityState.Modified;
            Save();
        }

        public void Delete(int pkProductId)
        {
            Product product = shoppingCardDB.Products.Find(pkProductId);
            shoppingCardDB.Products.Remove(product);
            Save();
        }

        public Product GetById(int pkProductId)
        {
            return shoppingCardDB.Products.Find(pkProductId);
        }

        public IQueryable<ProductyByCategory> GetAllProductsBasedOnCategory()
        {
            return from pro in shoppingCardDB.Products
                   join cat in shoppingCardDB.Categories on pro.FKCategoryId equals cat.PKCategoryId
                   select new ProductyByCategory
                   {
                       PKProductId = pro.PKProductId,
                       ProductName = pro.ProductName,
                       Description = pro.Description,
                       ImagePath = pro.ImagePath,
                       LargeImagePath = pro.LargeImagePath,
                       Price = pro.Price,
                       Quantity = pro.Quantity,
                       DateCreated = pro.DateCreated,
                       DateUpdated = pro.DateUpdated,
                      // IsActive = pro.IsActive,
                       CategoryName = cat.CategoryName,
                       FKProductId = pro.FKProductId
                   };
        }

        public IList<ProductyByCategory> GetAllProductsByFKCatId(int FKCatId)
        {
            if (FKCatId != 0)
            {
                var products = from pro in shoppingCardDB.Products
                               join cat in shoppingCardDB.Categories
                               on pro.FKCategoryId equals cat.PKCategoryId
                               where pro.FKCategoryId == FKCatId
                               select new ProductyByCategory
                               {
                                   PKProductId = pro.PKProductId,
                                   ProductName = pro.ProductName,
                                   Description = pro.Description,
                                   ImagePath = pro.ImagePath,
                                   LargeImagePath = pro.LargeImagePath,
                                   Price = pro.Price,
                                   Quantity = pro.Quantity,
                                   DateCreated = pro.DateCreated,
                                   DateUpdated = pro.DateUpdated,
                                  // IsActive = pro.IsActive,
                                   CategoryName = cat.CategoryName,
                                   FKProductId = pro.FKProductId
                               };
                return products.ToList();
            }
            else
            {
                var products = from pro in shoppingCardDB.Products
                               join cat in shoppingCardDB.Categories
                               on pro.FKCategoryId equals cat.PKCategoryId

                               select new ProductyByCategory
                               {
                                   PKProductId = pro.PKProductId,
                                   ProductName = pro.ProductName,
                                   Description = pro.Description,
                                   ImagePath = pro.ImagePath,
                                   LargeImagePath = pro.LargeImagePath,
                                   Price = pro.Price,
                                   Quantity = pro.Quantity,
                                   DateCreated = pro.DateCreated,
                                   DateUpdated = pro.DateUpdated,
                                   //IsActive = pro.IsActive,
                                   CategoryName = cat.CategoryName,
                                   FKProductId = pro.FKProductId
                               };
                return products.ToList();
            }
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
        ~ProductRepository()
    {
        Dispose(false);
    }
    }
}
