using JWTTokenWithRolesRealTime.DTOs;

namespace JWTTokenWithRolesRealTime.Interfaces
{
    public interface IAuthenticateService
    {
        Task<UserSignInResponse> UserSignIn(LoginDTO loginDTOObj);
        Task<UserRolesInformationResponse> GetUserRolesInformation(LoginDTO loginDTOObj);
    }
}
