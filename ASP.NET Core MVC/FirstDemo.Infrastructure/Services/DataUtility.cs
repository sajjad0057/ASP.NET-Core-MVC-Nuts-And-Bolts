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
        private readonly ITimeService _timeService;

        //// From IConfiguration, can Access  appsettings.json file all info -
        public DataUtility(IConfiguration config,ITimeService timeService)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
            _timeService = timeService;
        }
        public async Task InsertDataAsync()
        {

            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = _connectionString;

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = 
                $"insert into Courses (Id,Title,Fees,ClassStartDate) values('{Guid.NewGuid()}','ADO.NET',2000,'{_timeService.Now.AddDays(30).ToString()}')";

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
                sqlCommand.Dispose();
            }


        }

    }
}
