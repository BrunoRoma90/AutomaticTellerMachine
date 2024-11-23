using AtmModels;
using AtmRepository.Interfaces;
using AtmRepository;
using AtmServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AtmServices
{
    public class DepositTransactionServices : IDepositTransactionServices
    {
        private IDepositTransactionRepository _repository = new DepositTransactionRepository();
        private ITransactionServices _transactionServices = new TransactionServices();
        private IBankAccountServices _bankAccountServices = new BankAccountServices();
        public List<DepositTransaction> GetAllDepositTransactions()
        {
            List<DepositTransaction> lDepositTransactions = new List<DepositTransaction>();

            try
            {
                int transactionId;

                DataTable dt = new DataTable();
                dt = _repository.GetAllDepositTransaction();
                foreach (DataRow dr in dt.Rows)
                {

                    Transaction transaction = new Transaction();
                    if (dr["transactionId"] != DBNull.Value)
                    {
                        transactionId = Convert.ToInt32(dr["transactionId"]);
                        transaction = _transactionServices.GetTransactionById(transactionId);
                    }




                    DepositTransaction depositTransaction = new DepositTransaction
                    {
                        DepositId = Convert.ToInt32(dr["depositId"]),
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
                    lDepositTransactions.Add(depositTransaction);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Is not Working");
            }
            return lDepositTransactions;

        }


        public DepositTransaction GetDepositTransactionById(int id)
        {
            DataTable dt = _repository.GetDepositTransactionById(id);

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

            DepositTransaction depositTransaction = new DepositTransaction
            {
                DepositId = transaction.Id,
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

            return depositTransaction;
        }

        //public List<DepositTransaction> GetDepositTransactionByTransactionId(int transactionId)
        //{
        //    List<DepositTransaction> lDepositTransactions = new List<DepositTransaction>();

        //    try
        //    {
        //        DataTable dt = _repository.GetDepositTransactionByTransactionId(transactionId);
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            int depositTransactionId = Convert.ToInt32(dr["depositId"]);
        //            DepositTransaction depositTransaction = GetDepositTransactionById(depositTransactionId);
        //            if (depositTransactionId != null)
        //            {
        //                lDepositTransactions.Add(depositTransaction);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }


        //    return lDepositTransactions;
        //}


        public DepositTransaction GetDepositTransactionByTransactionId(int transactionId)
        {
            DataTable dt = _repository.GetDepositTransactionByTransactionId(transactionId);

            if (dt.Rows.Count == 0)
            {
                return null;
            }

            DataRow dr = dt.Rows[0];

            Transaction transaction = _transactionServices.GetTransactionById(transactionId);

            DepositTransaction depositTransaction = new DepositTransaction
            {
                DepositId = transaction.Id,
                BankAccount = transaction.BankAccount,
                Date = transaction.Date,
                Amount = Convert.ToDouble(dr["amount"]),
                Type = transaction.Type,
            };

            return depositTransaction;
        }

        public List<DepositTransaction> GetDepositTransactionsByBankAccountId(int bankAccountId)
        {
            List<DepositTransaction> depositTransactions = new List<DepositTransaction>();
            List<Transaction> transactions = _transactionServices.GetTransactionByBankAccountId(bankAccountId);

            foreach (Transaction transaction in transactions)
            {
                DepositTransaction deposit = GetDepositTransactionByTransactionId(transaction.Id);
                if (deposit != null)
                {
                    depositTransactions.Add(deposit);
                }

            }
            return depositTransactions;
        }


        public Boolean InsertNewDepositTransaction(DepositTransaction depositTransaction)
        {
            bool inserted = false;

            try
            {

                _repository.InsertNewDepositTransaction(depositTransaction);
                inserted = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("This is not working");
            }
            return inserted;
        }

        public Boolean Deposit(int bankAccountId, double amount)
        {
            bool success = false;

            try
            {
                // Obter a conta bancária pelo ID
                BankAccount bankAccount = _bankAccountServices.GetBankAccountById(bankAccountId);

                if (bankAccount == null)
                {
                    Console.WriteLine("Conta inexistente"); // Saldo insuficiente ou conta inexistente
                    return false;
                }

                // Atualizar o saldo da conta bancária
                bankAccount.Balance += amount;
                _bankAccountServices.UpdateBalanceBankAccount(bankAccount);

                // Criar a transação do levantamento
                DepositTransaction depositTransaction = new DepositTransaction
                {
                    BankAccount = bankAccount,
                    Date = DateTime.Now,
                    Amount = amount
                };

                // Inserir a transacção na tabela `Transaction`
                bool transactionInserted = _transactionServices.InsertNewTransaction(depositTransaction);
                //Ultimo Id transation inserida na tabela Transaction
                int lastTransactionId = _transactionServices.LastTransactionId();
                depositTransaction.Id = lastTransactionId;
                // Inserir a transacção na tabela `DepositTransaction`
                bool depositInserted = InsertNewDepositTransaction(depositTransaction);

                success = transactionInserted && depositInserted;
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
