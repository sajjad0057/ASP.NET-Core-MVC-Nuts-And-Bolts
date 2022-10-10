using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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


        private SqlCommand _PrepareCommand(string sql, Dictionary<string, object> parameters)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = _connectionString;

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = sql;


            if (parameters != null)
            {
                foreach (var item in parameters)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(item.Key, item.Value));
                }
            }


            return sqlCommand;

        }
        public async Task ExecuteCommandAsync(string command,Dictionary<string,object> parameters)
        {

            using SqlCommand sqlCommand = _PrepareCommand(command, parameters);  

            try
            {
                if(sqlCommand.Connection.State != System.Data.ConnectionState.Open)
                {
                    sqlCommand.Connection.Open();   
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


        public void GetDataAsync(string command, Dictionary<string, object> parameters)
        {

        }

    }
}
