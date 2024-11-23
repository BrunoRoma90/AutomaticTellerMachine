using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtmRepository.Interfaces;
using AtmModels;

namespace AtmRepository
{
    public class TransactionRepository : ITransactionRepository
    {
        private static Generic _generic = new Generic();
        private static string _connectionString = _generic.GetConnectionString();

        public DataTable GetAllTransactions()
        {
            DataTable dt = new DataTable();


            using (SqlConnection con = new SqlConnection(_connectionString))
            {


                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Transactions";

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

        public DataTable GetTransactionById(int id)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Transactions Where id = @id";

                    cmd.CommandText = query;
                    cmd.Connection = con;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;  //Definimos o formato neste caso é um INT

                    if (con.State != ConnectionState.Open) //Ligação for diferente de aberto Abre
                        con.Open();

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }// Comando/Instrução/Faz isto

            }
            return dt;
        }

        public DataTable GetTransactionByBankAccountId(int bankAccountId)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Transactions WHERE bankAccountId = @bankAccountId";

                    cmd.CommandText = query;
                    cmd.Connection = con;
                    cmd.Parameters.Add("@bankAccountId", SqlDbType.Int).Value = bankAccountId;

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public void InsertNewTransaction(Transaction transaction)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "INSERT INTO Transactions(bankAccountId, date, type) VALUES(@bankAccountId, @date, @type)";

                    cmd.CommandText = query;
                    cmd.Connection = con;

                    #region Insert query values                    
                    cmd.Parameters.Add("@bankAccountId", SqlDbType.Int).Value = transaction.BankAccount.Id;
                    cmd.Parameters.Add("@date", SqlDbType.DateTime2).Value = transaction.Date;
                    cmd.Parameters.Add("@type", SqlDbType.NVarChar).Value = transaction.Type;
                    #endregion

                    if (con.State != ConnectionState.Open)
                        con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

        }

        public int LastTransactionId()
        {
            int transactionId = 0;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT IDENT_CURRENT('Transactions');";

                    cmd.CommandText = query;
                    cmd.Connection = con;

                    if (con.State != ConnectionState.Open)
                        con.Open();
                    transactionId = Convert.ToInt32(cmd.ExecuteScalar());
                }
                return transactionId;
            }

        }

    }
}
