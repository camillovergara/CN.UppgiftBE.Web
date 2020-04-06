using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace CN.UppgiftBE.Web.Util
{
    public interface IFormatUtils
    {
        int TryParseInt(string value, int defaultValue = 0);
        float TryParseFloat(string value, float defaultValue = 0);
    }
    public class FormatUtils : IFormatUtils
    {
       
        public int TryParseInt(string value, int defaultValue = 0)
        {
            int parsedValue;
            if (int.TryParse(value, out parsedValue))
            {
                return parsedValue;
            }

            return defaultValue;
        }
        public float TryParseFloat(string value, float defaultValue = 0)
        {
            float parsedValue;
            NumberStyles style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            if (float.TryParse(value.Replace(',', '.'), style, culture, out parsedValue))
            {
                return parsedValue;
            }

            return defaultValue;
        }
    }
}