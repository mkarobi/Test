﻿using Microsoft.AspNetCore.Http;
using System;

namespace TFWebService.Common.Helpers
{
    public static class Extentions
    {
        public static void AddAppError(this HttpResponse response,string message)
        {
            response.Headers.Add("App-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers","App-Error");
            response.Headers.Add("Access-Control-Allow-Origin","*");
        }

        public static int ToAge(this DateTime dateTime)
        {
            var age = DateTime.Today.Year - dateTime.Year;
            if (dateTime.AddYears(age) > DateTime.Today)
                age--;

            return age;
        }
    }
}
