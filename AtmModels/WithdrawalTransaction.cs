using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmModels
{
    public class WithdrawalTransaction : Transaction
    {
        public int WithdrawalId { get; set; }
        public double Amount { get; set; }
        public WithdrawalTransaction() 
        {
            Type = "Withdrawal";
        }
        public WithdrawalTransaction(int withdrawalId, double amount, int id, BankAccount bankAccount, DateTime data) 
            :base(id, bankAccount, data, "Withdrawal")
        {
            WithdrawalId = withdrawalId;
            Amount = amount;
        }
    }
}
