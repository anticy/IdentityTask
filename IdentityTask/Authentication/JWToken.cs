using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using IdentityTask.Models;
using Microsoft.IdentityModel.Tokens;

namespace IdentityTask.Authentication
{
    class JwToken :IToken
    {
        public string GenerateJwt(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                notBefore: now,
                claims: GetIdentity(user).Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }

        private ClaimsIdentity GetIdentity(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                //new Claim(JwtRegisteredClaimNames.Sub, "")
            };
            var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}
