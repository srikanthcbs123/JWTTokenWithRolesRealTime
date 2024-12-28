using JWTTokenWithRolesRealTime.DTOs;
using JWTTokenWithRolesRealTime.Models;

namespace JWTTokenWithRolesRealTime.Interfaces
{
    public interface IUserService
    {
        Task<UserSignInResponse> UserResgistration(UsersDTO usersObj);
        Task<UserSignInResponse> UserRolesMapping(UserRoleDTO userRoleDTOObj);
    }
}
