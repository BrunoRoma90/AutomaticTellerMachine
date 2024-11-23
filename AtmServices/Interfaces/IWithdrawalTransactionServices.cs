using AtmModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmServices.Interfaces
{
    public interface IWithdrawalTransactionServices
    {
        public List<WithdrawalTransaction> GetAllWithdrawalTransactions();
        public WithdrawalTransaction GetWithdrawalTransactionById(int id);

        //public List<WithdrawalTransaction> GetWithdrawalTransactionByTransactionId(int transactionId);
        public WithdrawalTransaction GetWithdrawalTransactionByTransactionId(int transactionId);
        public List<WithdrawalTransaction> GetWithdrawalTransactionsByBankAccountId(int bankAccountId);
        public Boolean InsertNewWithdrawalTransaction(WithdrawalTransaction withdrawalTransaction);

        public Boolean Withdrawal(int bankAccountId, double amount);
    }
}
