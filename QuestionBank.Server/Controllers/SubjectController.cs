using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestionBank.Core.Interfaces.Services.Subjects;
using QuestionBank.Core.ViewModels.Request.Groups;
using QuestionBank.Core.ViewModels.Request;
using QuestionBank.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using QuestionBank.Core.ViewModels.Request.Subjects;

namespace QuestionBank.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class SubjectController : ControllerBase
	{
		private readonly ISubjectService _subjectService;

		public SubjectController(ISubjectService subjectService)
		{
			_subjectService = subjectService;
		}

		[HttpGet]
		[Route("GetList")]
		public async Task<IActionResult> GetList([FromQuery] PagedRequest request)
		{
			try
			{
				var res = await _subjectService.GetList(request);

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
				var res = await _subjectService.Get(id);
				return Ok(res);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		[Route("Create")]
		public async Task<IActionResult> Create(SubjectRequest request)
		{
			try
			{
				var res = await _subjectService.Create(request);
				return Ok(res);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		[Route("Update")]
		public async Task<IActionResult> Update(int id, SubjectRequest request)
		{
			try
			{
				var res = await _subjectService.Update(id, request);
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
				var res = await _subjectService.DeleteSoft(id);
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
				var res = await _subjectService.DeleteRecord(id);
				return Ok(res);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
