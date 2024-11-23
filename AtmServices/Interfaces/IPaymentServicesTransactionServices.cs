using AtmModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmServices.Interfaces
{
    public interface IPaymentServicesTransactionServices
    {
        public List<PaymentServicesTransaction> GetAllPaymentServicesTransaction();
        public PaymentServicesTransaction GetPaymentServicesTransactionById(int id);

        //public List<PaymentServicesTransaction> GetPaymentServicesTransactionByTransactionId(int transactionId);

        public PaymentServicesTransaction GetPaymentServicesTransactionByTransactionId(int transactionId);
        public List<PaymentServicesTransaction> GetPaymentServicesTransactionsByBankAccountId(int bankAccountId);

        public Boolean InsertNewPaymentServicesTransaction(PaymentServicesTransaction paymentServicesTransaction);

        public Boolean PaymentServices(int bankAccountId, double amount, string nameService);
    }

}
