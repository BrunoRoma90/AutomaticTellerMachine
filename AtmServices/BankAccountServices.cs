using AtmModels;
using AtmRepository;
using AtmRepository.Interfaces;
using AtmServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AtmServices
{
    public class BankAccountServices : IBankAccountServices
    {

        private IBankAccountRepository _repository = new BankAccountRepository();
        private IUserServices _userServices = new UserServices();
        public List<BankAccount> GetAllBankAccounts()
        {
            List<BankAccount> lBankAccounts = new List<BankAccount>();

            try
            {
                int userId;

                DataTable dt = new DataTable();
                dt = _repository.GetAllBankAccounts();
                foreach (DataRow dr in dt.Rows)
                {

                    User user = new User();
                    if (dr["userId"] != DBNull.Value)
                    {
                        userId = Convert.ToInt32(dr["userId"]);
                        user = _userServices.GetUserById(userId);
                    }




                    BankAccount bankAccount = new BankAccount
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Number = dr["number"].ToString(),
                        Balance = Convert.ToDouble(dr["balance"].ToString()),
                        User = user,
                      
                    };
                    lBankAccounts.Add(bankAccount);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Is not Working");
            }
            return lBankAccounts;

        }

        public BankAccount GetBankAccountById(int id)
        {
            DataTable dt = _repository.GetBankAccountById(id);

            if (dt.Rows.Count == 0)
            {
                // Lidar com o caso em que o estudante não é encontrado
                return null;
            }

            DataRow dr = dt.Rows[0];

            User user = null;
            if (dr["userId"] != DBNull.Value)
            {
                int userId = Convert.ToInt32(dr["userId"]);
                user = _userServices.GetUserById(userId);
            }

            BankAccount bankAccount = new BankAccount
            {
                Id = id,
                Number = dr["number"].ToString(),
                Balance = Convert.ToDouble(dr["balance"].ToString()),
                User = user


            };
            
            return bankAccount;
        }


        public BankAccount GetBankAccountByUserId(int userId)
        {
                 
            DataTable dt = _repository.GetBankAccountByUserId(userId);

            if (dt.Rows.Count == 0)
            {
                return null;
            }

            DataRow dr = dt.Rows[0];

            User user = _userServices.GetUserById(userId);

            BankAccount bankAccount = new BankAccount
            {
                Id = Convert.ToInt32(dr["id"]),
                Number = dr["number"].ToString(),
                Balance = Convert.ToDouble(dr["balance"].ToString()),
                User = user,

            };

            return bankAccount;
        }


        public List<BankAccount> GetBankAccountsByUserId(int userId)
        {
            List<BankAccount> lBankAccounts = new List<BankAccount>();

            try
            {
                DataTable dt = _repository.GetBankAccountByUserId(userId);
                foreach (DataRow dr in dt.Rows)
                {
                    int bankAccountId = Convert.ToInt32(dr["id"]);
                    BankAccount bankAccount = GetBankAccountById(bankAccountId);
                    if (bankAccount != null)
                    {
                        lBankAccounts.Add(bankAccount);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return lBankAccounts;
        }


        public Boolean UpdateBalanceBankAccount(BankAccount bankAccountUpdated)
        {
            bool updated = false;

            try
            {
                _repository.UpdateBalanceBankAccount(bankAccountUpdated);
                updated = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return updated;
        }

        public BankAccount GetBankAccountIdByNumber(string number)
        {
            DataTable dt = _repository.GetBankAccountIdByNumber(number);

            if (dt.Rows.Count == 0)
            {
                // Lidar com o caso em que o estudante não é encontrado
                return null;
            }

            DataRow dr = dt.Rows[0];
            User user = null;
            if (dr["userId"] != DBNull.Value)
            {
                int userId = Convert.ToInt32(dr["userId"]);
                user = _userServices.GetUserById(userId);
            }

            BankAccount bankAccount = new BankAccount
            {
                Id = Convert.ToInt32(dr["id"].ToString()),
                Number = number,
                Balance = Convert.ToDouble(dr["balance"].ToString()),
                User = user,


            };

            return bankAccount;

        }
    }
}
