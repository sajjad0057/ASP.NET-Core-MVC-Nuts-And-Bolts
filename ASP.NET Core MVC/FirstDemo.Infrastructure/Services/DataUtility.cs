using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Infrastructure.Services
{
    public class DataUtility : IDataUtility
    {

        private readonly string _connectionString;

        //// From IConfiguration, can Access  appsettings.json file all info -
        public DataUtility(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }
        public void InsertData()
        {

            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = _connectionString;


        }

    }
}
