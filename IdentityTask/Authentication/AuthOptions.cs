using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace IdentityTask.Authentication
{
    internal static class AuthOptions
    {
        public const string Issuer = "MyAuthServer"; 
        public const string Audience = "http://localhost:58914/"; 
        private const string Key = "secretkey123dssafjgjldfk";
        public const int Lifetime = 10;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
