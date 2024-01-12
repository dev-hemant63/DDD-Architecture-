using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WEBAPI.Model;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JWT _jWT;
        private readonly IUserService _userService;
        public AccountController(AppSettings appSettings, IUserService userService)
        {
            _jWT = appSettings.JWT;
            _userService = userService;
        }

        [HttpPost(nameof(SignUp))]
        public async Task<IActionResult> SignUp(SignUpReq signUpReq)
        {
            var res = await _userService.Create(new ApplicationUsers
            {
                FirstName = signUpReq.Name,
                MobileNo = signUpReq.MobileNo,
                Address = string.Empty,
                PostalCode = 0,
                AlternateNumber = signUpReq.MobileNo,
                WhatsAppNumber = signUpReq.MobileNo,
                MeddleName = string.Empty,
                LastName = string.Empty,
                RoleId = ApplicationRoles.User
            });
            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginReq loginReq)
        {
            var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, loginReq.UserName),
                        };

            var identity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var token = GenerateJwtToken(claims);
            return Ok(new
            {
                Token = token
            });
        }
        private string GenerateJwtToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWT.Secretkey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jWT.Issuer,
                audience: _jWT.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jWT.DurationInMinutes)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
