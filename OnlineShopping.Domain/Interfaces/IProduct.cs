using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using OnlineShopping.Domain;
namespace OnlineShopping.Domain.Interfaces
{
    public interface IProduct:IDisposable
    {
        IEnumerable<Product> GetAllProduct();
        void Create(Product product);
        void Update(Product product);
        void Delete(int pkProductId);
        Product GetById(int pkProductId);
        IQueryable<ProductyByCategory> GetAllProductsBasedOnCategory();
        IList<ProductyByCategory> GetAllProductsByFKCatId(int FKCatId);


    }
}
