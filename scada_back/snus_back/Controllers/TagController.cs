using System;
using Microsoft.AspNetCore.Mvc;
using scada_back.DTOs;
using scada_back.Services.IServices;

namespace scada_back.Controllers
{
    [ApiController]
	[Route("api/[controller]")]
	public class TagController : Controller
	{
		private ITagService tagService;
		public TagController(ITagService tagService)
		{
			this.tagService = tagService;
		}

		[HttpGet]
		public ActionResult GetAllTags()
		{
			try
			{
                List<TagDTO> response = this.tagService.getAllTags();
                return Ok(response);
			}
			catch(Exception e)
			{
				return BadRequest(new { Message = e.Message });
			}

		}

        [HttpPost]
        public ActionResult CreateTag(CreateTagDTO createTagDTO)
        {
            try
            {
                this.tagService.CreateTag(createTagDTO);
                return Ok(new { Message = "Tag created successfully" });
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpDelete("{tagId}")]
        public ActionResult DeleteTag(int tagId)
        {
            try
            {
                this.tagService.DeleteTag(tagId);
                return Ok(new { Message = "Tag deleted successfully" });
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpPut]
        public ActionResult UpdateTag(UpdateTagDTO updateTagDTO)
        {
            try
            {
                this.tagService.UpdateTag(updateTagDTO);
                return Ok(new { Message = "Tag updated successfully" });
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpPut("{tagId}")]
        public ActionResult ToggleScan(int tagId)
        {
            try
            {
                this.tagService.ToggleIsScanOn(tagId);
                return Ok(new { Message = "Scan toggled successfully" });
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpGet]
        [Route("{address}")]
        public ActionResult GetAllRecordsByIOAddress(string address)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine(address);
                ICollection<TagRecordDTO> ret = this.tagService.GetAllRecordsByIOAddress(address);
                return Ok(ret);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpPost]
        [Route("dates")]
        public ActionResult GetAllTagsBetweenDates(DateRangeDTO dto)
        {
            try
            {
                ICollection<TagRecordDTO> ret = this.tagService.GetAllTagsBetweenDates(dto.StartTime, dto.EndTime);
                return Ok(ret);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpGet]
        [Route("latestAI")]
        public ActionResult GetLatestAi()
        {
            try
            {
                ICollection<TagRecordDTO> ret = this.tagService.GetLatestAI();
                return Ok(ret);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpGet]
        [Route("latestDI")]
        public ActionResult GetLatestDi()
        {
            try
            {
                ICollection<TagRecordDTO> ret = this.tagService.GetLatestDI();
                return Ok(ret);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

    }
}

