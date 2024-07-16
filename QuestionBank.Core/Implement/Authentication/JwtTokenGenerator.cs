using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuestionBank.Core.Interfaces.Services.Authentication;
using QuestionBank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBank.Core.Implement.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
	{
		private readonly JwtSettings _jwtSettings;

		public JwtTokenGenerator(IOptions<JwtSettings> jwtOptions)
		{
			_jwtSettings = jwtOptions.Value;
		}

		public async Task<string> GenerateToken(AppUser user, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
		{
			var signingCredentials = new SigningCredentials
			(
				new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
				SecurityAlgorithms.HmacSha256
			);

			var claims = new List<Claim>()
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.GivenName, user.Email ?? string.Empty),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			};

			/*var roles = await userManager.GetRolesAsync(user);
			foreach (var userRole in roles)
			{
				var role = await roleManager.FindByNameAsync(userRole);

				if (role != null)
				{
					claims.Add(new Claim(ClaimTypes.Role, userRole));
					var roleClaims = await roleManager.GetClaimsAsync(role);
					foreach (var roleClaim in roleClaims)
					{
						claims.Add(roleClaim);
					}
				}
			}*/

			var securityToken = new JwtSecurityToken(
				issuer: _jwtSettings.Issuer,
				expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
				audience: _jwtSettings.Audience,
				claims: claims,
				signingCredentials: signingCredentials
			);

			return new JwtSecurityTokenHandler().WriteToken(securityToken);
		}
	}
}
