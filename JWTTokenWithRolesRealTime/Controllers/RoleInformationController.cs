using JWTTokenWithRolesRealTime.DTOs;
using JWTTokenWithRolesRealTime.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTTokenWithRolesRealTime.Controllers
{
    [Authorize]
  // [Authorize(Roles = "user")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleInformationController : ControllerBase
    {
        private readonly IRolesService _rolesService;
        public RoleInformationController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }
        //[HttpGet]
        //[Route("sampledata")]
        //public async Task<IActionResult> Sample1([FromQuery] RolesDTO rolesDTOObj)
        //{
        //    return Ok("hello");
        //}
        [HttpPost]
        [Route("RolesCreation")]
        public async Task<IActionResult> RolesCreation([FromBody] RolesDTO rolesDTOObj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var res = await _rolesService.RolesCreation(rolesDTOObj);
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
