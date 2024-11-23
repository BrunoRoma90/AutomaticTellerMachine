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

namespace AtmServices
{
    public class WithdrawalTransactionServices : IWithdrawalTransactionServices
    {
        private IWithdrawalTransactionRepository _repository = new WithdrawalTransactionRepository();
        private ITransactionServices _transactionServices = new TransactionServices();
        private IBankAccountServices _bankAccountServices = new BankAccountServices();


        public List<WithdrawalTransaction> GetAllWithdrawalTransactions()
        {
            List<WithdrawalTransaction> lWithdrawalTransactions = new List<WithdrawalTransaction>();

            try
            {
                int transactionId;

                DataTable dt = new DataTable();
                dt = _repository.GetAllWithdrawalTransaction();
                foreach (DataRow dr in dt.Rows)
                {

                    Transaction transaction = new Transaction();
                    if (dr["transactionId"] != DBNull.Value)
                    {
                        transactionId = Convert.ToInt32(dr["transactionId"]);
                        transaction = _transactionServices.GetTransactionById(transactionId);
                    }




                    WithdrawalTransaction withdrawalTransaction = new WithdrawalTransaction
                    {
                        WithdrawalId = Convert.ToInt32(dr["withdrawalId"]),
                        BankAccount = new BankAccount
                        {
                            Number = transaction.BankAccount.Number,
                            Balance = transaction.BankAccount.Balance,
                            User = transaction.BankAccount.User,
                        },
                        Date = transaction.Date,
                        Amount = Convert.ToDouble(dr["amount"].ToString()),
                        Type = transaction.Type,


                    };
                    lWithdrawalTransactions.Add(withdrawalTransaction);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Is not Working");
            }
            return lWithdrawalTransactions;

        }

        public WithdrawalTransaction GetWithdrawalTransactionById(int id)
        {
            DataTable dt = _repository.GetWithdrawalTransactionById(id);

            if (dt.Rows.Count == 0)
            {
                // Lidar com o caso em que o estudante não é encontrado
                return null;
            }

            DataRow dr = dt.Rows[0];

            Transaction transaction = null;
            if (dr["transactionId"] != DBNull.Value)
            {
                int transactionId = Convert.ToInt32(dr["transactionId"]);
                transaction = _transactionServices.GetTransactionById(transactionId);
            }

            WithdrawalTransaction withdrawalTransaction = new WithdrawalTransaction
            {
                WithdrawalId = transaction.Id,
                BankAccount = new BankAccount
                {
                    Number = transaction.BankAccount.Number,
                    Balance = transaction.BankAccount.Balance,
                    User = transaction.BankAccount.User,
                },
                Date = transaction.Date,
                Amount = Convert.ToDouble(dr["amount"].ToString()),
                Type = transaction.Type,

            };

            return withdrawalTransaction;
        }

        //public List<WithdrawalTransaction> GetWithdrawalTransactionByTransactionId(int transactionId)
        //{
        //    List<WithdrawalTransaction> lWithdrawalTransaction = new List<WithdrawalTransaction>();

        //    try
        //    {
        //        DataTable dt = _repository.GetWithdrawalTransactionByTransactionId(transactionId);
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            int withdrawalTransactionId = Convert.ToInt32(dr["withdrawalId"]);
        //            WithdrawalTransaction withdrawalTransaction = GetWithdrawalTransactionById(withdrawalTransactionId);
        //            if (withdrawalTransaction != null)
        //            {
        //                lWithdrawalTransaction.Add(withdrawalTransaction);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }


        //    return lWithdrawalTransaction;
        //}

        public WithdrawalTransaction GetWithdrawalTransactionByTransactionId(int transactionId)
        {
            DataTable dt = _repository.GetWithdrawalTransactionByTransactionId(transactionId);

            if (dt.Rows.Count == 0)
            {
                return null;
            }

            DataRow dr = dt.Rows[0];

            Transaction transaction = _transactionServices.GetTransactionById(transactionId);

            WithdrawalTransaction withdrawalTransaction = new WithdrawalTransaction
            {
                WithdrawalId = transaction.Id,
                BankAccount = transaction.BankAccount,
                Date = transaction.Date,
                Amount = Convert.ToDouble(dr["amount"]),
                Type = transaction.Type,
            };

            return withdrawalTransaction;
        }



        public List<WithdrawalTransaction> GetWithdrawalTransactionsByBankAccountId(int bankAccountId)
        {
            List<WithdrawalTransaction> withdrawalTransactions = new List<WithdrawalTransaction>();
            List<Transaction> transactions = _transactionServices.GetTransactionByBankAccountId(bankAccountId);

            foreach (Transaction transaction in transactions)
            {
                
                WithdrawalTransaction withdrawal = GetWithdrawalTransactionByTransactionId(transaction.Id);
                if (withdrawal != null)
                {
                    withdrawalTransactions.Add(withdrawal);
                }
            }

            return withdrawalTransactions;
        }

        public Boolean InsertNewWithdrawalTransaction(WithdrawalTransaction WithdrawalTransaction)
        {
            bool inserted = false;

            try
            {

                _repository.InsertNewWithdrawalTransaction(WithdrawalTransaction);
                inserted = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("This is not working");
            }
            return inserted;
        }

        public Boolean Withdrawal(int bankAccountId, double amount)
        {
            bool success = false;

            try
            {
                // Obter a conta bancária pelo ID
                BankAccount bankAccount = _bankAccountServices.GetBankAccountById(bankAccountId);

                if (bankAccount == null || bankAccount.Balance < amount)
                {
                    Console.WriteLine("Saldo insuficiente ou conta inexistente"); // Saldo insuficiente ou conta inexistente
                    return false;
                }

                // Atualizar o saldo da conta bancária
                bankAccount.Balance -= amount;
                _bankAccountServices.UpdateBalanceBankAccount(bankAccount);

                // Criar a transação de retirada
                WithdrawalTransaction withdrawalTransaction = new WithdrawalTransaction
                {
                    BankAccount = bankAccount,
                    Date = DateTime.Now,
                    Amount = amount
                };

                // Inserir a transação na tabela `Transaction`
                bool transactionInserted = _transactionServices.InsertNewTransaction(withdrawalTransaction);
                //Ultimo Id transation inserida na tabela Transaction
                int lastTransactionId = _transactionServices.LastTransactionId();
                withdrawalTransaction.Id = lastTransactionId;
                // Inserir a transação na tabela `WithdrawalTransaction`
                bool withdrawalInserted = InsertNewWithdrawalTransaction(withdrawalTransaction);

                success = transactionInserted && withdrawalInserted;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("This is not working");
            }

            return success;

        }


    }   
}
