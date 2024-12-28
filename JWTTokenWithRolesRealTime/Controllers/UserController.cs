using JWTTokenWithRolesRealTime.DTOs;
using JWTTokenWithRolesRealTime.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTTokenWithRolesRealTime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("UserRegistartion")]
        public async Task<IActionResult> UserRegistartion([FromBody] UsersDTO usersDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var res = await _userService.UserResgistration(usersDTO);
                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("UserRolesMapping")]
        public async Task<IActionResult> UserRolesMapping([FromBody]UserRoleDTO userRoleDTOObj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var res = await _userService.UserRolesMapping(userRoleDTOObj);
                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
