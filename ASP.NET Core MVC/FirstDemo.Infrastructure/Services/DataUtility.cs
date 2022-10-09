using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Infrastructure.Services
{
    public class DataUtility
    {
        public static void InsertData()
        {

            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = "Server = .\\SQLEXPRESS; Database = Asp.netCoreMvc; User Id = Asp.netCoreMvc; Password = 123456;";


        }
    }
}
