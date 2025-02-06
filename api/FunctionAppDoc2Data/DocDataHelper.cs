using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace FunctionAppDoc2Data;
public static class DocDataHelper
{
  public static decimal GetNumberFromString(decimal value, string contentString)
  {
    try
    {
      if (!string.IsNullOrEmpty(contentString))
      {
        if (!string.IsNullOrEmpty(value.ToString()) && value != 0)
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
    catch
    {
      return 0;
    }
  }

  public static DateTime GetDateFromString(DateTime? value, string contentString)
  {
    if (!string.IsNullOrEmpty(contentString))
    {
      if (value is not null)
      {
        return value.Value;
      }

      string[] dateFormats =
       [
          // Slash (/) Separator
          "yyyy/MM/dd",       // e.g., 2023/10/05
                "MM/dd/yyyy",       // e.g., 10/05/2023
                "dd/MM/yyyy",       // e.g., 05/10/2023
                "yy/MM/dd",         // e.g., 23/10/05
                "MM/dd/yy",         // e.g., 10/05/23
                "dd/MM/yy",         // e.g., 05/10/23",

                // Hyphen (-) Separator
                "yyyy-MM-dd",       // e.g., 2023-10-05 (ISO 8601)
                "MM-dd-yyyy",       // e.g., 10-05-2023
                "dd-MM-yyyy",       // e.g., 05-10-2023
                "yy-MM-dd",         // e.g., 23-10-05
                "MM-dd-yy",         // e.g., 10-05-23
                "dd-MM-yy",         // e.g., 05-10-23",

                // Dot (.) Separator
                "yyyy.MM.dd",       // e.g., 2023.10.05
                "MM.dd.yyyy",       // e.g., 10.05.2023
                "dd.MM.yyyy",       // e.g., 05.10.2023
                "yy.MM.dd",         // e.g., 23.10.05
                "MM.dd.yy",         // e.g., 10.05.23
                "dd.MM.yy",         // e.g., 05.10.23",

                // Month Names
                "dd MMM yyyy",      // e.g., 05 Oct 2023
                "MMM dd, yyyy",     // e.g., Oct 05, 2023
                "dd MMMM yyyy",     // e.g., 05 October 2023
                "MMMM dd, yyyy",    // e.g., October 05, 2023
                "yyyy MMM dd",      // e.g., 2023 Oct 05
                "yyyy MMMM dd",     // e.g., 2023 October 05",

                // No Separators
                "yyyyMMdd",         // e.g., 20231005
                "ddMMyyyy",         // e.g., 05102023
                "MMddyyyy",         // e.g., 10052023
                "yyMMdd",           // e.g., 231005
                "ddMMyy",           // e.g., 051023
                "MMddyy",           // e.g., 100523",

                // Time Included
                "yyyy-MM-dd HH:mm:ss",          // e.g., 2023-10-05 14:30:45
                "MM/dd/yyyy HH:mm:ss",          // e.g., 10/05/2023 14:30:45
                "dd/MM/yyyy HH:mm:ss",          // e.g., 05/10/2023 14:30:45
                "yyyyMMddHHmmss",               // e.g., 20231005143045
                "yyyy-MM-ddTHH:mm:ss",          // e.g., 2023-10-05T14:30:45 (ISO 8601)
                "yyyy-MM-ddTHH:mm:ssZ",         // e.g., 2023-10-05T14:30:45Z (UTC)
                "yyyy-MM-ddTHH:mm:sszzz",       // e.g., 2023-10-05T14:30:45+05:30 (Time zone offset)",

                // Milliseconds
                "yyyy-MM-ddTHH:mm:ss.fff",      // e.g., 2023-10-05T14:30:45.123
                "yyyy-MM-dd HH:mm:ss.fff",      // e.g., 2023-10-05 14:30:45.123",

                // Ordinal Indicators
                "ddth MMM yyyy",                // e.g., 05th Oct 2023
                "MMMM ddth, yyyy",              // e.g., October 05th, 2023",

                // Weekday Names
                "ddd, dd MMM yyyy",             // e.g., Thu, 05 Oct 2023
                "dddd, dd MMM yyyy",            // e.g., Thursday, 05 Oct 2023
                "ddd, MMM dd, yyyy",            // e.g., Thu, Oct 05, 2023
                "dddd, MMMM dd, yyyy",          // e.g., Thursday, October 05, 2023",

                // Era (e.g., AD/BC)
                "yyyy MM dd G",                 // e.g., 2023 10 05 AD
                "G yyyy MM dd",                 // e.g., AD 2023 10 05",

                // Custom Text
                "'Date:' yyyy-MM-dd",           // e.g., Date: 2023-10-05
                "'Today is' MMM dd, yyyy",      // e.g., Today is Oct 05, 2023",

                // Non-Standard Representations
                "dd-MMM-yy",                    // e.g., 05-Oct-23
                "MMM-yy",                       // e.g., Oct-23
                "yyyyMM",                       // e.g., 202310
                "MMyyyy",                       // e.g., 102023",

                // Leading Zeros Omitted
                "M/d/yyyy",                     // e.g., 10/5/2023
                "d/M/yyyy",                     // e.g., 5/10/2023
                "yyyy-M-d",                     // e.g., 2023-10-5
                "d-M-yyyy",                     // e.g., 5-10-2023",

                // 24-Hour and 12-Hour Time
                "yyyy-MM-dd HH:mm:ss",          // e.g., 2023-10-05 14:30:45 (24-hour)
                "yyyy-MM-dd hh:mm:ss tt",       // e.g., 2023-10-05 02:30:45 PM (12-hour)",

                // Unix Timestamps (Not directly supported by DateTime.TryParseExact)
                // Unix timestamps require custom handling.

                // Fiscal Years
                "yyyy-'Q'Q",                    // e.g., 2023-Q4 (Fiscal quarter)
                "yyyy-'FY'",                    // e.g., 2023-FY (Fiscal year)",

                // Week Numbers
                "yyyy-'W'ww",                   // e.g., 2023-W40 (ISO week number)
                "yyyy-'W'ww-d",                 // e.g., 2023-W40-5 (ISO week number and day)"
            ];

      var cleanedString = Regex.Replace(contentString, @"[^0-9a-zA-Z\/\-\.\s,]", "");

      DateTime parsedDate;
      if (DateTime.TryParseExact(cleanedString, dateFormats,
                                  System.Globalization.CultureInfo.InvariantCulture,
                                  System.Globalization.DateTimeStyles.None,
                                  out parsedDate))
      {
        return parsedDate;
      }
    }

    return DateTime.MinValue;
  }
}
