namespace scada_back.DTOs
{
    public class DeviceDTO
    {
        public string IOAddress { get; set; }
        public double Value { get; set; }
        public string Type { get; set; }
    }
}