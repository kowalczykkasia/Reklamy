using AdvertApi.DTOs.Requests;
using AdvertApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AdvertApi.Controllers
{
    [ApiController]
    [Route("api/clients/login")]
    public class LoginController : ControllerBase
    {

        public IConfiguration Configuration { get; set; }

        public LoginController(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        [HttpPost]
        public IActionResult Login([FromServices] IClientDal _dbService, LoginRequest loginRequest)
        {
            if (!_dbService.CheckPassword(loginRequest)) return Unauthorized();
            else
            {
                var claims = new[]
               {
                new Claim(ClaimTypes.NameIdentifier, loginRequest.Login)
                };

                var key = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(Configuration["SecretKey"]));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken
                (
                    issuer: "Gakko",
                    audience: "Clients",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: creds
                    );

                Guid refreshToken = Guid.NewGuid();
                _dbService.GrandToken(loginRequest.Login, refreshToken.ToString());

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    refreshToken = refreshToken
                });
            }
        }

        [HttpPost("refresh-token/{refToken}")]
        public IActionResult RefreshToken([FromServices] IClientDal _dbService, string refToken)
        {
            string login = "ja";

            if (login.Equals("")) return NotFound();


            var claims = new[]
               {
                new Claim(ClaimTypes.NameIdentifier, login)
                };

            var key = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration["SecretKey"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: "Gakko",
                audience: "Clients",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
                );

            Guid refreshToken = Guid.NewGuid();

            _dbService.GrandToken(login, refreshToken.ToString());

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = refreshToken
            });
        }

    }
}
