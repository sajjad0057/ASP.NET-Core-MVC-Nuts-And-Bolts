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
        public async Task ExecuteCommandAsync(string command,Dictionary<string,object> parameters)
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

                if(parameters != null)
                {
                    foreach(var item in parameters)
                    {
                        sqlCommand.Parameters.Add(new SqlParameter(item.Key,item.Value));
                    }
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
