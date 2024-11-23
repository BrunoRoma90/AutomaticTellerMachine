using AtmModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmServices.Interfaces
{
    public interface ITransactionServices
    {
        public List<Transaction> GetAllTransactions();

        public Transaction GetTransactionById(int id);
        public List<Transaction> GetTransactionByBankAccountId(int bankAcountId);

        public Boolean InsertNewTransaction(Transaction transaction);

        public int LastTransactionId();

      

    }
}
