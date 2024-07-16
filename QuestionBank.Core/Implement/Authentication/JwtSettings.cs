using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBank.Core.Implement.Authentication
{
	public class JwtSettings
	{
		public const string SectionsName = "JwtSettings";

		public string Secret { get; init; } = default!;

		public int ExpiryMinutes { get; init; } = default!;

		public string Audience { get; init; } = default!;

		public string Issuer { get; init; } = default!;
	}
}
