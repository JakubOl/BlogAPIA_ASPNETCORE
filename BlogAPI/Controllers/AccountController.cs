using BlogAPIModels.DtoModels;
using BlogAPIServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Controllers
{
    [Route("/")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("register")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromForm] RegisterUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            var user = await _accountService.Register(dto);
            
            return Redirect("/posts");
        }

        [HttpGet("login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromForm] LoginUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            var user = await _accountService.Login(dto);
            if(user != -1)
            {
                return Redirect("/posts");
            }
            TempData["Error"] = "Username or Password is invalid";
            return View("login");
        }

        [HttpGet("logout")]
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            await _accountService.Logout();
            return Redirect("/login");
        }
    }
}
