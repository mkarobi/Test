using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TFWebService.Data.Dtos.CANDC.DeviceInfo
{
    public class DeviceForAddDto
    {
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
    }
}
