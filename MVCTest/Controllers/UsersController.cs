using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace MVCTest.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Login(string userName, string password, string ReturnUrl)
        //{
        //    var user = _userService.Login(userName, password);
        //    if (user != null)
        //    {

        //        user.AuthenticationType = CookieAuthenticationDefaults.AuthenticationScheme;
        //        var identity = new ClaimsIdentity(user);
        //        identity.AddClaim(new Claim(ClaimTypes.Name, user.UserID));
        //        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

        //        return RedirectToAction("Index", "Dashboard");

        //    }
        //    ViewBag.Errormessage = "登录失败，用户名密码不正确";
        //    return View();
        //}
    }
}