using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmModels
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string Number { get; set; }

        public double Balance { get; set; }
        public User User { get; set; }
        public List<Transaction> Transactions { get; set; }

        public BankAccount() { }

        public BankAccount(int id, string number, double balance, User user, List<Transaction> transactions)
        {
            Id = id;
            Number = number;
            Balance = balance;
            User = user;
            Transactions = transactions;    
        }
    }
}
