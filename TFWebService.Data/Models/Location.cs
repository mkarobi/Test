using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TFWebService.Data.Models
{
    public class Location : BaseEntity<int>
    {
        public Location()
        {
            Id = new int();
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
        }

        public string Longitude { get; set; } = null;
        public string Latitude { get; set; } = null;
        public string Accuracy { get; set; } = null;
        public string Altitude { get; set; } = null;
        public string Provider { get; set; } = null;
        public string Speed { get; set; } = null;
        public string Time { get; set; } = null;

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
