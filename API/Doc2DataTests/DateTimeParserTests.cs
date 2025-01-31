namespace Doc2DataTests
{

    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FunctionAppDoc2Data;

    [TestClass]
    public class DateTimeParserTests
    {
        [TestMethod]
        public void Test_GetDateFromString_ValidDateString()
        {
            // Arrange
            string contentString = "2023/10/05";
            DateTime? value = null;

            // Act
            DateTime result = DocDataHelper.GetDateFromString(value, contentString);

            // Assert
            Assert.AreEqual(new DateTime(2023, 10, 5), result);
        }

        [TestMethod]
        public void Test_GetDateFromString_InvalidDateString()
        {
            // Arrange
            string contentString = "InvalidDate";
            DateTime? value = null;

            // Act
            DateTime result = DocDataHelper.GetDateFromString(value, contentString);

            // Assert
            Assert.AreEqual(DateTime.MinValue, result); // Should return DateTime.MinValue for invalid input
        }

        [TestMethod]
        public void Test_GetDateFromString_WithValueParameter()
        {
            // Arrange
            string contentString = "2023/10/05";
            DateTime? value = new DateTime(2024, 12, 25);

            // Act
            DateTime result = DocDataHelper.GetDateFromString(value, contentString);

            // Assert
            Assert.AreEqual(value.Value, result); // Should return the provided value
        }

        [TestMethod]
        public void Test_GetDateFromString_EmptyContentString()
        {
            // Arrange
            string contentString = "";
            DateTime? value = null;

            // Act
            DateTime result = DocDataHelper.GetDateFromString(value, contentString);

            // Assert
            Assert.AreEqual(DateTime.MinValue, result); // Should return DateTime.MinValue for empty input
        }

        [TestMethod]
        public void Test_GetDateFromString_WithExtraCharacters()
        {
            // Arrange
            string contentString = "$2023/10/05";
            DateTime? value = null;

            // Act
            DateTime result = DocDataHelper.GetDateFromString(value, contentString);

            // Assert
            Assert.AreEqual(new DateTime(2023, 10, 5), result); // Should handle extra characters
        }

        [TestMethod]
        public void Test_GetDateFromString_EdgeCases()
        {
            // Arrange
            string contentString1 = "0001-01-01"; // Minimum DateTime value
            string contentString2 = "9999-12-31"; // Maximum DateTime value
            DateTime? value = null;

            // Act
            DateTime result1 = DocDataHelper.GetDateFromString(value, contentString1);
            DateTime result2 = DocDataHelper.GetDateFromString(value, contentString2);

            // Assert
            Assert.AreEqual(new DateTime(1, 1, 1), result1);
            Assert.AreEqual(new DateTime(9999, 12, 31), result2);
        }

        [TestMethod]
        public void Test_GetDateFromString_WithTimeComponent()
        {
            // Arrange
            string contentString = "2023-10-05T14:30:45";
            DateTime? value = null;

            // Act
            DateTime result = DocDataHelper.GetDateFromString(value, contentString);

            // Assert
            Assert.AreEqual(new DateTime(2023, 10, 5, 14, 30, 45), result); // Should handle time component
        }

        [TestMethod]
        public void Test_GetDateFromString_WithDifferentFormats()
        {
            // Arrange
            string[] dateStrings = {
            "10/05/2023",       // MM/dd/yyyy
            "05-10-2023",       // dd-MM-yyyy
            "2023.10.05",       // yyyy.MM.dd
            "05 Oct 2023",      // dd MMM yyyy
            "20231005",         // yyyyMMdd
            "10/05/23",         // MM/dd/yy
            "05-10-23",         // dd-MM-yy
            "23.10.05"          // yy.MM.dd
        };
            DateTime? value = null;

            // Act & Assert
            foreach (var dateString in dateStrings)
            {
                DateTime result = DocDataHelper.GetDateFromString(value, dateString);
                Assert.AreEqual(new DateTime(2023, 10, 5), result); // All should parse to 2023-10-05
            }
        }
    }


}
