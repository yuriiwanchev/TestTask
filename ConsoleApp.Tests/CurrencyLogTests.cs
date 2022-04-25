using System;
using System.Linq;
using ConsoleApp.Services;
using Domain.Helpers;
using Domain.Models;
using NUnit.Framework;

namespace ConsoleApp.Tests
{
    [TestFixture]
    public class CurrencyLogTests
    {
        [Test]
        public void ReadXmlData_CertainDate_CertainCurrency()
        {
            var currencyLib = new CurrencyLog(new DateTime(2021,02,02));
            var array = currencyLib.GetArrayXmlElements();

            var expectedResult = new Currency(
                "R01010",
                "57,6785",
                new DateTime(2021,02,02)
            );

            Assert.AreEqual(expectedResult,array[0]);
        }
        
        [Test]
        public void ReadXmlData_DateWithNoData_ThrowInvalidDataXmlException()
        {
            var currencyLib = new CurrencyLog(new DateTime(1990,02,02));

            Assert.Throws<InvalidDataXmlException>(
                delegate { currencyLib.GetArrayXmlElements(); });
        }
    }
}