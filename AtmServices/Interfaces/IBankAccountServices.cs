using AtmModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmServices.Interfaces
{
    public interface IBankAccountServices
    {
        public List<BankAccount> GetAllBankAccounts();

        public BankAccount GetBankAccountById(int id);
        public List<BankAccount> GetBankAccountsByUserId(int userId);

        public BankAccount GetBankAccountByUserId(int userId);
        public Boolean UpdateBalanceBankAccount(BankAccount bankAccountUpdated);

        public BankAccount GetBankAccountIdByNumber(string number);
    }
}
