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
    public class BankAccountRepository : IBankAccountRepository
    {
        private static Generic _generic = new Generic();
        private static string _connectionString = _generic.GetConnectionString();
        public DataTable GetAllBankAccounts()
        {
            DataTable dt = new DataTable();


            using (SqlConnection con = new SqlConnection(_connectionString))
            {


                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM BankAccount";

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


        public DataTable GetBankAccountById(int id)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM BankAccount Where id = @id";

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

        public DataTable GetBankAccountByUserId(int userId)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM BankAccount WHERE userId = @userId";

                    cmd.CommandText = query;
                    cmd.Connection = con;
                    cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }

            return dt;
        }


        public void UpdateBalanceBankAccount(BankAccount bankAccount)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "UPDATE BankAccount SET balance = @balance WHERE id = @id";
                    cmd.CommandText = query;
                    cmd.Connection = con;

                    cmd.Parameters.Add("@balance", SqlDbType.Decimal).Value = bankAccount.Balance;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = bankAccount.Id;

                    if (con.State != ConnectionState.Open)
                        con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public DataTable GetBankAccountIdByNumber(string number)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM BankAccount WHERE number = @number";

                    cmd.CommandText = query;
                    cmd.Connection = con;
                    cmd.Parameters.Add("@number", SqlDbType.NVarChar).Value = number;

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
