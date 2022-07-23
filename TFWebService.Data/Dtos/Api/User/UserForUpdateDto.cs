using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TFWebService.Data.Dtos.Api.User
{
    public class UserForUpdateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; } = null;
        public string Address { get; set; } = null;
        public bool? Gender { get; set; }
        public string DateOfBirth { get; set; } = null;
        public string City { get; set; } = null;
    }
}
