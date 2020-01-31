namespace SGDE.API.Controllers
{
    #region Using

    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Domain.Supervisor;
    using Domain.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Models;
    using Microsoft.Extensions.Logging;
    using System.Linq;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;
    using System.Security.Claims;

    #endregion

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly ISupervisor _supervisor;
        private readonly ILogger<AuthenticateController> _logger;

        public AuthenticateController(ILogger<AuthenticateController> logger, ISupervisor supervisor)
        {
            _logger = logger;
            _supervisor = supervisor;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public object Authenticate(Login login)
        {
            try
            {
                if (login.username != "test" || login.password != "test")
                {
                    _logger.LogWarning("Error in Authenticate: username [{Username}] not registered or incorrect password", login.username);
                    return BadRequest(new { message = "Username or password is incorrect" });
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("SECRET_KEY_TO_AUTHENTICATE");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, login.username)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return new ObjectResult(new Session
                {
                    username = login.username,
                    token = tokenHandler.WriteToken(token)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(500, BadRequest(new { message = ex.Message }));
            }
        }
    }
}