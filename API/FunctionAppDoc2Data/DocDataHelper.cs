using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace FunctionAppDoc2Data;
public static class DocDataHelper
{
    public static decimal GetNumberFromString(decimal value, string contentString)
    {
        try
        {
            if (!String.IsNullOrEmpty(contentString))
            {
                if (!String.IsNullOrEmpty(value.ToString()) && value != 0)
                {
                    return value;
                }
                else
                {
                    contentString = Regex.Replace(contentString, @"[^\d.,-]", "");

                    // Try parsing the number using invariant culture
                    if (decimal.TryParse(contentString, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result))
                    {
                        return result;
                    }

                    // Try parsing with the current culture in case of localized formats
                    if (decimal.TryParse(contentString, NumberStyles.Any, CultureInfo.CurrentCulture, out result))
                    {
                        return result;
                    }
                }
            }
            return 0;
        }
        catch { 
            return 0;
        }
    }

    public static List<DateTime?> ListParseStringsToDateTime(params string[] dateStrings)
    {
        // Define the possible date formats you expect
        string[] dateFormats = new[]
        {
            "yyyy-MM-dd",
            "MM/dd/yyyy",
            "dd/MM/yyyy",
            "yyyyMMdd",
            "dd-MMM-yyyy",
            "yyyy/MM/dd",
            "MM-dd-yyyy",
            "dd.MM.yyyy",
            "yyyy.MM.dd",
            "MMM dd, yyyy",
            "dd MMM yyyy",
            "yyyy MMM dd"
        };

        List<DateTime?> parsedDates = new List<DateTime?>();

        foreach (var dateString in dateStrings)
        {
            // Remove extra characters (non-digits, non-letters, and non-separators)
            string cleanedString = Regex.Replace(dateString, @"[^0-9a-zA-Z\/\-\.\s,]", "");

            DateTime parsedDate;
            bool success = DateTime.TryParseExact(cleanedString, dateFormats,
                                                  System.Globalization.CultureInfo.InvariantCulture,
                                                  System.Globalization.DateTimeStyles.None,
                                                  out parsedDate);

            if (success)
            {
                parsedDates.Add(parsedDate);
            }
            else
            {
                parsedDates.Add(null); // Add null if parsing fails
            }
        }

        return parsedDates;
    }


    public static DateTime? ParseStringsToDateTime(string dateStrings)
    {
        // Define the possible date formats you expect
        string[] dateFormats = new[]
        {
            "yyyy-MM-dd",
            "MM/dd/yyyy",
            "dd/MM/yyyy",
            "yyyyMMdd",
            "dd-MMM-yyyy",
            "yyyy/MM/dd",
            "MM-dd-yyyy",
            "dd.MM.yyyy",
            "yyyy.MM.dd",
            "MMM dd, yyyy",
            "dd MMM yyyy",
            "yyyy MMM dd"
        };

        List<DateTime?> parsedDates = new List<DateTime?>();
        string cleanedString = Regex.Replace(dateStrings, @"[^0-9a-zA-Z\/\-\.\s,]", "");

        DateTime parsedDate;
        bool success = DateTime.TryParseExact(cleanedString, dateFormats,
                                              System.Globalization.CultureInfo.InvariantCulture,
                                              System.Globalization.DateTimeStyles.None,
                                              out parsedDate);

        if (success)
        {
            return parsedDate;
        }

        return null;
    }
}
