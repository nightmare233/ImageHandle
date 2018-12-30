using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Application;
using WebApplication.Models;
using WebApplication.Common;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private UserService userService;

        public HomeController()
        {
            userService = new UserService();
        }

        public ActionResult Index()
        {
            Models.User user = new User();
            return View(user);
        }

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
             
            return RedirectToAction("Index", "User"); 
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}