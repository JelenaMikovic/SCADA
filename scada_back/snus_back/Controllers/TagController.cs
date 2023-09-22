﻿using System;
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
		[Route("")]
		public ActionResult GetAllTags()
		{
			try
			{
                ICollection<TagDTO> response = this.tagService.getAllTags();
                return Ok(response);
			}
			catch(Exception e)
			{
				return BadRequest(new { Message = e.Message });
			}

		}


        [HttpGet]
        [Route("{IOaddress}")]
        public ActionResult GetAllTagsByIOAddress(string address)
        {
            try
            {
                ICollection<TagDTO> response = this.tagService.getAllTagsByIOAddress(address);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpGet]
        [Route("input-dbm")]
        public ActionResult GetAllInputTagsDBManager()
        {
            try
            {
                ICollection<ITDbManagerDTO> ret = this.tagService.GetAllInputTagsDBManager();
                return Ok(ret);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }


        [HttpGet]
        [Route("output-dbm")]
        public ActionResult GetAllOutputTagsDBManager()
        {
            try
            {
                ICollection<OTDbManagerDTO> ret = this.tagService.GetAllOutputTagsDBManager();
                return Ok(ret);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        [HttpPost]
        public ActionResult CreateTag(CreateTagDTO createTagDTO)
        {
            try
            {
                Console.WriteLine("kara2");
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
    }
}

