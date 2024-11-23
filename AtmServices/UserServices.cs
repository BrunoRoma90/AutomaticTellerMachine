using AtmModels;
using AtmRepository;
using AtmRepository.Interfaces;
using AtmServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmServices
{
    public class UserServices : IUserServices
    {
        private IUserRepository _repository = new UserRepository();
        private IAddressServices _addressServices = new AddressServices();


        public User GetUserById(int id)
        {
            DataTable dt = _repository.GetUserById(id);

            if (dt.Rows.Count == 0)
            {
                // Lidar com o caso em que o estudante não é encontrado
                return null;
            }

            DataRow dr = dt.Rows[0];

            Address address = null;
            if (dr["addressId"] != DBNull.Value)
            {
                int addressId = Convert.ToInt32(dr["addressId"]);
                address = _addressServices.GetAddressById(addressId);
            }

            User user = new User
            {
                Id = id,
                FirstName = dr["firstName"].ToString(),
                LastName = dr["lastName"].ToString(),
                Birthdate = Convert.ToDateTime(dr["birthdate"]),
                Address = address,
                Email = dr["email"].ToString(),
                Password = dr["password"].ToString(),
                UserName = dr["username"].ToString(),
                
                
            };
            user.CalculateAge(user.Birthdate);
            return user;
        }



        public List<User> GetAllUsers()
        {
            List<User> lUsers = new List<User>();

            try
            {
                int addressId;

                DataTable dt = new DataTable();
                dt = _repository.GetAllUsers();
                foreach (DataRow dr in dt.Rows)
                {

                    Address address = new Address();
                    if (dr["addressId"] != DBNull.Value)
                    {
                        addressId = Convert.ToInt32(dr["addressId"]);
                        address = _addressServices.GetAddressById(addressId);
                    }




                    User user = new User
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        FirstName = dr["firstName"].ToString(),
                        LastName = dr["lastName"].ToString(),
                        Birthdate = Convert.ToDateTime(dr["birthdate"]),
                        Address = address,
                        Email = dr["email"].ToString(),
                        Password = dr["password"].ToString(),
                        UserName = dr["username"].ToString(),


                    };
                    lUsers.Add(user);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Is not Working");
            }
            return lUsers;

        }


        public Boolean InsertNewUser(User newUser)
        {
            bool inserted = false;

            try
            {
                int addressId = _addressServices.InsertNewAddress(newUser.Address);
                newUser.Address.Id = addressId;
                _repository.InsertNewUser(newUser);
                inserted = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("This is not working");
            }
            return inserted;
        }


        public Boolean UpdateUser(User updatedUser)
        {
            bool updated = false;

            try
            {
                _repository.UpdateUser(updatedUser);
                updated = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return updated;
        }


        public User Login(string username, string password)
        {


            DataTable dt = _repository.GetUserByCredentials(username, password);


            foreach (DataRow row in dt.Rows)
            {
                User authenticatedUser = new User
                {
                    Id = Convert.ToInt32(row["id"]),
                    UserName = row["username"].ToString(),
                };
                return authenticatedUser;
            }
            return null;


        }

    }
}
