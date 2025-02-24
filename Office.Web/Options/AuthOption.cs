using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Office.Web.Options;

public class AuthOption
{
    public class AuthOptions
    {
        public const string ISSUER = "FreeOutOfOffice"; // token publisher
        public const string AUDIENCE = "OutOfOffice"; // token customer
        const string KEY = "mysupersecret_secretkey!OutOfOffice";   // encryption key
        public const int LIFETIME = 1440; // token liftime
        public const string EmployeeIdCalmName = "EmployeeId";
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}