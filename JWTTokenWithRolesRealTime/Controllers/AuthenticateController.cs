using JWTTokenWithRolesRealTime.DTOs;
using JWTTokenWithRolesRealTime.Interfaces;
using JWTTokenWithRolesRealTime.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace JWTTokenWithRolesRealTime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly IConfiguration _configuration;
        public AuthenticateController(IAuthenticateService authenticateService, IConfiguration configuration)
        {
            _authenticateService = authenticateService;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("UserSignIn")]
        public async Task<IActionResult> UserSignIn([FromBody] LoginDTO loginDTOObj)
        {
            try
            {
                UserLoginResponse loginResponseObj = new UserLoginResponse();
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var res = await _authenticateService.UserSignIn(loginDTOObj);
                    if(res.StatusCode == ConstantMessages.SuccessStatusCode)
                    {
                        var userRolesObj = await _authenticateService.GetUserRolesInformation(loginDTOObj);
                        var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserName",userRolesObj.UserName),
                        new Claim("EmailId", userRolesObj.EmailId),
                        new Claim("PhoneNumber", userRolesObj.PhoneNumber),
                        new Claim("Address", userRolesObj.Address),
                        new Claim("IsActive", Convert.ToString(userRolesObj.IsActive)),
                        new Claim("Roles", userRolesObj.RoleName)
                    };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            _configuration["Jwt:Issuer"],
                            _configuration["Jwt:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddMinutes(10),
                            signingCredentials: signIn);
                        loginResponseObj.UserMessage = res.StatusMessage;
                        loginResponseObj.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);
                        return Ok(loginResponseObj);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status204NoContent, res.StatusMessage);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
