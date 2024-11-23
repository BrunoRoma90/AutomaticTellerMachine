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
    public class WithdrawalTransactionRepository : IWithdrawalTransactionRepository
    {
        private static Generic _generic = new Generic();
        private static string _connectionString = _generic.GetConnectionString();
        public DataTable GetAllWithdrawalTransaction()
        {
            DataTable dt = new DataTable();


            using (SqlConnection con = new SqlConnection(_connectionString))
            {


                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM WithdrawalTransaction";

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

        public DataTable GetWithdrawalTransactionById(int id)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM WithdrawalTransaction Where withdrawalId = @withdrawalId";

                    cmd.CommandText = query;
                    cmd.Connection = con;
                    cmd.Parameters.Add("@withdrawalId", SqlDbType.Int).Value = id;  //Definimos o formato neste caso é um INT

                    if (con.State != ConnectionState.Open) //Ligação for diferente de aberto Abre
                        con.Open();

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }// Comando/Instrução/Faz isto

            }
            return dt;
        }

        public DataTable GetWithdrawalTransactionByTransactionId(int transactionId)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM WithdrawalTransaction WHERE transactionId = @transactionId";

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


        public void InsertNewWithdrawalTransaction(WithdrawalTransaction withdrawalTransaction)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "INSERT INTO WithdrawalTransaction(transactionId, amount) VALUES(@transactionId, @amount)";

                    cmd.CommandText = query;
                    cmd.Connection = con;

                    #region Insert query values                    
                    cmd.Parameters.Add("@transactionId", SqlDbType.Int).Value = withdrawalTransaction.Id;
                    cmd.Parameters.Add("@amount", SqlDbType.Decimal).Value = withdrawalTransaction.Amount;
                    
                    #endregion

                    if (con.State != ConnectionState.Open)
                        con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

        }
    }



    
}
