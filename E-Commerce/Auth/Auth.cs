using E_comm.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace e_comm.Auth
{
    public class Auth : IAuth
    {
        private readonly DataContext db;
        private readonly string key;

        public Auth(string key, DataContext context)
        {
            db = context;
            this.key = key;
        }

        public string Authentication(string email, string password)
        {
            var user = db.Users.FirstOrDefault(u => u.Email == email);
            if (user == null || user.Password != password)
            {
                return null;
            }

            //var admin = db.Admins.FirstOrDefault(u => u.Name == username);
            //if (admin == null || admin.Password != password)
            //{
            //    return null; 
            //}

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
