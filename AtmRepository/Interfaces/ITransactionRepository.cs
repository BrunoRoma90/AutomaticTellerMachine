using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtmModels;

namespace AtmRepository.Interfaces
{
    public interface ITransactionRepository
    {
        public DataTable GetAllTransactions();

        public DataTable GetTransactionById(int id);

        public DataTable GetTransactionByBankAccountId(int bankAcountId);

        public void InsertNewTransaction(Transaction transaction);

        public int LastTransactionId();
    }
}
