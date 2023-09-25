using System;
using Microsoft.AspNetCore.Mvc;
using scada_back.DTOs;
using scada_back.Services.IServices;

namespace scada_back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceController : Controller
    {
        private IDeviceService deviceService;
        public DeviceController(IDeviceService deviceService)
        {
            this.deviceService = deviceService;
        }

        [HttpPut]
        public ActionResult UpdateDevices(List<DeviceDTO> devicesDtos)
        {
            try
            {
                this.deviceService.UpdateValues(devicesDtos);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpGet]
        public ActionResult GetAvailableDevices()
        {
            try
            {
                return Ok(this.deviceService.GetAvailableDevices());
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

    }
}
