using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SPANTECH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController<AuthController>
    {
        private const string SecretKey = "YourVeryStrongSecretKeyForJWT12345!"; // Replace with your key
        public AuthController(ILogger<AuthController> logger) : base(logger) { }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Logging the request initiation
            LogInformation($"Login attempt for username: {request.Username}");

            // Example validation (replace with your actual authentication logic)

            if (request.Username == "span" && request.Password == "spanTech") // Example validation
            {
                LogInformation($"Successful login for username: {request.Username}");

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

                var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


                List<Claim> claims = new List<Claim>()
                {
                    new Claim("Username",request.Username)
                }.ToList();

                var token = new JwtSecurityToken
                    (
                    issuer: "EmployeeAPI",
                    audience: "EmployeeAPIUsers",
                    claims: claims,
                    signingCredentials: signingCredentials,
                    expires: DateTime.UtcNow.AddMinutes(15)
                    );

                var jwttoken = new JwtSecurityTokenHandler().WriteToken(token);

                // Logging token generation
                LogInformation($"JWT token generated for username: {request.Username}");

                return Ok(new { Token = jwttoken });
            }

            LogWarning($"Failed login attempt for username: {request.Username}");

            return Unauthorized(); // Standard Unauthorized response
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
