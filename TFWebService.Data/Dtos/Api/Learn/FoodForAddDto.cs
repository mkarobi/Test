using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace TFWebService.Data.Dtos.Api.Learn
{
    public class FoodForAddDto
    {
        [Required]
        public string Title { get; set; }
        public string Properties { get; set; } = null;
        public string HowToMake { get; set; } = null;
        public string AmountCalories { get; set; } = null;
        public string PicUrl { get; set; } = null;
        public IFormFile File { get; set; }
    }
}
