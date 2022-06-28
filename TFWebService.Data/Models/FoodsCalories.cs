using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TFWebService.Data.Models
{
    public class FoodsCalories : BaseEntity<int>
    {
        public FoodsCalories()
        {
            Id = new int();
            CreateTime = DateTime.Now;
            UpdateTime= DateTime.Now;
        }

        [Required]
        public string Title { get; set; }

        public string Properties { get; set; } = null;
        public string HowToMake { get; set; } = null;
        public string AmountCalories { get; set; } = null;
        public string PicUrl { get; set; } = null;


    }
}
