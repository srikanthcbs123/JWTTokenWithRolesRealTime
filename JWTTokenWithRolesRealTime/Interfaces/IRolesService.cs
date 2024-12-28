using JWTTokenWithRolesRealTime.DTOs;
using JWTTokenWithRolesRealTime.Models;

namespace JWTTokenWithRolesRealTime.Interfaces
{
    public interface IRolesService
    {
        Task<UserSignInResponse> RolesCreation(RolesDTO rolesObj);
    }
}
