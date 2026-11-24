namespace Beacon_V1
{
    public record StatusResult
    {
        public required string Name { get; set; }
        public string? Indicator { get; set; }
        public string? Description { get; set; }
        public string? Error { get; set; }
    }
}