using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace TFWebService.Data.Dtos.Api.Learn
{
    public class FitnessForAddDto
    {
        [Required]
        public string Title { get; set; }
        public string Muscle { get; set; } = null;
        public string Description { get; set; } = null;
        public string Number { get; set; } = null;
        public string AmountCalories { get; set; } = null;
        public string PicUrl { get; set; } = null;
        public IFormFile File { get; set; }
    }
}
