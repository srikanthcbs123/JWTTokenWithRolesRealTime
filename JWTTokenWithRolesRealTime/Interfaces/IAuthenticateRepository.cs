using JWTTokenWithRolesRealTime.DTOs;

namespace JWTTokenWithRolesRealTime.Interfaces
{
    public interface IAuthenticateRepository
    {
        Task<UserSignInResponse> UserSignIn(LoginDTO loginDTOObj);
        Task<UserRolesInformationResponse> GetUserRolesInformation(LoginDTO loginDTOObj);
    }
}
