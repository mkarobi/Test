﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TFWebService.Data.Models
{
    public class User : BaseEntity<int>
    {
        public User()
        {
            Id = new int();
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
        }

        [Required]
        public string Name { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string PhoneNumber { get; set; } = null;
        public string Address { get; set; } = null;

        public bool IsAdmin { get; set; } = false;

        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }

        public bool? Gender { get; set; }
        public string DateOfBirth { get; set; } = null;
        public string City { get; set; } = null;

        [Required] public bool IsAcive { get; set; } = true;
        [Required] public bool Status { get; set; } = true;

        public ICollection<TrackDetails> TrackDetails { get; set; }
        public ICollection<MainDetails> MainDetails { get; set; }
        public ICollection<Device> Devices { get; set; }
    }
}
