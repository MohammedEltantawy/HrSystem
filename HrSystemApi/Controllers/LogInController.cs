using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using HrSystem.Services;

namespace HrSystemApi.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class LogInController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserService _userService;
        public LogInController(IConfiguration configuration, UserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpGet("LogIn")]
        public Task<string> Login([FromQuery] string username, [FromQuery] string password)
        {
            _userService.AddToPredicateList(x => x.UserName == username && x.PasswordHash == password);
            var result = _userService.GetAll(null, null, null);
            if (result.Count() > 0)
            {
                string roleName = result.ToList().First().Roles.First().Name.ToString();
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role,roleName),
                    new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddDays(2),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
                    );
                return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
            }
            return Task.FromResult("Unauthorized");
        }
    }
}
