using System.ComponentModel.DataAnnotations;

namespace TFWebService.Data.Dtos.Api.Auth
{
    public class UserForLoginDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده صحیح نمی باشد")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool IsRemember { get; set; }
    }
}
