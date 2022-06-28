using System.ComponentModel.DataAnnotations;

namespace TFWebService.Data.Dtos.Api.Auth
{
    public class UserForRegisterDto
    {
        [Required]
        [EmailAddress(ErrorMessage ="ایمیل وارد شده صحیح نمی باشد")]
        public string UserName { get; set; }
        [Required]
        [StringLength(10,MinimumLength =4,ErrorMessage ="پسورد باید بین 4 تا 10 رقم باشد")]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        public string PhoneNumber { get; set; } = null;
        
    }
}
