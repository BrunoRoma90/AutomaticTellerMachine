using AtmRepository.Interfaces;
using AtmRepository;
using AtmServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AtmModels;

namespace AtmServices
{
    public class TransactionServices : ITransactionServices
    {
        private ITransactionRepository _repository = new TransactionRepository();
        private IBankAccountServices _bankAccountServices = new BankAccountServices();
       


        public List<Transaction> GetAllTransactions()
        {
            List<Transaction> ltransactions = new List<Transaction>();

            try
            {
                int bankAccountId;

                DataTable dt = new DataTable();
                dt = _repository.GetAllTransactions();
                foreach (DataRow dr in dt.Rows)
                {

                    BankAccount bankAccount = new BankAccount();
                    if (dr["bankAccountId"] != DBNull.Value)
                    {
                        bankAccountId = Convert.ToInt32(dr["bankAccountId"]);
                        bankAccount = _bankAccountServices.GetBankAccountById(bankAccountId);
                    }




                    Transaction transaction = new Transaction
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Date = Convert.ToDateTime(dr["date"]),
                        BankAccount = bankAccount,
                        Type = dr["type"].ToString(),

                    };
                    ltransactions.Add(transaction);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Is not Working");
            }
            return ltransactions;

        }

        public Transaction GetTransactionById(int id)
        {
            DataTable dt = _repository.GetTransactionById(id);

            if (dt.Rows.Count == 0)
            {
                // Lidar com o caso em que o estudante não é encontrado
                return null;
            }

            DataRow dr = dt.Rows[0];

            BankAccount bankAccount = null;
            if (dr["bankAccountId"] != DBNull.Value)
            {
                int bankAccountId = Convert.ToInt32(dr["bankAccountId"]);
                bankAccount = _bankAccountServices.GetBankAccountById(bankAccountId);
            }

            Transaction transaction = new Transaction
            {
                Id = Convert.ToInt32(dr["id"]),
                Date = Convert.ToDateTime(dr["date"]),
                BankAccount = bankAccount,
                Type = dr["type"].ToString(),

            };

            return transaction;
        }

        public List<Transaction> GetTransactionByBankAccountId(int bankAcountId)
        {
            List<Transaction> lTransactions = new List<Transaction>();

            try
            {
                DataTable dt = _repository.GetTransactionByBankAccountId(bankAcountId);
                foreach (DataRow dr in dt.Rows)
                {
                    int transactionId = Convert.ToInt32(dr["id"]);
                    Transaction transaction = GetTransactionById(transactionId);
                    if (transaction != null)
                    {
                        lTransactions.Add(transaction);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return lTransactions;
        }

        public Boolean InsertNewTransaction(Transaction transaction)
        {
            bool inserted = false;

            try
            {
                
                _repository.InsertNewTransaction(transaction);
                inserted = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("This is not working");
            }
            return inserted;
        }

        public int LastTransactionId()
        {
            return _repository.LastTransactionId();
        }


       
    }

    
}
