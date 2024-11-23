using AtmModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmServices.Interfaces
{
    public interface IAddressServices
    {
        public Address GetAddressById(int id);
        public List<Address> GetAllAddresses();
        public int InsertNewAddress(Address newAddress);
        public Boolean UpdateAddress(Address updatedAddress);
    }
}
