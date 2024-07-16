using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using QuestionBank.Core.Interfaces.Services.Users;
using QuestionBank.Core.ViewModels.Request;
using QuestionBank.Core.ViewModels.Request.User;
using QuestionBank.Core.ViewModels.Response;
using QuestionBank.Core.ViewModels.Response.User;
using QuestionBank.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace QuestionBank.Server.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly UserManager<AppUser> _userManager;
		public UserController(IUserService userService, UserManager<AppUser> userManager)
		{
			_userService = userService;
			_userManager = userManager;
		}

		[HttpGet]
		[Authorize]
		[Route("AutoGet")]
		public async Task<IActionResult> AutoGet()
		{
			try
			{
				var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

				if (string.IsNullOrEmpty(token))
				{
					return Unauthorized();
				}

				var handler = new JwtSecurityTokenHandler();
				var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

				if (jwtToken == null)
				{
					return Unauthorized();
				}

				var idClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "sub");

				if (idClaim == null || !int.TryParse(idClaim.Value, out int id))
				{
					return Unauthorized();
				}

				var res = await _userService.AutoGet(id);

				return Ok(res);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
		[Authorize]
		[Route("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				var res = await _userService.Get(id);
				return Ok(res);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
		[Authorize]
		[Route("GetList")]
		public async Task<IActionResult> GetListUser([FromQuery] PagedRequest request)
		{
			try
			{
				var res = await _userService.GetListUsers(request);
				return Ok(res);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		[Authorize]
		[Route("Create")]
		public async Task<IActionResult> CreateUser([FromBody] UserRequest request)
		{
			try
			{
				var res = await _userService.CreateUser(request);
				return Ok(res);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete]
		[Route("DeleteSoft")]
		[Authorize]
		public async Task<IActionResult> DeleteSoftUser(int id)
		{
			try
			{
				var res = await _userService.DeleteSoftUser(id);
				return Ok(res);
			}
			catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete]
		[Route("DeleteRecord")]
		[Authorize]
		public async Task<IActionResult> DeleteRecordUser(int id)
		{
			try
			{
				var res = await _userService.DeleteRecordUser(id);
				return Ok(res);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		[Route("Update")]
		[Authorize]
		public async Task<IActionResult> UpdateUser(int id, [FromBody] UserRequest request)
		{
			try
			{
				var res = await _userService.UpdateUser(id, request);
				return Ok(res);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
