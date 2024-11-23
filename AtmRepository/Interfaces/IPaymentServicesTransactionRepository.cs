using AtmModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmRepository.Interfaces
{
    public interface IPaymentServicesTransactionRepository
    {
        public DataTable GetAllPaymentServicesTransactions();
        public DataTable GetPaymentServicesTransactionById(int id);

        public DataTable GetPaymentServicesTransactionByTransactionId(int transactionId);

        public void InsertNewPaymentServicesTransaction(PaymentServicesTransaction paymentServicesTransaction);
    }
}
