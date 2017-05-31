using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace iScrumMaster.Infrastructure.Models
{
    /// <summary>
    /// This class will deserialize the xmlstring from the object.
    /// </summary>
    public static class SerializationManager
    {
        public static object FromString(string xmlString, Type sourceType)
        {
            Object deserializedObject = null;
            if (!string.IsNullOrEmpty(xmlString) && sourceType != null)
            {
                XmlSerializer serializer = new XmlSerializer(sourceType);

                using (StringReader reader = new StringReader(xmlString))
                {
                    deserializedObject = serializer.Deserialize(reader);
                }
                serializer = null;
            }
            return deserializedObject;
        }

        /// <summary>
        /// This method will take the object and convert that to the string.
        /// </summary>
        /// <param name="sourceObject"></param>
        /// <param name="fileName"></param>
        public static string ToString(object sourceObject)
        {
            string xmlString = string.Empty;
            if (sourceObject != null)
            {
                XmlSerializer serializer = new XmlSerializer(sourceObject.GetType());

                using (StringWriter writter = new StringWriter())
                {
                    try
                    {
                        serializer.Serialize(writter, sourceObject);
                        xmlString = writter.ToString();
                    }
                    catch (Exception ex)
                    {
                        ///TODO: Handle exception here.
                    }
                    
                }
                serializer = null;
            }
            return xmlString;
        }
    }
}