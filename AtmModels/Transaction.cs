using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmModels
{
    public class Transaction
    {
        public int Id { get; set; }
        public BankAccount BankAccount { get; set; }
        public DateTime Date { get; set; }

        public string Type { get; set; }

        public Transaction() { }

        public Transaction(int id, BankAccount bankAccount, DateTime date, string type)
        {
            Id = id;
            BankAccount = bankAccount;
            Date = date;
            Type = type;
        }
    }
}
