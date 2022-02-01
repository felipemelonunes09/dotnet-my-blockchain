using System;
using System.Text.Json;

namespace my_blockchain.blockchain.core {
    static class Utils {

        public static string PayloadToJsonString(Payload payload) {
            string json = JsonSerializer.Serialize<Payload>(payload);
            return json;
        }

        public static long GetTimestamp () {
            var timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            return timestamp;
        }
    }
}