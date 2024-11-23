using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmRepository
{
    public class Generic
    {
        public string GetConnectionString()
        {

            string connectionString = @"Server=localhost\SQLEXPRESS;Database=AtmProject;Trusted_Connection=True;Integrated Security=True;";
            return connectionString;
        }
    }
}
