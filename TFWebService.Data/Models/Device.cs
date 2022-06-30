using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TFWebService.Data.Models
{
    public class Device : BaseEntity<int>
    {
        public Device()
        {
            Id = new int();
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
        }

        public string Brand { get; set; } = null;
        public string DeviceID { get; set; } = null;
        public string Model { get; set; } = null;
        public string BuildID { get; set; } = null;
        public string SDK { get; set; } = null;
        public string Manufacture { get; set; } = null;
        public string BuildUser { get; set; } = null;
        public string Type { get; set; } = null;
        public string Base { get; set; } = null;
        public string Incremental { get; set; } = null;
        public string Board { get; set; } = null;
        public string Host { get; set; } = null;
        public string FingerPrint { get; set; } = null;
        public string VersionCode { get; set; } = null;

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
