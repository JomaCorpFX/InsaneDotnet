//using Newtonsoft.Json;
//using Newtonsoft.Json.Converters;

//namespace InsaneIO.Insane.Serialization
//{
//    [Obsolete(@"Use ""System.Text.Json instead"".", true)]
//    public class JsonSerializerDeserializer
//    {

//        public static T Deserialize<T>(string json)
//        {
//            JsonSerializerSettings settings = new JsonSerializerSettings();
//            settings.Converters.Add(new JsonByteArrayConverter());
//            settings.Converters.Add(new StringEnumConverter
//            {
//                AllowIntegerValues = true
//            });
//            return JsonConvert.DeserializeObject<T>(json, settings)!;
//        }

//        public static string Serialize(object instance)
//        {
//            return Serialize(instance, false, false);
//        }

//        public static string Serialize(object instance, bool isoFormatDate, Boolean enumAsString)
//        {
//            JsonSerializerSettings settings = new JsonSerializerSettings();
//            settings.Converters.Add(new JsonByteArrayConverter());
//            settings.DateFormatHandling = isoFormatDate ? DateFormatHandling.IsoDateFormat : DateFormatHandling.MicrosoftDateFormat;
//            if (enumAsString)
//            {
//                settings.Converters.Add(new StringEnumConverter
//                {
//                    AllowIntegerValues = true
//                });
//            }
//            return JsonConvert.SerializeObject(instance, settings);
//        }

//    }
//}
