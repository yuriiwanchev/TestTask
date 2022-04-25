using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Domain.Helpers;
using Domain.Models;
using Serilog;

namespace ConsoleApp.Services
{
    internal abstract class XmlWorker<T>
    {
        protected abstract string SchemaUri { get; }
        protected abstract string InputUri { get; }
        protected abstract string XmlElementName { get; }

        public T[] GetArrayXmlElements()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            List<T> currencies = new List<T>();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas.Add(null, SchemaUri);

            using (XmlReader reader = XmlReader.Create(InputUri, settings))
            {
                try
                {
                    reader.MoveToContent();
                }
                catch (System.Xml.Schema.XmlSchemaValidationException e)
                {
                    Log.Error("No data",e);
                    throw new InvalidDataXmlException();
                }
                reader.ReadToDescendant(XmlElementName);
                
                do
                {
                    currencies.Add(ReadXmlData(reader));
                } while (reader.ReadToFollowing(XmlElementName));
                
            }

            return currencies.ToArray();
        }

        protected abstract T ReadXmlData(XmlReader reader);
    }
}