using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Coursework2021Api.Auth
{
    public class AuthOptions
    {
        public const string ISSUER = "Coursework2021ShpekApi";
        public const string AUDIENCE = "Coursework2021ShpekClient";
        const string KEY = "ljqwasdklm!4-12312";
        public const int LIFETIME = 300;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}