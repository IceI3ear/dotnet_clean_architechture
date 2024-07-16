using Microsoft.AspNetCore.Identity;
using QuestionBank.Core.Interfaces.Services.Authentication;
using QuestionBank.Core.ViewModels.Request.User;
using QuestionBank.Core.ViewModels.Response;
using QuestionBank.Core.ViewModels.Response.User;
using QuestionBank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestionBank.Core.ViewModels.Request;
using QuestionBank.Core.ViewModels.Response.Groups;
using QuestionBank.Core.Interfaces.Services.Users;
using QuestionBank.Core.Interfaces.Repositories.Users;
using QuestionBank.Core.Constants;
using System.Linq.Expressions;
using QuestionBank.Core.Utilities;

namespace QuestionBank.Core.Implement.Users
{
	public class UserService : IUserService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IUserRepository _userRepository;
		private readonly RoleManager<AppRole> _roleManager;
		public UserService(IUserRepository userRepository, RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
		{
			_userRepository = userRepository;
			_roleManager = roleManager;
			_userManager = userManager;
		}

		public async Task<UserResponse> AutoGet(int id)
		{
			var res = await _userRepository.Get(i => i.Id == id);

			if (res is null) throw new Exception("Something wrong");

			var roles = await _userManager.GetRolesAsync(res);

			return new UserResponse(res, roles);
		}

		public async Task<bool> CreateUser(UserRequest model)
		{
			var user = new AppUser() 
			{ 
				UserName = model.UserName,
				Email = model.Email,
				PhoneNumber = model.PhoneNumber,
				UserGroups = new List<UserGroup>(),
			};

			foreach( var groupId in model.GroupIds )
			{
				user.UserGroups.Add(new UserGroup() { GroupId = groupId });
			}

			await _userManager.CreateAsync(user);

			await _userManager.AddToRolesAsync(user, model.Roles);

			return true;
		}

		public async Task<bool> DeleteRecordUser(int id)
		{
			var res = await _userRepository.Get(i => i.Id == id);

			if (res is null) throw new Exception("Something wrong");

			await _userRepository.Delete(res);

			return true;
		}

		public async Task<bool> DeleteSoftUser(int id)
		{
			var res = await _userRepository.Get(i =>i.Id == id);

			if (res is null) throw new Exception("Something wrong");

			res.IsDeleted = true;

			await _userRepository.Update(res);

			return true;
		}

		public async Task<UserResponse> Get(int id)
		{
			var res = await _userRepository.Get(i => i.Id == id);

			if (res is null) throw new Exception("Something wrong");

			var roles = await _userManager.GetRolesAsync(res);

			return new UserResponse(res, roles);
		}

		public async Task<PagedResponse<UserResponse>> GetListUsers(PagedRequest request)
		{
			Expression<Func<AppUser, bool>> filter = i => !i.IsDeleted;

			if (request.SearchText != null)
			{
				Expression<Func<AppUser, bool>> searchFilter = i => i.UserName.Contains(request.SearchText);

				filter = PredicateBuilder.AndAlso(filter, searchFilter);
			}

			var query = await _userRepository
		   .QueryAsync(
			   filter,
			   //orderBy: q => Helper.ApplyOrder(q, request.sort.Field ?? "UserName", request.sort.Order),
			   pageSize: request.PageSize,
			   page: request.CurrentPage
		   );

			int total = await _userRepository.Count(filter);

			var userList = new List<UserResponse>();

			foreach (var user in query)
			{
				var roles = await _userManager.GetRolesAsync(user);
				userList.Add(new UserResponse(user, roles));
			}

			return new PagedResponse<UserResponse>()
			{
				Items = userList,
				Limit = request.PageSize,
				TotalItems = total,
				TotalPages = (int)Math.Ceiling((double)total / request.PageSize),
				Page = request.CurrentPage
			};
		}

		public async Task<bool> UpdateUser(int id, UserRequest model)
		{
			var user = await _userRepository.Get(i =>i.Id == id);

			if (user is null) throw new Exception("Something wrong");

			user.UserName = model.UserName;
			user.Email = model.Email;
			user.PhoneNumber = model.PhoneNumber;

			user.UserGroups.Clear();

			foreach (var idGroup in model.GroupIds)
			{
				user.UserGroups.Add(new UserGroup() { GroupId = idGroup });
			}

			await _userRepository.Update(user);

			var currentRoles = await _userManager.GetRolesAsync(user);
			await _userManager.RemoveFromRolesAsync(user, currentRoles);
			await _userManager.AddToRolesAsync(user, model.Roles);
			return true;
		}
	}
}
