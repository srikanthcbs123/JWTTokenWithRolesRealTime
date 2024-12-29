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
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),//this is one object
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),//this is one object
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),//this is one object
                        new Claim("UserName",userRolesObj.UserName),//this is one object
                        new Claim("EmailId", userRolesObj.EmailId),//this is one object
                        new Claim("PhoneNumber", userRolesObj.PhoneNumber),//this is one object
                        new Claim("Address", userRolesObj.Address),//this is one object
                        new Claim("IsActive", Convert.ToString(userRolesObj.IsActive)),//this is one object
                        new Claim("Roles", userRolesObj.RoleName)//this is one object
                    };
                        //this symentric security key comming from Microsoft.IdentityModel.Tokens  namespace
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                        //this SigningCredentials comming from Microsoft.IdentityModel.Tokens  namespace
                        //HmacSha256 is the encryption alogrotham.
                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        //this JwtSecurityToken comming from System.IdentityModel.Tokens.Jwt namespace                                
                        var token = new JwtSecurityToken(
                            _configuration["Jwt:Issuer"],
                            _configuration["Jwt:Audience"],
                            claims,//pass the claims object here.
                            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:TokenExpriyTime"])),//SET THE TIME FOR  YOUR TOKEN 
                            signingCredentials: signIn);
                        loginResponseObj.UserMessage = res.StatusMessage;
                        loginResponseObj.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);//pass the token object here
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
