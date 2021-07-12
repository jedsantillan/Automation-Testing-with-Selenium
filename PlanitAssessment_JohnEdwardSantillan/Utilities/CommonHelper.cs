using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PlanitAssessment_JohnEdwardSantillan.Utilities
{
    public class CommonHelper
    {
        public static string GetNumbersFromText(string input)
        {
            return new string(input.Where(c => char.IsDigit(c) || c.Equals('.')).ToArray());
        }

        public static T TryParseText<T>(string text)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            return (T)converter.ConvertFromString(null, CultureInfo.InvariantCulture, text);
        }
    }
}
