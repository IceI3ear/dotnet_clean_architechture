using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using QuestionBank.Core.ViewModels.Response.Authentication;
using QuestionBank.Core.ViewModels.Request.Authentication;
using QuestionBank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using QuestionBank.Core.Interfaces.Services.Authentication;

namespace QuestionBank.Core.Implement.Authentication
{
    public class AccountService : IAccountService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IJwtTokenGenerator _jwtTokenGenerator;
		private readonly RoleManager<AppRole> _roleManager;
		public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtTokenGenerator jwtTokenGenerator, RoleManager<AppRole> roleManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_jwtTokenGenerator = jwtTokenGenerator;
			_roleManager = roleManager;
		}

		public async Task<LoginResponse> Login(LoginRequest request)
		{
			var user = await _userManager.FindByEmailAsync(request.Email);

			if (user == null)
			{
				throw new Exception("Can not find User");
			}

			var roles = await _userManager.GetRolesAsync(user);

			if (roles == null)
			{
				throw new Exception("User has no role ");
			}

			var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

			if (result.Succeeded)
			{
				var jwtToken = await _jwtTokenGenerator.GenerateToken(user!, _userManager, _roleManager);
				return new LoginResponse(user, jwtToken, roles);
			}

			return new LoginResponse()
			{
				User = null,
				Token = null
			};
		}
	}
}
