using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace OnlineShopping.Domain.Interfaces
{
   public  interface IShippingAddress:IDisposable
    {
      // IEnumerable<UserAddress> GetAllAddress();
       UserAddress GetAddressByPkId(int pkAddId);
       void Create(UserAddress userAddress);
       void Update(UserAddress userAddress);
       void Delete(int PkAddId);
       UserAddress GetAddressByUserId(int userId);
    }
}
