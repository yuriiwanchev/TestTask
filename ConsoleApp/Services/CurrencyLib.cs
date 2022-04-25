using System;
using System.Xml;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ConsoleApp.Services
{
    internal class CurrencyLib : XmlWorker<CurrencyData>
    {
        protected override string SchemaUri { get; }
        protected override string InputUri { get; }
        protected override string XmlElementName { get; }
        
        public CurrencyLib()
        {
            SchemaUri = "http://www.cbr.ru/StaticHtml/File/92172/XML_valFull.xsd";
            InputUri = "http://www.cbr.ru/scripts/XML_valFull.asp";
            XmlElementName = "Item";
        }

        // Get CurrencyData from xml from InputUri via SchemaUri
        protected override CurrencyData ReadXmlData(XmlReader reader)
        {
            try
            {
                string parentCode = reader.GetAttribute("ID");
                reader.ReadToDescendant("Name");
                var name = reader.ReadElementContentAsString();
                var engName = reader.ReadElementContentAsString();
                var nominal = (uint)reader.ReadElementContentAsObject();
                // В следующем узле часто хранятся некорректные данные
                // поэтому parentCode будем брать через ID
                // var parentCode = reader.ReadElementContentAsString().Trim();
                reader.ReadElementContentAsString();
                var numCode = reader.ReadElementContentAsString();
                var charCode = reader.ReadElementContentAsString();

                return new CurrencyData(name, engName, nominal, parentCode, numCode, charCode);
            }
            catch (Exception e)
            {
                Log.Error($"Cannot get data from {InputUri}",e);
                throw;
            }
        }
    }
}