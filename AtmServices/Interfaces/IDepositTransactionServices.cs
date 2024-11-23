using AtmModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmServices.Interfaces
{
    public interface IDepositTransactionServices
    {
        public List<DepositTransaction> GetAllDepositTransactions();
        public DepositTransaction GetDepositTransactionById(int id);

        //public List<DepositTransaction> GetDepositTransactionByTransactionId(int transactionId);

        public DepositTransaction GetDepositTransactionByTransactionId(int transactionId);

        public List<DepositTransaction> GetDepositTransactionsByBankAccountId(int bankAccountId);

        public Boolean InsertNewDepositTransaction(DepositTransaction depositTransaction);

        public Boolean Deposit(int bankAccountId, double amount);
    }
}
