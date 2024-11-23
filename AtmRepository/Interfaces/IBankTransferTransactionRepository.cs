using AtmModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmRepository.Interfaces
{
    public interface IBankTransferTransactionRepository
    {
        public DataTable GetAllBankTransferTransaction();
        public DataTable GetBankTransferTransactionById(int id);

        public DataTable GetBankTransferTransactionByTransactionId(int transactionId);

        public void InsertNewBankTransferTransaction(BankTransferTransaction bankTransferTransaction);

        public DataTable GetBankTransferByDestinationAccountId(int destinationAccountId);
    }
}
