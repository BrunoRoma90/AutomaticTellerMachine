using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmModels
{
    public class BankTransferTransaction : Transaction
    {
        public int BankTransferId { get; set; }
        public BankAccount DestinationAccount { get; set; }

        public double Amount { get; set; }

        public BankTransferTransaction()
        {
            Type = "BankTransfer";
        }

        public BankTransferTransaction(int bankTransferId, BankAccount destinationAccount, double amount, int id, BankAccount bankAccount, DateTime data)
            : base(id, bankAccount, data, "BankTransfer")
        {
            BankTransferId = bankTransferId;
            DestinationAccount = destinationAccount;
            Amount = amount;
        }
    }
}
