using System.Text.Json.Serialization;

namespace PorkbunDynamicDNS.Models
{
    public class PorkbunApiBody
    {
        [JsonPropertyName("apikey")]
        public required string ApiKey { get; set; }
        [JsonPropertyName("secretapikey")]
        public required string SecretApiKey { get; set; }
        [JsonPropertyName("name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Name { get; set; }
        [JsonPropertyName("type")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Type { get; set; }
        [JsonPropertyName("content")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Content { get; set; }
        [JsonPropertyName("ttl")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Ttl { get; set; }
    }
}
