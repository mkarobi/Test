using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;

namespace TFWebService.Data.Models
{
    public class FitnessCalories : BaseEntity<int>
    {
        public FitnessCalories()
        {
            Id = new int();
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
        }

        [Required]
        public string Title { get; set; }

        public string Muscle { get; set; } = null;
        public string Description { get; set; } = null;
        public string Number { get; set; } = null;
        public string AmountCalories { get; set; } = null;
        public string PicUrl { get; set; } = null;

    }
}
