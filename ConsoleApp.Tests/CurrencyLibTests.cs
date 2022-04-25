using System.Linq;
using ConsoleApp.Services;
using Domain.Helpers;
using Domain.Models;
using NUnit.Framework;

namespace ConsoleApp.Tests
{
    [TestFixture]
    public class CurrencyLibTests
    {
        [Test]
        public void ReadXmlData_FirstElement_AustralianDollar()
        {
            var currencyLib = new CurrencyLib();
            var array = currencyLib.GetArrayXmlElements();

            var expectedResult = new CurrencyData(
                "Австралийский доллар",
                "Australian Dollar",
                1,
                "R01010",
                "36",
                "AUD"
            );

            Assert.AreEqual(expectedResult,array[0]);
        }
        
        [Test]
        public void ReadXmlData_LastElement_JapaneseYen()
        {
            var currencyLib = new CurrencyLib();
            var array = currencyLib.GetArrayXmlElements();

            var expectedResult = new CurrencyData(
                "Японская иена",
                "Japanese Yen",
                100,
                "R01820",
                "392",
                "JPY"
            );

            Assert.AreEqual(expectedResult,array.Last());
        }
    }
}