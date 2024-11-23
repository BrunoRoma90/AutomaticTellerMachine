using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmModels
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; } 
        public Address Address { get; set; }
        public int Age { get; set; } 
        public string Email { get; set; }
        public string Password { get; set; }

        public string UserName { get; set; }

        public User() { }

        public User(int id, string firstName, string lastName, DateTime birthdate, Address address, string email, string password, string username)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Birthdate = birthdate;
            Address = address;
            Email = email;
            Password = password;
            UserName = username;
            CalculateAge(birthdate);
        }

        public void CalculateAge(DateTime birthdate)
        {
            Age = DateTime.Now.Year - birthdate.Year;
            if (DateTime.Now.DayOfYear < birthdate.DayOfYear)
            {
                Age--;
            }
        }
    }
}
