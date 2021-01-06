using Blog.Services.IdentityService;
using Blog.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IIdentityService _identityService;

        private string _commonError;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IIdentityService identityService)
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
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

            if(!result.Succeeded)
            {
                _commonError = string.Join(", ", result.Errors);
                ModelState.AddModelError("UserName", _commonError);

                return View(vm);
            }

            var isAdmin = await _identityService.UserIsAdmin(vm.UserName);

            if(isAdmin)
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
            if(!ModelState.IsValid)
            {
                return View(vm);
            }

            var result = await _identityService.CreateUserAsync(vm.Email, vm.Password);

            if(result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach(var error in result.Errors)
                {
                    _commonError = string.Join(", ", error);
                }

                ModelState.AddModelError("Password", _commonError);

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
