using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBank.Core.Constants
{
	public class AuthenticationConstant
	{
		public static readonly string RoleAdmin = "Admin";
		public static readonly string RoleProofReader = "ProofReader";
		public static readonly string RoleQuestionCreator = "QuestionCreator";
		public static readonly List<string> Roles = [
			RoleAdmin,
			RoleProofReader,
			RoleQuestionCreator
		];
	}
}
