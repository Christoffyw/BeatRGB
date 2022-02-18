using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static BeatmapSaveData;
using static beatRGB.Lighting.Extensions;

namespace beatRGB.Configuration
{
    public class LightingEventConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            BeatmapEventType c = (BeatmapEventType)value;
            var objValue = c.ToNamed();
            serializer.Serialize(writer, objValue);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return new BeatmapEventType();
            }
            else
            {
                JObject obj = JObject.Load(reader);
                return ((string)obj).FromNamed();
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(BeatmapEventType));
        }
    }
}
