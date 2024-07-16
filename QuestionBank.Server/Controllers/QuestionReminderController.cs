using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestionBank.Core.Interfaces.Services.QuestionReminders;
using QuestionBank.Core.Interfaces.Services.Subjects;
using QuestionBank.Core.ViewModels.Request;
using QuestionBank.Core.ViewModels.Request.QuestionReminders;
using QuestionBank.Domain.Entities;

namespace QuestionBank.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class QuestionReminderController : ControllerBase
	{
		private readonly IQuestionReminderService _questionReminderService;

		public QuestionReminderController(IQuestionReminderService questionReminderService)
		{
			_questionReminderService = questionReminderService;
		}

		[HttpGet]
		[Route("GetList")]
		public async Task<IActionResult> GetList([FromQuery] PagedRequest request)
		{
			try
			{
				var res = await _questionReminderService.GetList(request);

				return Ok(res);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
		[Route("Get")]
		public async Task<IActionResult> GetById(int id)
		{
			try
			{
				var res = await _questionReminderService.Get(id);
				return Ok(res);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		[Route("Create")]
		public async Task<IActionResult> Create(QuestionReminderRequest request)
		{
			try
			{
				var res = await _questionReminderService.Create(request);
				return Ok(res);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		[Route("Update")]
		public async Task<IActionResult> Update(int id, QuestionReminderRequest request)
		{
			try
			{
				var res = await _questionReminderService.Update(id, request);
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
				var res = await _questionReminderService.DeleteSoft(id);
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
				var res = await _questionReminderService.DeleteRecord(id);
				return Ok(res);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
