using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebScheduler.BLL
{
    public class AuthOptions
    {
        public const string ISSUER = "AuthSchedulerServer"; 
        public const string AUDIENCE = "WebScheduler"; 
        const string KEY = "secretkey_of_my_web_scheduler_SS";  
        public const int LIFETIME = 100; 
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
