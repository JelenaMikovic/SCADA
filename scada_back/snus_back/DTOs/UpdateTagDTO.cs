﻿namespace scada_back.DTOs
{
    public class UpdateTagDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IOAddress { get; set; }
        public double Value { get; set; }
        public int? ScanTime { get; set; }
        public bool? IsScanOn { get; set; }
        public double? LowLimit { get; set; }
        public double? HighLimit { get; set; }
        public string? Unit { get; set; }
        public string TagType { get; set; }
    }
}
