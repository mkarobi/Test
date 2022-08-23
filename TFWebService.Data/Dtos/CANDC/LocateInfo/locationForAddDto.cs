using System;
using System.Collections.Generic;
using System.Text;

namespace TFWebService.Data.Dtos.CANDC.LocateInfo
{
    public class locationForAddDto
    {
        public string Longitude { get; set; } = null;
        public string Latitude { get; set; } = null;
        public string Accuracy { get; set; } = null;
        public string Altitude { get; set; } = null;
        public string Provider { get; set; } = null;
        public string Speed { get; set; } = null;
        public string Time { get; set; } = null;
    }
}
