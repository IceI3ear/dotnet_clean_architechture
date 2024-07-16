using Microsoft.AspNetCore.Identity;
using QuestionBank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBank.Core.Interfaces.Services.Authentication
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateToken(AppUser user, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager);
    }
}
