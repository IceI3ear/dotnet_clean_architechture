using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestionBank.Core.Interfaces.Services.Authentication;
using QuestionBank.Core.ViewModels.Request.Authentication;

namespace QuestionBank.Server.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly IAccountService _accountService;

		public AuthenticationController(IAccountService accountService)
		{
			_accountService = accountService;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginRequest request)
		{
			try
			{
				var res = await _accountService.Login(request);
				if(res.User == null) { return BadRequest("Login fails"); }
				else { return Ok(res); }
			}
			catch (Exception ex) 
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
