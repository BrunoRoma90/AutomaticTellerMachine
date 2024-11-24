using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using AtmRepository.Interfaces;
using System.Data;
using AtmModels;

namespace AtmRepository
{
    public class UserRepository : IUserRepository
    {
        private static Generic _generic = new Generic();
        private static string _connectionString = _generic.GetConnectionString();
        public DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();

            
            using (SqlConnection con = new SqlConnection(_connectionString))
            {


                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Users";

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


        public DataTable GetUserById(int id)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Users Where id = @id";

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


        public void InsertNewUser(User user)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "INSERT INTO Users(firstName, lastName, birthdate, addressId, email, password, username) VALUES(@firstName, @lastName, @birthdate, @addressId, @email, @password, @username)";

                    cmd.CommandText = query;
                    cmd.Connection = con;

                    #region Insert query values
                    cmd.Parameters.Add("@firstName", SqlDbType.NVarChar).Value = user.FirstName;
                    cmd.Parameters.Add("@lastName", SqlDbType.NVarChar).Value = user.LastName;
                    cmd.Parameters.Add("@birthdate", SqlDbType.DateTime2).Value = user.Birthdate;                                       
                    cmd.Parameters.Add("@addressId", SqlDbType.Int).Value = user.Address.Id;
                    cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = user.Email;
                    cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = user.Password;
                    cmd.Parameters.Add("@username", SqlDbType.NVarChar).Value = user.UserName;

                    #endregion

                    if (con.State != ConnectionState.Open)
                        con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void UpdateUser(User user)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "UPDATE Users SET firstName= @firstName, lastName= @lastName, birthdate= @birthdate, addressId= @addressId, email = @email, password= @password, username= @username WHERE id= @id ";

                    cmd.CommandText = query;
                    cmd.Connection = con;

                    #region Insert query values
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = user.Id;
                    cmd.Parameters.Add("@firstName", SqlDbType.NVarChar).Value = user.FirstName;
                    cmd.Parameters.Add("@lastName", SqlDbType.NVarChar).Value = user.LastName;
                    cmd.Parameters.Add("@birthdate", SqlDbType.DateTime2).Value = user.Birthdate;
                    cmd.Parameters.Add("@addressId", SqlDbType.Int).Value = user.Address.Id;
                    cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = user.Email;
                    cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = user.Password;
                    cmd.Parameters.Add("@username", SqlDbType.NVarChar).Value = user.UserName;
                    #endregion

                    if (con.State != ConnectionState.Open)
                        con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

        }


        public DataTable GetUserByCredentials(string username, string password)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Users WHERE username = @username AND password = @password";

                    cmd.CommandText = query;
                    cmd.Connection = con;
                    cmd.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                    cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);



                }
            }
            return dt;


        }


        public void UpdateUserPassword(int id, string password)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "UPDATE Users SET password= @password WHERE id = @id " + Environment.NewLine;

                    cmd.CommandText = query;
                    cmd.Connection = con;

                    #region Insert query values
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;
                    #endregion

                    if (con.State != ConnectionState.Open)
                        con.Open();
                    cmd.ExecuteNonQuery();


                }
            }
        }
    }
}
