using scada_back.Models;

namespace scada_back.DTOs
{
    public class AlarmRecordDTO
    {
        public int TagId { get; set; }
        public Priority Priority { get; set; }
        public DateTime TimeStamp { get; set; }
        public Models.Type Type { get; set; }
        public double Value { get; set; }
        public int AlarmId { get; set; }
    }
}