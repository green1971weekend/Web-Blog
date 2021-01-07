using System.Collections.Generic;
using System.Linq;

namespace Blog.Application.Models
{
    /// <summary>
    /// Application result allows to configure any type of result.
    /// </summary>
    public class Result
    {
        internal Result(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public bool Succeeded { get; set; }

        public IEnumerable<string> Errors { get; set; }

        /// <summary>
        /// Returns application result in case of success.
        /// </summary>
        /// <returns></returns>
        public static Result Success()
        {
            return new Result(true, new string[] { });
        }

        /// <summary>
        /// Returns application result in case of failure.
        /// </summary>
        /// <param name="errors">Error set.</param>
        /// <returns></returns>
        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }
    }
}
