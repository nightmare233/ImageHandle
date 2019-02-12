using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Application;
using Models;
using WebApplication.Common;

namespace WebApplication.Controllers
{
    public class UserController : Controller
    { 
        private UserService userService;

        public UserController()
        { 
            userService = new UserService();
        }

        // GET: User
        public ActionResult Index()
        {
            if (new UserHelper().GetCurrentUser.Role != EnumRole.管理员.ToString())
            {
                return RedirectToAction("Error", "Front", new { Message = "你没有权限!"});
            }
            var list = userService.ListAll();
            //ViewBag["Model"] = list;
            return View("Index", list);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            var user = userService.GetUserForId(id);
            return View("Details", user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View("Create",new User());
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                var existUser = userService.GetUserForLoginName(user.LoginName);
                if (existUser != null && existUser.LoginName == user.LoginName)
                {
                    ViewBag.Message = "登录名已经存在！";
                    return View(user);
                }

                user.Password = Utils.Encrypt(user.Password);//加密
                userService.Save(user); 
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(user);
            }
        }

        // GET: User/Update/5
        public ActionResult Update(int id)
        {
            var user = userService.GetUserForId(id);
            return View("Update", user); 
        }

        // POST: User/Update/5
        [HttpPost]
        public ActionResult Update(User user)
        {
            try
            {
                var oldUser = userService.GetUserForId(user.Id);
                if (string.IsNullOrEmpty(user.Password))
                {
                    user.Password = oldUser.Password;
                }
                else
                {
                    user.Password = Utils.Encrypt(user.Password); //加密
                }
                userService.Save(user);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }


        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                userService.Delete(id);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }
    }
}
