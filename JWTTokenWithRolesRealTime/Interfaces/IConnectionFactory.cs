using System.Data;

namespace JWTTokenWithRolesRealTime.Interfaces
{
    public interface IConnectionFactory
    {
        IDbConnection GetHotelManagementSqlConnection();
    }
}
