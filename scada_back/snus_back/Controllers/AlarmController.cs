using Microsoft.AspNetCore.Mvc;
using scada_back.DTOs;
using scada_back.Services.IServices;

namespace scada_back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlarmController : Controller
    {
        private IAlarmService alarmService;
        public AlarmController(IAlarmService alarmService)
        {
            this.alarmService = alarmService;
        }

        [HttpPost]
        public ActionResult AddAlarm(AddAlarmDTO dto)
        {
            try
            {
                this.alarmService.AddAlarm(dto);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteAlarm(int id)
        {
            try
            {
                this.alarmService.DeleteAlarm(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpGet("{tagId}")]
        public ActionResult GetTagsAlarms(int tagId)
        {
            try
            {
                List<AlarmDTO> alarms = this.alarmService.GetTagsAlarms(tagId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }
    }
}
