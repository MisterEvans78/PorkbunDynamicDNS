using System.Text.Json.Serialization;

namespace PorkbunDynamicDNS.Models
{
    public class Record
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        [JsonPropertyName("content")]
        public string? Content { get; set; }
        [JsonPropertyName("ttl")]
        public int? Ttl { get; set; }
        [JsonPropertyName("prio")]
        public int? Prio { get; set; }
        [JsonPropertyName("notes")]
        public string? Notes { get; set; }
    }
}
