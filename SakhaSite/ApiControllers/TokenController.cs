using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SakhaSite.Models.DataModels;
using SakhaSite.Models.ViewModels;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace SakhaSite.ApiControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private UserManager<AppUser> _userManager;

        public TokenController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Username or password is invalid.");

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
                return BadRequest("Username or password is invalid.");

            var checkPassword = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!checkPassword)
                return BadRequest("Username or password is invalid.");

            var claims = await _userManager.GetClaimsAsync(user);
            var roles = string.Join(',', await _userManager.GetRolesAsync(user));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim("UserName", user.UserName));
            claims.Add(new Claim("Roles", roles));

            var key = new SymmetricSecurityKey(Encoding.Unicode.GetBytes("my secure password"));
            var token = new JwtSecurityToken(
                issuer: "http://localhost",
                audience: "http://localhost",
                claims: claims,
                expires: DateTime.Now.AddHours(6),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            return Json(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expires = token.ValidTo,
            });

        }
    }
}