using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmModels
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }

        public Address() { }

        public Address(int id, string street, int number, string postalCode, string city)
        {
            Id = id;
            Street = street;
            Number = number;
            PostalCode = postalCode;
            City = city;
        }
    }
    
}
