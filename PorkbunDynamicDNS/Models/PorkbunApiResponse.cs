using System.Text.Json.Serialization;

namespace PorkbunDynamicDNS.Models
{
    public class PorkbunApiResponse
    {
        [JsonPropertyName("status")]
        public required string Status { get; set; }
        [JsonPropertyName("message")]
        public string? Message { get; set; }
        [JsonPropertyName("cloudflare")]
        public string? Cloudflare { get; set; }
        [JsonPropertyName("id")]
        public int? Id { get; set; }
        [JsonPropertyName("records")]
        public IEnumerable<Record>? Records { get; set; }
        [JsonPropertyName("yourIp")]
        public string? YourIP { get; set; }
    }
}
