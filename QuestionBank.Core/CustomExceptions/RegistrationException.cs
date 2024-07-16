using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouchStudy.Core.CustomExceptions
{
    public class RegistrationException : Exception
    {
        public const int StatusCode = 400;
        public RegistrationException(IEnumerable<IdentityError> errors)
        {
            Errors = errors;
        }

        public IEnumerable<IdentityError>? Errors { get; set; } = default;
    }
}
