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
    public class PaymentServicesTransactionRepository : IPaymentServicesTransactionRepository
    {

        private static Generic _generic = new Generic();
        private static string _connectionString = _generic.GetConnectionString();
        public DataTable GetAllPaymentServicesTransactions()
        {
            DataTable dt = new DataTable();


            using (SqlConnection con = new SqlConnection(_connectionString))
            {


                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM PaymentServicesTransaction";

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

        public DataTable GetPaymentServicesTransactionById(int id)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM PaymentServicesTransaction Where paymentServicesId = @paymentServicesId";

                    cmd.CommandText = query;
                    cmd.Connection = con;
                    cmd.Parameters.Add("@paymentServicesId", SqlDbType.Int).Value = id;  //Definimos o formato neste caso é um INT

                    if (con.State != ConnectionState.Open) //Ligação for diferente de aberto Abre
                        con.Open();

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }// Comando/Instrução/Faz isto

            }
            return dt;
        }

        public DataTable GetPaymentServicesTransactionByTransactionId(int transactionId)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM PaymentServicesTransaction WHERE transactionId = @transactionId";

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

        public void InsertNewPaymentServicesTransaction(PaymentServicesTransaction paymentServicesTransaction)
        {
             using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "INSERT INTO PaymentServicesTransaction(transactionId, nameService, amount) VALUES(@transactionId, @nameService, @amount)";

                    cmd.CommandText = query;
                    cmd.Connection = con;

                    #region Insert query values                    
                    cmd.Parameters.Add("@transactionId", SqlDbType.Int).Value = paymentServicesTransaction.Id;
                    cmd.Parameters.Add("@nameService", SqlDbType.NVarChar).Value = paymentServicesTransaction.NameService;
                    cmd.Parameters.Add("@amount", SqlDbType.Decimal).Value = paymentServicesTransaction.Amount;

                    #endregion

                    if (con.State != ConnectionState.Open)
                        con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
