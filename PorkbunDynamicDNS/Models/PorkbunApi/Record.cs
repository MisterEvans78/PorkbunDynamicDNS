namespace PorkbunDynamicDNS.Models.PorkbunApi
{
    public class Record
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Content { get; set; }
        public int? Ttl { get; set; }
        public int? Prio { get; set; }
        public string? Notes { get; set; }
    }
}
