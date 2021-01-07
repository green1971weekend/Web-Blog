using Blog.Application.Models;
using Blog.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Services.IdentityService
{
    ///<inheritdoc cref="IIdentityService"/>
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public IdentityService(UserManager<IdentityUser> userManager,
                               SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        ///<inheritdoc/>
        public async Task<bool> UserIsAdmin(string userName)
        {
            userName = userName ?? throw new ArgumentNullException(nameof(userName));

            var user = await _userManager.FindByNameAsync(userName);
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            return isAdmin;
        }

        ///<inheritdoc/>
        public async Task<string> GetEmailByIdAsync(string userId)
        {
            userId = userId ?? throw new ArgumentNullException(nameof(userId));

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            return user.Email;
        }

        ///<inheritdoc/>
        public async Task<Result> CreateUserAsync(string email, string password)
        {
            var user = new IdentityUser
            {
                Email = email,
                UserName = email,
            };

            IdentityResult result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "user");
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return result.ToApplicationResult();
        }

        ///<inheritdoc/>
        public async Task<Result> LoginUserAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            var result = await _signInManager.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);

            return result.ToApplicationResult(errors: new string[] { "Coudn't sign in. Try again." });

        }

        ///<inheritdoc/>
        public async Task LogoutUserAsync()
        {
            // Delete authentification cookies of the current session.
            await _signInManager.SignOutAsync();
        }
    }
}
