using Blog.Application.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Infrastructure.Extensions
{
    /// <summary>
    /// Transform Identity service result to the custom ApplicationResult which responsible for additional response configuration.
    /// </summary>
    public static class IdentityResultExtension
    {
        /// <summary>
        /// Transform IdentityResult to the custom ApplicationResult.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Result ToApplicationResult(this IdentityResult result)
        {
            result = result ?? throw new ArgumentNullException(nameof(result));

            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => e.Description));
        }

        /// <summary>
        /// Transform SignInResult to the custom ApplicationResult.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="errors">Set of errors in case of failure.</param>
        /// <returns></returns>
        public static Result ToApplicationResult(this SignInResult result, IEnumerable<string> errors)
        {
            result = result ?? throw new ArgumentNullException(nameof(result));

            return result.Succeeded
                ? Result.Success()
                : Result.Failure(errors);
        }
    }
}
