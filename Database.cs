using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookProject
{
    internal class Database
    {
        private readonly string connectionString = "Data Source=DESKTOP-N7T3J86\\SQLEXPRESS;Integrated Security=True";

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

    }
}
