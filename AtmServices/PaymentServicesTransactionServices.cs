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
    public class PaymentServicesTransactionServices : IPaymentServicesTransactionServices
    {
        private IPaymentServicesTransactionRepository _repository = new PaymentServicesTransactionRepository();
        private ITransactionServices _transactionServices = new TransactionServices();
        private IBankAccountServices _bankAccountServices = new BankAccountServices();

        public List<PaymentServicesTransaction> GetAllPaymentServicesTransaction()
        {
            List<PaymentServicesTransaction> lPaymentServicesTransactions = new List<PaymentServicesTransaction>();

            try
            {
                int transactionId;

                DataTable dt = new DataTable();
                dt = _repository.GetAllPaymentServicesTransactions();
                foreach (DataRow dr in dt.Rows)
                {

                    Transaction transaction = new Transaction();
                    if (dr["transactionId"] != DBNull.Value)
                    {
                        transactionId = Convert.ToInt32(dr["transactionId"]);
                        transaction = _transactionServices.GetTransactionById(transactionId);
                    }




                    PaymentServicesTransaction paymentServicesTransaction = new PaymentServicesTransaction
                    {
                        PaymentServicesId = Convert.ToInt32(dr["paymentServicesId"]),
                        BankAccount = new BankAccount
                        {
                            Number = transaction.BankAccount.Number,
                            Balance = transaction.BankAccount.Balance,
                            User = transaction.BankAccount.User,
                        },
                        Date = transaction.Date,
                        Amount = Convert.ToDouble(dr["amount"].ToString()),
                        NameService = dr["nameService"].ToString(),
                        Type = transaction.Type,


                    };
                    lPaymentServicesTransactions.Add(paymentServicesTransaction);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Is not Working");
            }
            return lPaymentServicesTransactions;
        }
        public PaymentServicesTransaction GetPaymentServicesTransactionById(int id)
        {
            DataTable dt = _repository.GetPaymentServicesTransactionById(id);

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

            PaymentServicesTransaction paymentServicesTransaction = new PaymentServicesTransaction
            {
                PaymentServicesId = transaction.Id,
                BankAccount = new BankAccount
                {
                    Number = transaction.BankAccount.Number,
                    Balance = transaction.BankAccount.Balance,
                    User = transaction.BankAccount.User,
                },
                Date = transaction.Date,
                Amount = Convert.ToDouble(dr["amount"].ToString()),
                NameService = dr["nameService"].ToString(),
                Type = transaction.Type,

            };

            return paymentServicesTransaction;
        }

        //public List<PaymentServicesTransaction> GetPaymentServicesTransactionByTransactionId(int transactionId)
        //{
        //    List<PaymentServicesTransaction> lPaymentServicesTransactions = new List<PaymentServicesTransaction>();

        //    try
        //    {
        //        DataTable dt = _repository.GetPaymentServicesTransactionByTransactionId(transactionId);
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            int paymentServicesTransactionId = Convert.ToInt32(dr["paymentServicesId"]);
        //            PaymentServicesTransaction paymentServicesTransaction = GetPaymentServicesTransactionById(paymentServicesTransactionId);
        //            if (paymentServicesTransactionId != null)
        //            {
        //                lPaymentServicesTransactions.Add(paymentServicesTransaction);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }


        //    return lPaymentServicesTransactions;
        //}


        public PaymentServicesTransaction GetPaymentServicesTransactionByTransactionId(int transactionId)
        {
            DataTable dt = _repository.GetPaymentServicesTransactionByTransactionId(transactionId);

            if (dt.Rows.Count == 0)
            {
                return null;
            }

            DataRow dr = dt.Rows[0];

            Transaction transaction = _transactionServices.GetTransactionById(transactionId);

            PaymentServicesTransaction paymentServicesTransaction = new PaymentServicesTransaction
            {
                PaymentServicesId = transaction.Id,
                BankAccount = transaction.BankAccount,
                Date = transaction.Date,
                Amount = Convert.ToDouble(dr["amount"]),
                Type = transaction.Type,
                NameService = dr["nameService"].ToString(),
            };

            return paymentServicesTransaction;
        }


        public List<PaymentServicesTransaction> GetPaymentServicesTransactionsByBankAccountId(int bankAccountId)
        {
            List<PaymentServicesTransaction> lPaymentServicesTransaction = new List<PaymentServicesTransaction>();
            List<Transaction> transactions = _transactionServices.GetTransactionByBankAccountId(bankAccountId);

            foreach (Transaction transaction in transactions)
            {
                // Obtenha a transação de depósito específica pelo transactionId
                PaymentServicesTransaction paymentServicesTransaction = GetPaymentServicesTransactionByTransactionId(transaction.Id);
                if (paymentServicesTransaction != null)
                {
                    lPaymentServicesTransaction.Add(paymentServicesTransaction);
                }
            }

            return lPaymentServicesTransaction;
        }

        public Boolean InsertNewPaymentServicesTransaction(PaymentServicesTransaction paymentServicesTransaction)
        {
            bool inserted = false;

            try
            {

                _repository.InsertNewPaymentServicesTransaction(paymentServicesTransaction);
                inserted = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("This is not working");
            }
            return inserted;
        }

        public Boolean PaymentServices(int bankAccountId, double amount, string nameService)
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
                PaymentServicesTransaction paymentServicesTransaction = new PaymentServicesTransaction
                {
                    BankAccount = bankAccount,
                    Date = DateTime.Now,
                    Amount = amount,
                    NameService = nameService

                };

                // Inserir a transação na tabela `Transaction`
                bool transactionInserted = _transactionServices.InsertNewTransaction(paymentServicesTransaction);
                //Ultimo Id transation inserida na tabela Transaction
                int lastTransactionId = _transactionServices.LastTransactionId();
                paymentServicesTransaction.Id = lastTransactionId;
                // Inserir a transação na tabela `PaymentServicesTransaction`
                bool paymentServicesInserted = InsertNewPaymentServicesTransaction(paymentServicesTransaction);

                success = transactionInserted && paymentServicesInserted;
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
