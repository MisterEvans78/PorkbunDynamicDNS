namespace PorkbunDynamicDNS.Models
{
    public class PorkbunApiResponse
    {
        public required string Status { get; set; }
        public string? Message { get; set; }
        public string? Cloudflare { get; set; }
        public int? Id { get; set; }
        public IEnumerable<Record>? Records { get; set; }
        public string? YourIP { get; set; }
    }
}
