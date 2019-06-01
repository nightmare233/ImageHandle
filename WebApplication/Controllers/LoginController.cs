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
        private LogService logService;
        private log4net.ILog log = log4net.LogManager.GetLogger("LoginController");
        public LoginController()
        {
            logService = new LogService();
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
            var logs = new Logs { Action = EnumAction.登录, Detail = user.Name, UserId = UserHelper.GetCurrentUser.Id, Time = DateTime.Now };
            logService.Insert(logs);
            return RedirectToAction("Index", "Order");
        }

        public ActionResult Logout()
        {
            var user = UserHelper.GetCurrentUser;
            if (user != null)
            {
                log.Error("user logout: " + user.LoginName); 
                HttpContext.Session["User"] = null;
                var logs = new Logs { Action = EnumAction.登出, Detail = user.Name, UserId = UserHelper.GetCurrentUser.Id, Time = DateTime.Now };
                logService.Insert(logs);
                return RedirectToAction("Login", "Login");
            }
            return RedirectToAction("Login", "Login"); 
        }
    }
}
