using System;
using System.Text.RegularExpressions;

namespace FunctionAppDoc2Data;
public static class DocDataHelper
{
    public static decimal GetNumberFromString(decimal value, string contentString)
    {
        if(!String.IsNullOrEmpty(contentString))
        {
            if (!String.IsNullOrEmpty(value.ToString()) && value != 0)
            {
                return value;
            }
            else
            {
                contentString = Regex.Replace(contentString, "^[^0-9]+|[^0-9]+$", "");
                contentString = Regex.Replace(contentString, "[^0-9]+", ".");
                return Convert.ToDecimal(contentString);
            }
        }
        return 0;
    }
}
