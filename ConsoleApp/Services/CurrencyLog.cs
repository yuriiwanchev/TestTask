using System;
using System.Globalization;
using System.Xml;
using Domain.Models;
using Serilog;

namespace ConsoleApp.Services
{
    internal class CurrencyLog : XmlWorker<Currency>
    {
        protected override string SchemaUri { get; }
        protected override string InputUri { get; }
        protected override string XmlElementName { get; }

        private readonly DateTime _date;

        public CurrencyLog(DateTime date)
        {
            _date = date;
            InputUri = $"http://www.cbr.ru/scripts/XML_daily.asp?date_req=" +
                       _date.ToString("dd/MM/yyyy",  CultureInfo.InvariantCulture);

            SchemaUri = "http://www.cbr.ru/StaticHtml/File/92172/ValCurs.xsd";
            XmlElementName = "Valute";
        }
        
        // Get Currency from xml from InputUri via SchemaUri
        protected override Currency ReadXmlData(XmlReader reader)
        {
            try
            {
                string parentCode = reader.GetAttribute("ID");

                reader.ReadToDescendant("Value");
                var value = reader.ReadElementContentAsString();

                return new Currency(parentCode, value, _date);
            }
            catch (Exception e)
            {
                Log.Error($"Cannot get data from {InputUri}",e);
                throw;
            }
        }
    }
}