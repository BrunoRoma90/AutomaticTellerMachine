using AtmModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmServices.Interfaces
{
    public interface IBankTransferTransactionServices
    {
        public List<BankTransferTransaction> GetAllBankTransferTransactions();
        public BankTransferTransaction GetBankTransferTransactionById(int id);

        //public List<BankTransferTransaction> GetBankTransferTransactionByTransactionId(int transactionId);

        public BankTransferTransaction GetBankTransferTransactionByTransactionId(int transactionId);


        public List<BankTransferTransaction> GetBankTransferTransactionByBankAccountId(int bankAccountId);

        public Boolean InsertBankTransferTransaction(BankTransferTransaction bankTransferTransaction);

        public Boolean BankTransfer(int bankAccountId, double amount, int destinationAccountId);

        public List<BankTransferTransaction> GetBankTransferTransactionByDestinationAccountId(int destinationAccountId);
    }
}
