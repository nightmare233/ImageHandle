using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Filters;
using WebApplication.Application;
using WebApplication.Common;
using WebApplication.Models;
using System.Net;

namespace WebApplication.Controllers
{
    [SkipCheckLogin]
    public class LoginController : Controller
    {
        private UserService userService;

        public LoginController()
        {
            userService = new UserService();
        }
        // GET: Login
        public ActionResult Login()
        {
            Models.User user = new User();
            return View(user);
        }


        [HttpPost]
        public ActionResult Login(User user)
        {
            var userFromDb = userService.GetUserForLoginName(user.LoginName);
            string pwd = Utils.Encrypt(user.Password);
            if (userFromDb == null)
            {
                ViewBag.Message = "用户名不存在！";
                return View(user);
            }
            if (userFromDb.Password != pwd)
            {
                ViewBag.Message = "密码不对！";
                return View(user);
            }
            HttpContext.Session["LoginUser"] = user.LoginName;

            HttpCookie cookie = new HttpCookie("cookieUser");
            cookie["name"] = user.LoginName;
            DateTime dtNow = DateTime.Now;
            TimeSpan tsTime = new TimeSpan(0, 10, 0, 0);
            cookie.Expires = dtNow + tsTime;
            Response.Cookies.Add(cookie);

            //if(user.Role == EnumRole.管理员)
            return RedirectToAction("Index", "Home");
        }

    }
}
