using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmModels
{
    public class PaymentServicesTransaction : Transaction
    {
        public int PaymentServicesId { get; set; }

        public string NameService { get; set; }

        public double Amount { get; set; }
        
        public PaymentServicesTransaction() 
        {
            Type = "Services Payment";
        }

        public PaymentServicesTransaction(int paymentServicesId, string nameService, double amount, int id, BankAccount bankAccount, DateTime data) 
            :base(id, bankAccount, data, "Services Payment")
        {
            PaymentServicesId = paymentServicesId;
            NameService = nameService;
            Amount = amount;
        }
    }
}
