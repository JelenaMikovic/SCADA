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

        [HttpPost]
        public ActionResult CreateDevices(List<DeviceDTO> devicesDtos)
        {
            try
            {
                this.deviceService.CreateDevices(devicesDtos);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpGet]
        [Route("input")]
        public ActionResult GetAvailableInputDevices()
        {
            try
            {
                return Ok(this.deviceService.GetAvailableInputDevices());
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpGet]
        [Route("output")]
        public ActionResult GetAvailableOutputDevices()
        {
            try
            {
                return Ok(this.deviceService.GetAvailableOutputDevices());
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpGet]
        public ActionResult GetDevices()
        {
            try
            {
                return Ok(this.deviceService.GetDevices());
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

    }
}
