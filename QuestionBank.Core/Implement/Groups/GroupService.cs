using Microsoft.AspNetCore.Identity;
using QuestionBank.Core.Interfaces.Repositories.Groups;
using QuestionBank.Core.Interfaces.Repositories.Users;
using QuestionBank.Core.Interfaces.Services.Groups;
using QuestionBank.Core.Utilities;
using QuestionBank.Core.ViewModels.Request;
using QuestionBank.Core.ViewModels.Request.Groups;
using QuestionBank.Core.ViewModels.Response;
using QuestionBank.Core.ViewModels.Response.Groups;
using QuestionBank.Core.ViewModels.Response.User;
using QuestionBank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBank.Core.Implement.Groups
{
	public class GroupService : IGroupService
	{
		private readonly IGroupRepository _groupRepository;

		public GroupService(IGroupRepository groupRepository)
		{
			_groupRepository = groupRepository;
		}

		public async Task<bool> Create(GroupRequest request)
		{
			var group = new Group()
			{
				Name = request.Name,
				Description = request.Description,
				UserGroups = new List<UserGroup>(),
			};

			await _groupRepository.Add(group);
			return true;
		}

		public async Task<bool> DeleteRecord(int id)
		{
			var res = await _groupRepository.Get(i => i.Id == id);

			if (res is null) { throw new Exception("Something wrong"); }

			await _groupRepository.Delete(res);
			return true;
		}

		public async Task<bool> DeleteSoft(int id)
		{
			var res = await _groupRepository.Get(i => i.Id == id);

			if (res is null) { throw new Exception("Something wrong"); }

			res.IsDeleted = true;

			await _groupRepository.Update(res);
			return true;
		}

		public async Task<GroupResponse> GetById(int id)
		{
			var res = await _groupRepository.Get(i => i.Id == id, includeProperties: "UserGroups");

			if (res is null) { throw new Exception("Something wrong"); }

			var total = res.UserGroups.Count;

			return new GroupResponse(res, total);
		}

		public async Task<PagedResponse<GroupResponse>> GetList(PagedRequest request)
		{
			Expression<Func<Group, bool>> filter = i => !i.IsDeleted;

			if (request.SearchText != null)
			{
				Expression<Func<Group, bool>> searchFilter = i => i.Name.Contains(request.SearchText);

				filter = PredicateBuilder.AndAlso(filter, searchFilter);
			}

			var query = await _groupRepository
				.QueryAsync(
				filter,
				orderBy: q => Helper.ApplyOrder(q, request.SortColumn ?? "Name", request.SortOrder),
				pageSize: request.PageSize,
				page: request.CurrentPage,
				includeProperties: "UserGroups"
		   );

			int total = await _groupRepository.Count(filter);

			var list = new List<GroupResponse>();

			foreach (var item in query)
			{
				list.Add(new GroupResponse(item, item.UserGroups.Count()));
			}

			return new PagedResponse<GroupResponse>()
			{
				Items = list,
				Limit = request.PageSize,
				TotalItems = total,
				TotalPages = (int)Math.Ceiling((double)total / request.PageSize),
				Page = request.CurrentPage
			};
		}

		public async Task<bool> Update(int id, GroupRequest request)
		{
			var res = await _groupRepository.Get(i => i.Id == id);

			if (res is null) { throw new Exception("Something wrong"); }

			res.Name = request.Name;
			res.Description = request.Description;

			await _groupRepository.Update(res);
			return true;
		}
	}
}
