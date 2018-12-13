using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MVCTest.Data;
using MVCTest.Models;

namespace MVCTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;

        public HomeController(DataContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            var loginuser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName);
            if (loginuser == null)
                return BadRequest("没有该用户");
            if (loginuser.UserPwd != Md5Change(user.UserPwd))
                return BadRequest("密码错误");


            return RedirectToAction("Index", "VisaForms");
        }

        public static string Md5Change(string str)
        {
            string rs = "";
            byte[] result = Encoding.Default.GetBytes(str.Trim());
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            rs = BitConverter.ToString(output).Replace("-", "");
            return rs;
        }
    }
}
