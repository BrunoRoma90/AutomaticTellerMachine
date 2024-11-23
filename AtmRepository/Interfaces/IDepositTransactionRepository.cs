using AtmModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmRepository.Interfaces
{
    public interface IDepositTransactionRepository
    {
        public DataTable GetAllDepositTransaction();
        public DataTable GetDepositTransactionById(int id);

        public DataTable GetDepositTransactionByTransactionId(int transactionId);

        public void InsertNewDepositTransaction(DepositTransaction depositTransaction);

        
    }
}
