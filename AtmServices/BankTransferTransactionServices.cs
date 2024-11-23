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
using System.Security.Principal;

namespace AtmServices
{
    public class BankTransferTransactionServices : IBankTransferTransactionServices
    {
        private IBankTransferTransactionRepository _repository = new BankTransferTransactionRepository();
        private ITransactionServices _transactionServices = new TransactionServices();
        private IBankAccountServices _bankAccountServices = new BankAccountServices();
        public List<BankTransferTransaction> GetAllBankTransferTransactions()
        {
            List<BankTransferTransaction> lBankTransferTransactions = new List<BankTransferTransaction>();

            try
            {
                int transactionId;
                int destinationAccountId;
                DataTable dt = new DataTable();
                dt = _repository.GetAllBankTransferTransaction();
                foreach (DataRow dr in dt.Rows)
                {

                    Transaction transaction = new Transaction();
                    if (dr["transactionId"] != DBNull.Value)
                    {
                        transactionId = Convert.ToInt32(dr["transactionId"]);
                        transaction = _transactionServices.GetTransactionById(transactionId);
                    }

                    BankAccount destinationAccount = new BankAccount();
                    if (dr["destinationAccountId"] != DBNull.Value)
                    {
                        destinationAccountId = Convert.ToInt32(dr["destinationAccountId"]);
                        destinationAccount = _bankAccountServices.GetBankAccountById(destinationAccountId);
                    }



                    BankTransferTransaction bankTransferTransaction = new BankTransferTransaction
                    {
                        BankTransferId = Convert.ToInt32(dr["bankTransferId"]),
                        BankAccount = new BankAccount
                        {
                            Number = transaction.BankAccount.Number,
                            Balance = transaction.BankAccount.Balance,
                            User = transaction.BankAccount.User,
                        },
                        DestinationAccount = destinationAccount,
                        Date = transaction.Date,
                        Amount = Convert.ToDouble(dr["amount"].ToString()),
                        Type = transaction.Type,


                    };
                    lBankTransferTransactions.Add(bankTransferTransaction);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Is not Working");
            }
            return lBankTransferTransactions;

        }
        public BankTransferTransaction GetBankTransferTransactionById(int id)
        {
            DataTable dt = _repository.GetBankTransferTransactionById(id);

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

            BankAccount destinationAccount = null;
            if (dr["destinationAccountId"] != DBNull.Value)
            {
                int destinationAccountId = Convert.ToInt32(dr["destinationAccountId"]);
                destinationAccount = _bankAccountServices.GetBankAccountById(destinationAccountId);
            }

            BankTransferTransaction bankTransferTransaction = new BankTransferTransaction
            {
                BankTransferId = transaction.Id,
                BankAccount = new BankAccount
                {
                    Number = transaction.BankAccount.Number,
                    Balance = transaction.BankAccount.Balance,
                    User = transaction.BankAccount.User,
                },
                DestinationAccount = destinationAccount,
                Date = transaction.Date,
                Amount = Convert.ToDouble(dr["amount"].ToString()),
                Type = transaction.Type,

            };

            return bankTransferTransaction;
        }

        //public List<BankTransferTransaction> GetBankTransferTransactionByTransactionId(int transactionId)
        //{
        //    List<BankTransferTransaction> lBankTransferTransactions = new List<BankTransferTransaction>();

        //    try
        //    {
        //        DataTable dt = _repository.GetBankTransferTransactionByTransactionId(transactionId);
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            int bankTransferTransactionId = Convert.ToInt32(dr["bankTransferId"]);
        //            BankTransferTransaction bankTransferTransaction = GetBankTransferTransactionById(bankTransferTransactionId);
        //            if (bankTransferTransactionId != null)
        //            {
        //                lBankTransferTransactions.Add(bankTransferTransaction);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }


        //    return lBankTransferTransactions;
        //}

        public BankTransferTransaction GetBankTransferTransactionByTransactionId(int transactionId)
        {
            DataTable dt = _repository.GetBankTransferTransactionByTransactionId(transactionId);

            if (dt.Rows.Count == 0)
            {
                return null;
            }

            DataRow dr = dt.Rows[0];

            Transaction transaction = _transactionServices.GetTransactionById(transactionId);
            BankAccount destinationAccount = _bankAccountServices.GetBankAccountById(Convert.ToInt32(dr["destinationAccountId"]));

            BankTransferTransaction bankTransferTransaction = new BankTransferTransaction
            {
                BankTransferId = transaction.Id,
                BankAccount = transaction.BankAccount,
                Date = transaction.Date,
                DestinationAccount = destinationAccount,
                Amount = Convert.ToDouble(dr["amount"]),
                Type = transaction.Type,

            };

            return bankTransferTransaction;
        }

        public List<BankTransferTransaction> GetBankTransferTransactionByBankAccountId(int bankAccountId)
        {
            List<BankTransferTransaction> bankTransferTransactions = new List<BankTransferTransaction>();
            List<Transaction> transactions = _transactionServices.GetTransactionByBankAccountId(bankAccountId);

            foreach (Transaction transaction in transactions)
            {
                BankTransferTransaction bankTransfer = GetBankTransferTransactionByTransactionId(transaction.Id);
                if (bankTransfer != null)
                {
                    bankTransferTransactions.Add(bankTransfer);
                }
            }

            return bankTransferTransactions;
        }
        public Boolean InsertBankTransferTransaction(BankTransferTransaction bankTransferTransaction)
        {
            bool inserted = false;

            try
            {

                _repository.InsertNewBankTransferTransaction(bankTransferTransaction);
                inserted = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("This is not working");
            }
            return inserted;

        }

        public Boolean BankTransfer(int bankAccountId, double amount, int destinationAccountId)
        {
            bool success = false;

            try
            {
                // Obter a conta bancária de origem pelo ID
                BankAccount bankAccount = _bankAccountServices.GetBankAccountById(bankAccountId);

                // Obter a conta bancária de destino pelo ID
                BankAccount destinationBankAccount = _bankAccountServices.GetBankAccountById(destinationAccountId);

                if (bankAccount == null || destinationBankAccount == null || bankAccount.Balance < amount)
                {
                    Console.WriteLine("Saldo insuficiente ou conta inexistente"); // Saldo insuficiente ou conta inexistente
                    return false;
                }

                // Atualizar o saldo das contas bancárias
                bankAccount.Balance -= amount;
                destinationBankAccount.Balance += amount;
                _bankAccountServices.UpdateBalanceBankAccount(bankAccount);
                _bankAccountServices.UpdateBalanceBankAccount(destinationBankAccount);

                // Criar a transação de transferência bancária
                BankTransferTransaction bankTransfer = new BankTransferTransaction
                {
                    BankAccount = bankAccount,
                    DestinationAccount = destinationBankAccount,
                    Date = DateTime.Now,
                    Amount = amount
                };

                // Inserir a transação na tabela `Transaction`
                bool transactionInserted = _transactionServices.InsertNewTransaction(bankTransfer);
                //Ultimo Id transation inserida na tabela Transaction
                int lastTransactionId = _transactionServices.LastTransactionId();
                bankTransfer.Id = lastTransactionId;
                // Inserir a transação na tabela `WithdrawalTransaction`
                bool bankTransferInserted = InsertBankTransferTransaction(bankTransfer);



                success = transactionInserted && bankTransferInserted;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("This is not working");
            }

            return success;
        }

        public List<BankTransferTransaction> GetBankTransferTransactionByDestinationAccountId(int destinationAccountId)
        {
            List<BankTransferTransaction> bankTransferTransactions = new List<BankTransferTransaction>();

            try
            {
                DataTable dt = _repository.GetBankTransferByDestinationAccountId(destinationAccountId);

                foreach (DataRow dr in dt.Rows)
                {
                   
                    Transaction transaction = null;
                    if (dr["transactionId"] != DBNull.Value)
                    {
                        int transactionId = Convert.ToInt32(dr["transactionId"]);
                        transaction = _transactionServices.GetTransactionById(transactionId);
                    }

                    if (transaction == null) continue;

                   
                    BankAccount destinationAccount = _bankAccountServices.GetBankAccountById(destinationAccountId);

                    BankTransferTransaction bankTransferTransaction = new BankTransferTransaction
                    {
                        BankTransferId = Convert.ToInt32(dr["bankTransferId"]),
                        BankAccount = transaction.BankAccount,
                        DestinationAccount = destinationAccount,
                        Date = transaction.Date,
                        Amount = Convert.ToDouble(dr["amount"]),
                        Type = transaction.Type
                    };

                    bankTransferTransactions.Add(bankTransferTransaction);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Is not Working");
            }

            return bankTransferTransactions;
        }
    }
}
