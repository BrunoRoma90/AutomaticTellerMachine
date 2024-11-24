using AtmModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmServices.Interfaces
{
    public interface IUserServices
    {
        public List<User> GetAllUsers();
        public User GetUserById(int id);
        public Boolean InsertNewUser(User newUser);
        public Boolean UpdateUser(User updatedUser);
        public User Login(string username, string password);

        public Boolean UpdateUserPassword(int id, string password);
    }
}
