using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using carwash.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace carwash.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly CarwashDbContext _db;
        private readonly IConfiguration _configuration;
        public TokenRepository(IConfiguration configuration, CarwashDbContext db)
        {
            _configuration = configuration;
            _db=db;
        }
        public string CreateJwtToken(IdentityUser user, List<string> roles)
        {
            //Create Claims
            // var claims = new List<Claim>();
            // claims.Add(new Claim(ClaimTypes.Email,user.Email));
            var customer = _db.Users.FirstOrDefault(c => c.Email == user.Email);
            var washer = _db.Users.FirstOrDefault(c => c.Email == user.Email);
            string customerId = customer != null ? customer.Id.ToString() : string.Empty;

            string washerId = washer != null ? washer.Id.ToString() : string.Empty;
            // Create Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("customerId", customerId),  // 
                new Claim("washerId", washerId)  // 
            };

            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role,role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var credentials  = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires:DateTime.Now.AddMinutes(15),
                signingCredentials:credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
            
        }
    }
}