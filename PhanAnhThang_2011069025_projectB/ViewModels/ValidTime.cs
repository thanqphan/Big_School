using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace PhanAnhThang_2011069025_projectB.ViewModels
{
    public class ValidTime : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime;
            var isValid = DateTime.TryParseExact(Convert.ToString(value),
                            "HH:mm",
                            CultureInfo.CurrentCulture,//in4 about languega,number,datetime, related to culture
                            DateTimeStyles.None,
                            out dateTime);
            return isValid;
        }
    }
}