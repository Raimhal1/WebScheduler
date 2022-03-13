using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.DtoModels
{
    public class AuthenticateResponse
    {
        public string UserLogin { get; set; }
        public string JwtToken {get; set;}
        public string RefreshToken { get; set; }

        public AuthenticateResponse(User user, string jwtToken, string refreshToken)
        {
            UserLogin = user.Email;
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
        }
    }
}
