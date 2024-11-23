using AtmModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmRepository.Interfaces
{
    public interface IWithdrawalTransactionRepository
    {
        public DataTable GetAllWithdrawalTransaction();
        public DataTable GetWithdrawalTransactionById(int id);

        public DataTable GetWithdrawalTransactionByTransactionId(int transactionId);

        public void InsertNewWithdrawalTransaction(WithdrawalTransaction withdrawalTransaction);
    }
}
