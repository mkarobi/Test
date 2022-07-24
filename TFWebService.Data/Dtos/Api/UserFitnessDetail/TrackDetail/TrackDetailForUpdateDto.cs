using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TFWebService.Data.Dtos.Api.UserFitnessDetail.TrackDetail
{
    public class TrackDetailForUpdateDto
    {
        [Required] public string TrackActivity { get; set; }
        [Required] public string TrackFood { get; set; }
        [Required] public string TrackWeight { get; set; }
        [Required] public string PersianDate { get; set; }
    }
}
