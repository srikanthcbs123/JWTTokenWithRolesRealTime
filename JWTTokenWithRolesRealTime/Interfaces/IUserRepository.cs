using JWTTokenWithRolesRealTime.DTOs;
using JWTTokenWithRolesRealTime.Models;

namespace JWTTokenWithRolesRealTime.Interfaces
{
    public interface IUserRepository
    {
        Task<UserSignInResponse> UserResgistration(Users usersObj);
        Task<UserSignInResponse> UserRolesMapping(UserRole userRoleObj);
    }
}
