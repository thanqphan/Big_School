using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace PhanAnhThang_2011069025_projectB.ViewModels
{
    public class FutureDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime;
            var isValid = DateTime.TryParseExact(Convert.ToString(value), "M/dd/yyyy",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out dateTime);
            return (isValid && dateTime>DateTime.Now);
        }
    }
}