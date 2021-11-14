using System.ComponentModel.DataAnnotations;

namespace WebScheduler.BLL.DtoModels
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
