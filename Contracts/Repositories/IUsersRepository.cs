using Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repositories
{
    public interface IUsersRepository
    {
        void AddUser(User user);
        void AddUsers(params User[] users);
        void DeleteUser(int id);
        User GetUser(int id);
        IList<User> GetUsers();
        void UpdateUser(User user);
    }
}
