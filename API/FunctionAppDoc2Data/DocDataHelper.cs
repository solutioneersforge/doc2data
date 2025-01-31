using System;
using System.Text.RegularExpressions;

namespace FunctionAppDoc2Data;
public static class DocDataHelper
{
    public static decimal GetNumberFromString(decimal value, string contentString)
    {
        return Convert.ToDecimal(Regex.Replace(contentString, @"\D", ""));
    }
}
