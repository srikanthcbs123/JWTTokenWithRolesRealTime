using Dapper;
using JWTTokenWithRolesRealTime.DTOs;
using JWTTokenWithRolesRealTime.Interfaces;
using JWTTokenWithRolesRealTime.Utils;
using System.Data;

namespace JWTTokenWithRolesRealTime.Repositories
{
    public class AuthenticateRepository : IAuthenticateRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public AuthenticateRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<UserSignInResponse> UserSignIn(LoginDTO loginDTOObj)
        {
            using (IDbConnection con = _connectionFactory.GetHotelManagementSqlConnection())
            {
                var encryptText = EncryptionLibrary.EncryptText(loginDTOObj.Password);
                var p = new DynamicParameters();
                p.Add("@UserName", loginDTOObj.UserName);
                p.Add("@Password", encryptText);
                //var queryResult = await conn.QueryAsync<Hotel>(StoredProcedureStaticMessages.GetHotelDetails, CommandType.StoredProcedure);
                var result = await con.QueryAsync<UserSignInResponse>(StoredProcedureStatusMessages.SignIn, p, commandType: CommandType.StoredProcedure);
                var status = result.FirstOrDefault();
                return status;
            }
        }
        public async Task<UserRolesInformationResponse> GetUserRolesInformation(LoginDTO loginDTOObj)
        {
            using (IDbConnection con = _connectionFactory.GetHotelManagementSqlConnection())
            {
                var p = new DynamicParameters();
                p.Add("@UserName", loginDTOObj.UserName);
                //var queryResult = await conn.QueryAsync<Hotel>(StoredProcedureStaticMessages.GetHotelDetails, CommandType.StoredProcedure);
                var result = await con.QueryAsync<UserRolesInformationResponse>(StoredProcedureStatusMessages.GetUserRolesInformation, p, commandType: CommandType.StoredProcedure);
                var status = result.FirstOrDefault();
                return status;
            }
        }
    }
}
