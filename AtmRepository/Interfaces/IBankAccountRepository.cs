using AtmModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmRepository.Interfaces
{
    public interface IBankAccountRepository
    {
        public DataTable GetAllBankAccounts();

        public DataTable GetBankAccountById(int id);
        public DataTable GetBankAccountByUserId(int userId);

        public void UpdateBalanceBankAccount(BankAccount bankAccount);

        public DataTable GetBankAccountIdByNumber(string number);
    }
}
