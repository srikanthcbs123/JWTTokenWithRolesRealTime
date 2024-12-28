using JWTTokenWithRolesRealTime.Interfaces;
using Microsoft.Extensions.Options;
using System.Data;
using Microsoft.Data.SqlClient;
namespace JWTTokenWithRolesRealTime.Data
{
    public class ConnectionFactory: IConnectionFactory
    {
        private readonly IConfiguration _config;

        public ConnectionFactory(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection GetHotelManagementSqlConnection()
        {
            IDbConnection _connection = new SqlConnection(Convert.ToString(_config.GetSection("ConnectionStrings:hotelmanagementSqlConnectionString").Value));

            return _connection;
        }
    }
}
