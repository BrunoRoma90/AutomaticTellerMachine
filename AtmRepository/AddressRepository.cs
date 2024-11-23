using AtmRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using AtmModels;

namespace AtmRepository
{
    public class AddressRepository : IAddressRepository
    {
        private static Generic _generic = new Generic();
        private static string _connectionString = _generic.GetConnectionString();
        public DataTable GetAddressById(int id)
        {
            DataTable dt = new DataTable();



            using (SqlConnection con = new SqlConnection(_connectionString))
            {


                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Address Where id = @addressId";

                    cmd.CommandText = query;
                    cmd.Connection = con;
                    //cmd.Parameters.AddWithValue("@addressId", id);
                    cmd.Parameters.Add("@addressId", SqlDbType.Int).Value = id;

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }

            }
            return dt;
        }

        public DataTable GetAllAddresses()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {


                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT * FROM Address";

                    cmd.CommandText = query;
                    cmd.Connection = con;

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }

            }
            return dt;

        }

        public int InsertNewAddress(Address address)
        {
            int addressId;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "INSERT INTO Address(Street, Number, PostalCode, City) VALUES(@street, @number, @postalCode, @city); SELECT SCOPE_IDENTITY();" + Environment.NewLine; ;

                    cmd.CommandText = query;
                    cmd.Connection = con;

                    #region Insert query values
                    cmd.Parameters.Add("@street", SqlDbType.NVarChar).Value = address.Street;
                    cmd.Parameters.Add("@number", SqlDbType.Int).Value = address.Number;
                    cmd.Parameters.Add("@postalCode", SqlDbType.NVarChar).Value = address.PostalCode;
                    cmd.Parameters.Add("@city", SqlDbType.NVarChar).Value = address.City;
                    #endregion

                    if (con.State != ConnectionState.Open)
                        con.Open();
                    addressId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return addressId;
        }

        public void UpdateAddress(Address address)
        {

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "UPDATE Address SET Street= @street, Number= @number, PostalCode= @postalCode, City= @city WHERE id = @id " + Environment.NewLine;

                    cmd.CommandText = query;
                    cmd.Connection = con;

                    #region Insert query values
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = address.Id;
                    cmd.Parameters.Add("@street", SqlDbType.NVarChar).Value = address.Street;
                    cmd.Parameters.Add("@number", SqlDbType.Int).Value = address.Number;
                    cmd.Parameters.Add("@postalCode", SqlDbType.NVarChar).Value = address.PostalCode;
                    cmd.Parameters.Add("@city", SqlDbType.NVarChar).Value = address.City;
                    #endregion

                    if (con.State != ConnectionState.Open)
                        con.Open();
                    cmd.ExecuteNonQuery();


                }
            }

        }

    }
}
