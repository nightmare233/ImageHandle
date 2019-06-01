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
    //[RoleAuthorize]
    public class UserController : Controller
    { 
        private UserService userService;
        private log4net.ILog log = log4net.LogManager.GetLogger("UserController");
        public UserController()
        {
            //CheckPermission();
            userService = new UserService();
        } 

        // GET: User
        public ActionResult Index()
        { 
            var list = userService.ListAll();
            return View("Index", list);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            var user = userService.GetUserById(id);
            return View("Details", user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View("Create",new User());
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                User user = new User();
                user.Name = collection["Name"];
                user.LoginName = collection["LoginName"];
                user.Role = collection["Role"];
                user.Password = Utils.Encrypt(collection["Password"]);//加密
                var existUser = userService.GetUserByLoginName(user.LoginName);
                if (existUser != null && existUser.LoginName == user.LoginName)
                {
                    ViewBag.Message = "登录名已经存在！";
                    return View(user);
                } 
             
                userService.Save(user); 
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                log.Error(ex);
                return View();
            }
        }

        // GET: User/Update/5
        public ActionResult Update(int id)
        {
            var user = userService.GetUserById(id);
            return View("Update", user); 
        }

        // POST: User/Update/5
        [HttpPost]
        public ActionResult Update(User user)
        {
            try
            {
                var oldUser = userService.GetUserById(user.Id);
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
                log.Error(ex);
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
                log.Error(ex);
                return View();
            }
        }
    }
}
