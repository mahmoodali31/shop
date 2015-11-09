using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
namespace OnlineShopping.Domain.Interfaces
{
    public interface ICategory :IDisposable
    {
        IEnumerable<Category> GetAllCategory();
        void Create(Category category);
        void Update(Category category);
        void Delete(int pkCategoryId);
        Category GetById(int pkCategoryId);
    }
    
}
