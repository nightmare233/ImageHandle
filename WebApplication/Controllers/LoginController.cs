using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Filters;
using WebApplication.Application;
using WebApplication.Common;
using Models; 

namespace WebApplication.Controllers
{
    [SkipCheckLogin]
    public class LoginController : Controller
    {
        private UserService userService;
        private log4net.ILog log = log4net.LogManager.GetLogger("LoginController");
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
            log.Error("user login: " + user.LoginName);
            var userFromDb = userService.GetUserByLoginName(user.LoginName);
            string pwd = Utils.Encrypt(user.Password);
            if (userFromDb == null)
            {
                ViewBag.Message = "登录名不存在！";
                return View(user);
            }
            if (userFromDb.Password != pwd)
            {
                ViewBag.Message = "密码不正确！";
                return View(user);
            }
            HttpContext.Session["User"] = userFromDb;
            //存一份到cookie中，防止session意外丢失。
            //HttpCookie cookie = new HttpCookie("cookieUser");
            //cookie["LoginName"] = userFromDb.LoginName;
            //cookie["Name"] = userFromDb.Name;
            //cookie["Id"] = userFromDb.Id.ToString();
            //cookie["Role"] = userFromDb.Role;
            //DateTime dtNow = DateTime.Now;
            //TimeSpan tsTime = new TimeSpan(0, 10, 0, 0);
            //cookie.Expires = dtNow + tsTime;
            //Response.Cookies.Add(cookie);

            return RedirectToAction("Index", "Order");
        }

        public ActionResult Logout()
        {
            var user = UserHelper.GetCurrentUser;
            if (user != null)
            {
                log.Error("user logout: " + user.LoginName); 
                HttpContext.Session["User"] = null;
                return RedirectToAction("Login", "Login");
            }
            return RedirectToAction("Login", "Login"); 
        }
    }
}
