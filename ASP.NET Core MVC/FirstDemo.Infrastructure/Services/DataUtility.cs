using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
        public async Task InsertDataAsync(string command)
        {

            using SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = _connectionString;

            using SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = command;
                

            try
            {
                if(sqlConnection.State != System.Data.ConnectionState.Open)
                {
                    sqlConnection.Open();   
                }

                int impact = await sqlCommand.ExecuteNonQueryAsync();
            }
            catch(Exception ex)
            {

            }
            finally
            {

            }


        }

    }
}
