namespace scada_back.DTOs
{
    public class TagRecordDTO
    {
        public DateTime Timestamp { get; set; }
        public double Value { get; set; }
        public int TagId { get; set; }
        public double? LowLimit { get; set; }
        public double? HighLimit { get; set; }
    }
}