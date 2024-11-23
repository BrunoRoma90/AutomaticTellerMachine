using AtmModels;
using AtmRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmRepository
{
    public class BankTransferTransactionRepository : IBankTransferTransactionRepository
    {
        private static Generic _generic = new Generic();
        private static string _connectionString = _generic.GetConnectionString();
        public DataTable GetAllBankTransferTransaction()
        {

            DataTable dt = new DataTable();


            using (SqlConnection con = new SqlConnection(_connectionString))
            {


                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM BankTransferTransaction";

                    cmd.CommandText = query;
                    cmd.Connection = con;

                    if (con.State != ConnectionState.Open) //Ligação for diferente de aberto Abre
                        con.Open();

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }// Comando/Instrução/Faz isto

            }
            return dt;
        }
        public DataTable GetBankTransferTransactionById(int id)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM BankTransferTransaction Where bankTransferId = @bankTransferId";

                    cmd.CommandText = query;
                    cmd.Connection = con;
                    cmd.Parameters.Add("@bankTransferId", SqlDbType.Int).Value = id;  //Definimos o formato neste caso é um INT

                    if (con.State != ConnectionState.Open) //Ligação for diferente de aberto Abre
                        con.Open();

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }// Comando/Instrução/Faz isto

            }
            return dt;
        }

        public DataTable GetBankTransferTransactionByTransactionId(int transactionId)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM BankTransferTransaction WHERE transactionId = @transactionId";

                    cmd.CommandText = query;
                    cmd.Connection = con;
                    cmd.Parameters.Add("@transactionId", SqlDbType.Int).Value = transactionId;

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }

            return dt;

        }

        public void InsertNewBankTransferTransaction(BankTransferTransaction bankTransferTransaction)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "INSERT INTO BankTransferTransaction(transactionId, destinationAccountId, amount) VALUES(@transactionId, @destinationAccountId, @amount)";

                    cmd.CommandText = query;
                    cmd.Connection = con;

                    #region Insert query values                    
                    cmd.Parameters.Add("@transactionId", SqlDbType.Int).Value = bankTransferTransaction.Id;
                    cmd.Parameters.Add("@destinationAccountId", SqlDbType.Int).Value = bankTransferTransaction.DestinationAccount.Id;
                    cmd.Parameters.Add("@amount", SqlDbType.Decimal).Value = bankTransferTransaction.Amount;

                    #endregion

                    if (con.State != ConnectionState.Open)
                        con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public DataTable GetBankTransferByDestinationAccountId(int destinationAccountId)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM BankTransferTransaction WHERE destinationAccountId = @destinationAccountId";

                    cmd.CommandText = query;
                    cmd.Connection = con;
                    cmd.Parameters.Add("@destinationAccountId", SqlDbType.Int).Value = destinationAccountId;

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }

            return dt;
        }
    }
}
