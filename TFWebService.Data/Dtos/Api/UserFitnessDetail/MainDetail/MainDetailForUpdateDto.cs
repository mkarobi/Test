using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TFWebService.Data.Dtos.Api.UserFitnessDetail.MainDetail
{
    public class MainDetailForUpdateDto
    {
        [Required] public int Id { get; set; }
        [Required] public string TotalCalories { get; set; }
        [Required] public string ActivityCalories { get; set; }
        [Required] public string FoodCalories { get; set; }
        [Required] public string WaterGlasses { get; set; }
        [Required] public string SelfWeight { get; set; }
        [Required] public string PersianDate { get; set; }
    }
}
