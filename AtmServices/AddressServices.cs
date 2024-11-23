using AtmRepository.Interfaces;
using AtmRepository;
using AtmServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtmModels;
using System.Data;

namespace AtmServices
{
    public class AddressServices : IAddressServices
    {
        private IAddressRepository _addressRepository = new AddressRepository();

        public Address GetAddressById(int id)
        {


            DataTable dt = _addressRepository.GetAddressById(id);
            DataRow dr = dt.Rows[0];
           

            Address address = new Address
            {
                Id = id,
                Street = dr["street"].ToString(),
                Number = Convert.ToInt32(dr["number"].ToString()),
                PostalCode = dr["postalCode"].ToString(),
                City = dr["city"].ToString()

            };

            return address;
        }

        public List<Address> GetAllAddresses()
        {
            List<Address> addresses = new List<Address>();

            try
            {
                DataTable dt = _addressRepository.GetAllAddresses();

                foreach (DataRow dr in dt.Rows)
                {
                    Address address = new Address(
                        Convert.ToInt32(dr["id"].ToString()),
                        dr["street"].ToString(),
                        Convert.ToInt32(dr["number"].ToString()),
                        dr["postalCode"].ToString(),
                        dr["city"].ToString()
                    );

                    addresses.Add(address);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Is not Working");
            }

            return addresses;
        }
        public int InsertNewAddress(Address newAddress)
        {
            int addressId = 0;

            try
            {
                addressId = _addressRepository.InsertNewAddress(newAddress);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return addressId;
        }

        public Boolean UpdateAddress(Address updatedAddress)
        {
            bool updated = false;

            try
            {
                _addressRepository.UpdateAddress(updatedAddress);
                updated = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return updated;
        }
    }
}
