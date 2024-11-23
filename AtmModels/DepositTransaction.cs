using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmModels
{
    public class DepositTransaction : Transaction
    {
        public int DepositId { get; set; }
        public double Amount { get; set; }

        public DepositTransaction() 
        {
            Type = "Deposit";
        }

        public DepositTransaction(int depositId, double amount, int id, BankAccount bankAccount, DateTime data) 
            : base(id, bankAccount, data, "Deposit")
        {
            DepositId = depositId;
            Amount = amount;
        }
    }
}
