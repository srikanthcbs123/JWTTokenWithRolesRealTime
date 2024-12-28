using JWTTokenWithRolesRealTime.DTOs;
using JWTTokenWithRolesRealTime.Models;

namespace JWTTokenWithRolesRealTime.Interfaces
{
    public interface IRolesRepository
    {
        Task<UserSignInResponse> RolesCreation(Roles rolesObj);
    }
}
