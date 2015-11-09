using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
namespace OnlineShopping.Domain.Interfaces
{
   public interface IOrderDetail:IDisposable
    {
       OrderDetail GetOrderDetailByID(int pkDetailId);
        void Create(OrderDetail order);
        void Update(OrderDetail order);
        void Delete(int pkOrderId);
        IEnumerable<OrderDetail> GetAllOrderDetails();
        IEnumerable<OrderDetail> GetOrderDetailByFKOrderId(int FKOrderId);
    }
}
