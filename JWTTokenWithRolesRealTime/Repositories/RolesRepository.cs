using Dapper;
using JWTTokenWithRolesRealTime.DTOs;
using JWTTokenWithRolesRealTime.Interfaces;
using JWTTokenWithRolesRealTime.Models;
using JWTTokenWithRolesRealTime.Utils;
using System.Data;

namespace JWTTokenWithRolesRealTime.Repositories
{
    public class RolesRepository:IRolesRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public RolesRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<UserSignInResponse> RolesCreation(Roles rolesObj)
        {
            using (IDbConnection con = _connectionFactory.GetHotelManagementSqlConnection())
            {
                var p = new DynamicParameters();
                p.Add("@RoleName", rolesObj.RoleName);
                p.Add("@IsActive", rolesObj.IsActive);
                var result = await con.QuerySingleAsync<UserSignInResponse>(StoredProcedureStatusMessages.Usp_RolesResgistration, p, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
