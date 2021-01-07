using Blog.Application.Models;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Services.IdentityService
{
    /// <summary>
    /// Service manages with user identity. Collects all the necessary functionality from ASP.NET Identity for application.
    /// </summary>
    public interface IIdentityService
    {
        /// <summary>
        /// Checks if current user is admin by user name.
        /// </summary>
        /// <param name="userName">User name.</param>
        /// <returns></returns>
        Task<bool> UserIsAdmin(string userName);

        /// <summary>
        /// Gets an email of user by user identifier.
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns></returns>
        Task<string> GetEmailByIdAsync(string userId);

        /// <summary>
        /// Creates a new user to the database.
        /// </summary>
        /// <param name="email">User email.</param>
        /// <param name="password">User password.</param>
        /// <returns></returns>
        Task<Result> CreateUserAsync(string email, string password);

        /// <summary>
        /// Sign in the specified user by certain credentials.
        /// </summary>
        /// <param name="userName">User name.</param>
        /// <param name="password">Password.</param>
        /// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
        /// <param name="lockoutOnFailure">Flag indicating if the user account should be locked if the sign in fails.</param>
        /// <returns></returns>
        Task<Result> LoginUserAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure);

        /// <summary>
        /// Signs the current user out of the application.
        /// </summary>
        /// <returns></returns>
        Task LogoutUserAsync();
    }
}
