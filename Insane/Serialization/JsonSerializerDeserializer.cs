using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.CodeDom;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Insane.Serialization
{
    public class JsonSerializerDeserializer
    {

        public static T Deserialize<T>(string json)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new JsonByteArrayConverter());
            settings.Converters.Add(new StringEnumConverter
            {
                AllowIntegerValues = true
            });
            return JsonConvert.DeserializeObject<T>(json, settings)!;
        }

        public static string Serialize(object instance)
        {
            return Serialize(instance, false, false);
        }

        public static String Serialize(Object instance, Boolean isoFormatDate, Boolean enumAsString)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new JsonByteArrayConverter());
            settings.DateFormatHandling = isoFormatDate ? DateFormatHandling.IsoDateFormat : DateFormatHandling.MicrosoftDateFormat;
            if (enumAsString)
            {
                settings.Converters.Add(new StringEnumConverter
                {
                    AllowIntegerValues = true
                });
            }
            return JsonConvert.SerializeObject(instance, settings);
        }

    }
}
