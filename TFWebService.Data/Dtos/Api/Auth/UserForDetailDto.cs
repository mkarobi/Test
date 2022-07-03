﻿using System;

namespace TFWebService.Data.Dtos.Api.Auth
{
    public class UserForDetailDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateTime LastActive { get; set; }
        public string City { get; set; }
    }
}