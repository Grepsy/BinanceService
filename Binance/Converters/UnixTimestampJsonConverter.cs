using Binance.Extensions;
using Newtonsoft.Json;
using System;
using System.Reflection;

namespace Binance.Converters {
    public class UnixTimestampJsonConverter : JsonConverter {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            long timestamp = serializer.Deserialize<long>(reader);

            return timestamp.ToDateTime();
        }

        public override bool CanConvert(Type objectType) {
            return typeof(DateTime).IsAssignableFrom(objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            serializer.Serialize(writer, ((DateTime)value).ToUnixTimestamp());
        }

        public override bool CanRead {
            get { return true; }
        }
    }
}
