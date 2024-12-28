using Dapper;
using JWTTokenWithRolesRealTime.DTOs;
using JWTTokenWithRolesRealTime.Interfaces;
using JWTTokenWithRolesRealTime.Models;
using JWTTokenWithRolesRealTime.Utils;
using System.Data;

namespace JWTTokenWithRolesRealTime.Repositories
{
    public class Userrepository:IUserRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public Userrepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            
        }

        public async Task<UserSignInResponse> UserResgistration(Users usersObj)
        {
            using (IDbConnection con = _connectionFactory.GetHotelManagementSqlConnection())
            {
                var encryptText = EncryptionLibrary.EncryptText(usersObj.Password);
                var p = new DynamicParameters();
                p.Add("@UserName", usersObj.UserName);
                p.Add("@Password", encryptText);
                p.Add("@EmailId", usersObj.EmailId);
                p.Add("@PhoneNumber", usersObj.PhoneNumber);
                p.Add("@Address", usersObj.Address);
                p.Add("@IsActive", usersObj.IsActive);
                var result = await con.QuerySingleAsync<UserSignInResponse>(StoredProcedureStatusMessages.Usp_UserResgistration, p, commandType: CommandType.StoredProcedure);
                return result;
               
            }
        }
        public async  Task<UserSignInResponse> UserRolesMapping(UserRole userRoleObj)
        {
            using (IDbConnection con = _connectionFactory.GetHotelManagementSqlConnection())
            {
                var p = new DynamicParameters();
                p.Add("@RoleId", userRoleObj.RoleId);
                p.Add("@UserId", userRoleObj.UserId);
                var result = await con.QuerySingleAsync<UserSignInResponse>(StoredProcedureStatusMessages.Usp_UserRolesMapping, p, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
