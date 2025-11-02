using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcomApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;

        public HomeController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            var userEmail = User.Identity?.IsAuthenticated == true ? User.Identity?.Name : "Guest";
            ViewBag.User = userEmail;
            ViewBag.SiteName = _config["Brand:SiteName"];
            return View();
        }

        [Authorize]
        public IActionResult Checkout()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            ViewBag.Email = userEmail;
            ViewBag.SiteName = _config["Brand:SiteName"];
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            var authUrl = _config["AuthApp:LoginUrl"];
            var returnUrl = $"{Request.Scheme}://{Request.Host}/Home/LoginCallback";
            return Redirect($"{authUrl}?returnUrl={Uri.EscapeDataString(returnUrl)}");
        }

        [HttpGet]
        public IActionResult LoginCallback(string token)
        {
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Index");

            if (ValidateJwt(token))
            {
                Response.Cookies.Append("jwt", token, new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTimeOffset.UtcNow.AddHours(1)
                });
                TempData["Message"] = "You are now logged in!";
            }
            else
            {
                TempData["Message"] = "Invalid token.";
            }

            return RedirectToAction("Index");
        }

        private bool ValidateJwt(string token)
        {
            try
            {
                var jwtSection = _config.GetSection("Jwt");
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"]!));
                var parameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = jwtSection["Issuer"],
                    ValidAudience = jwtSection["Audience"],
                    IssuerSigningKey = key,
                    ValidateLifetime = true
                };

                var handler = new JwtSecurityTokenHandler();
                handler.ValidateToken(token, parameters, out var validatedToken);
                var jwt = (JwtSecurityToken)validatedToken;

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, jwt.Claims.First(c => c.Type == ClaimTypes.Email).Value),
                    new Claim(ClaimTypes.Name, jwt.Claims.First(c => c.Type == ClaimTypes.Name).Value)
                };

                var identity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                HttpContext.User = principal;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            TempData["Message"] = "Logged out successfully.";
            return RedirectToAction("Index");
        }
    }
}