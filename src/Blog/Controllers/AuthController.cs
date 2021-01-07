using Blog.Infrastructure.Services.IdentityService;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class AuthController : Controller
    {
        private readonly IIdentityService _identityService;

        public string CommonError { get; set; }

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            var result = await _identityService.LoginUserAsync(vm.UserName, vm.Password, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                CommonError = string.Join(", ", result.Errors);
                ModelState.AddModelError("UserName", CommonError);

                return View(vm);
            }

            var isAdmin = await _identityService.UserIsAdmin(vm.UserName);

            if (isAdmin)
            {
                return RedirectToAction("Index", "Panel");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var result = await _identityService.CreateUserAsync(vm.Email, vm.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    CommonError = string.Join(", ", error);
                }

                ModelState.AddModelError("Password", CommonError);

                return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _identityService.LogoutUserAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
