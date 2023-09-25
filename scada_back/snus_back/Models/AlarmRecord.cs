using scada_back.Models;
using System.ComponentModel.DataAnnotations;

namespace scada_back.Models
{
    public class AlarmRecord
    {
        [Key]
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public int AlarmId { get; set; }
        public int TagId { get; set; }
    }
}
