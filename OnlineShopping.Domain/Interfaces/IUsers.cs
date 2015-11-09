using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace OnlineShopping.Domain.Interfaces
{
    public interface IUsers:IDisposable
    {
        int Create(User user);
        void Update(User user);
        void Delet(int pkUserId);
        IEnumerable<User> GetAllUsers();
        User GetByEmail(string email);
        bool ValidateUser(string userName, string password);
        bool VerifyEmail(string email);
    }
}
