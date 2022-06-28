using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TFWebService.Data.Models
{
    public class TrackDetails : BaseEntity<int>
    {
        public TrackDetails()
        {
            Id = new int();
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
        }

        public string TrackActivity { get; set; } = "0";
        public string TrackFood { get; set; } = "0";
        public string TrackWeight { get; set; } = "0";
        public string PersianDate { get; set; } = null;

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
