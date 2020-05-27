using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using TrailerSearchLocation.Models;

namespace TrailerSearchLocation.Managers
{
    public class XMLSerializerManager
    {
        public RootModel xmlTrailerResponse;
        public VehicleRootModel xmlVehicleResponse;

        public XMLSerializerManager()
        {
            xmlTrailerResponse = new RootModel();
            xmlVehicleResponse = new VehicleRootModel();
        }

        public T Deserialize<T>(string input) where T : TrailerModel
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T), new XmlRootAttribute("root"));

            using (StringReader stringReader = new StringReader(input))
            {
                return (T)serializer.Deserialize(stringReader);
            }
        }

        public string Serialize<T>(T TrailerModel)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(TrailerModel.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, TrailerModel);
                return textWriter.ToString();
            }
        }

        public RootModel TestDeserializer<T>(string xml)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(RootModel));
            TextReader textReader = new StringReader(xml);
            xmlTrailerResponse = (RootModel)deserializer.Deserialize(textReader);

            return xmlTrailerResponse;
        }

        public VehicleRootModel VehicleDeserializer<T>(string xml)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(VehicleRootModel));
            TextReader textReader = new StringReader(xml);
            xmlVehicleResponse = (VehicleRootModel)deserializer.Deserialize(textReader);

            return xmlVehicleResponse;
        }


    }
}