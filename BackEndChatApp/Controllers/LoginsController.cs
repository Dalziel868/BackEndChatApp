using BackEndChatApp.Models;
using BackEndChatApp.Respositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BackEndChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly IUserLoginRes _login;
        public LoginsController(IUserLoginRes login)
        {
            _login = login;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLogin model)
        {
            if(model==null)
            {
                return BadRequest("Invalid client request");
            }
            if(await _login.Login(model))
            {
                var secrectKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signingCredentials = new SigningCredentials(secrectKey, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,model.Email),
                    new Claim(ClaimTypes.Role,"User"),

                };
                var tokenOptions = new JwtSecurityToken
                (
                    issuer: "https://localhost:44380",
                    audience: "https://localhost:44380",
                    claims:claims,
                    expires:DateTime.Now.AddMinutes(5),
                    signingCredentials:signingCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { Token= tokenString });
                
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
