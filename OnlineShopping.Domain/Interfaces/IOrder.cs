using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace OnlineShopping.Domain.Interfaces
{
    public interface IOrder:IDisposable
    {
        int Create(Order order);
        void Update(Order order);
        void Delete(int pkOrderId);
        IEnumerable<Order> GetAllOrders();
        IEnumerable<Order> GetOrdersByFKUserId(int FKUserId);
    }
}
