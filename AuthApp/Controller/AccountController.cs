using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace AuthApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _config;

        public AccountController(IConfiguration config)
        {
            _config = config;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl ?? _config["ClientApp:ReturnUrl"];
            ViewBag.SiteName = _config["Brand:SiteName"];
            return View();
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult Login(string email, string password, string? returnUrl = null)
        {
            const string demoEmail = "user@example.com";
            const string demoPassword = "Pass123!";

            if (email == demoEmail && password == demoPassword)
            {
                var jwtSection = _config.GetSection("Jwt");
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"]!));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, email),
                    new Claim(ClaimTypes.Email, email)
                };

                var token = new JwtSecurityToken(
                    issuer: jwtSection["Issuer"],
                    audience: jwtSection["Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSection["TokenExpiryMinutes"])),
                    signingCredentials: creds);

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                var targetUrl = returnUrl ?? _config["ClientApp:ReturnUrl"];
                return Redirect($"{targetUrl}?token={tokenString}");
            }

            ViewBag.Error = "Invalid credentials.";
            ViewBag.SiteName = _config["Brand:SiteName"];
            return View();
        }
    }
}