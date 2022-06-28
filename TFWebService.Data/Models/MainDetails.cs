using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TFWebService.Data.Models
{
    public class MainDetails : BaseEntity<int>
    {
        public MainDetails()
        {
            Id = new int();
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
        }

        public string TotalCalories { get; set; } = "0";
        public string ActivityCalories { get; set; } = "0";
        public string FoodCalories { get; set; } = "0";
        public string WaterGlasses { get; set; } = "0";
        public string SelfWeight { get; set; } = "0";
        public string PersianDate { get; set; } = null;

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }


    }
}
