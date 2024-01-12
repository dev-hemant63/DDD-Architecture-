using System.ComponentModel.DataAnnotations;

namespace WEBAPI.Model
{
    public class LoginReq
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
