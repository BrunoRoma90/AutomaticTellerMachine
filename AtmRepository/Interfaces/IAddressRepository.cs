using AtmModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmRepository.Interfaces
{
    public interface IAddressRepository
    {
        public DataTable GetAddressById(int id);
        public DataTable GetAllAddresses();
        public int InsertNewAddress(Address address);
        public void UpdateAddress(Address address);
    }
}
