using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestionBank.Core.Interfaces.Repositories.Groups;
using QuestionBank.Core.Interfaces.Services.Groups;
using QuestionBank.Core.ViewModels.Request;
using QuestionBank.Core.ViewModels.Request.Groups;

namespace QuestionBank.Server.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class GroupController : ControllerBase
	{
		private readonly IGroupService _groupService;

		public GroupController(IGroupService groupService)
		{
			_groupService = groupService;
		}

		[HttpGet]
		[Route("GetList")]
		[Authorize]
		public async Task<IActionResult> GetList([FromQuery] PagedRequest request)
		{
			try
			{
				var res = await _groupService.GetList(request);

				return Ok(res);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
		[Route("Get")]
		[Authorize]
		public async Task<IActionResult> GetById(int id)
		{
			try
			{
				var res = await _groupService.GetById(id);
				return Ok(res);
			}
			catch (Exception ex) 
			{ 
				return BadRequest(ex.Message); 
			}
		}

		[HttpPost]
		[Route("Create")]
		[Authorize]
		public async Task<IActionResult> Create(GroupRequest request)
		{
			try
			{
				var res = await _groupService.Create(request);
				return Ok(res);
			}
			catch (Exception ex) 
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		[Route("Update")]
		
		public async Task<IActionResult> Update(int id, GroupRequest request)
		{
			try
			{
				var res = await _groupService.Update(id, request);
				return Ok(res);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete]
		[Route("DeleteSoft")]
		
		public async Task<IActionResult> DeleteSoft(int id)
		{
			try
			{
				var res = await _groupService.DeleteSoft(id);
				return Ok(res);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete]
		[Route("DeleteRecord")]
		
		public async Task<IActionResult> DeleteRecord(int id)
		{
			try
			{
				var res = await _groupService.DeleteRecord(id);
				return Ok(res);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
