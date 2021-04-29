using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Coursework2021Api.Auth
{
    public static class AuthOptions
    {
        public const string ISSUER = "Coursework2021ShpekApi";
        public const string AUDIENCE = "Coursework2021ShpekClient";
        public const int LIFETIME = 300;
        private const string KEY = "ljqwasdklm!4-12312";

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}