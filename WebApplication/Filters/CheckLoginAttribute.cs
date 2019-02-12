using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;

namespace WebApplication.Filters
{
    public class CheckLoginAttribute : ActionFilterAttribute
    {
        //public override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    if (filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(SkipCheckLoginAttribute), false))
        //    {
        //        return;
        //    }
        //    if (filterContext.HttpContext.Session["User"] == null)
        //    {
        //        filterContext.HttpContext.Response.Redirect("/Login/Login");
        //    }
        //}

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(SkipCheckLoginAttribute), false))
            {
                base.OnActionExecuting(filterContext);
                return;
            }
            if ( filterContext.HttpContext.Session["User"] == null)
            {
                ////如果cookie中，从cookie中还原一份到session中
                //HttpCookie cookie = HttpContext.Current.Request.Cookies["cookieUser"]; 
                //if (cookie != null)
                //{
                //    if (cookie.Value != "")
                //    {
                //        User user = new User
                //        {
                //            Id = int.Parse(cookie["Id"].ToString()),
                //            Name = cookie["Name"].ToString(),
                //            LoginName = cookie["LoginName"].ToString(),
                //            Role = cookie["Role"].ToString()
                //        };
                //        filterContext.HttpContext.Session["User"] = user;
                //        base.OnActionExecuting(filterContext);
                //        return;
                //    }
                //}
                 
                var Url = new UrlHelper(filterContext.RequestContext);
                var url = Url.Action("Login", "Login", new { area = "" });
                filterContext.Result = new RedirectResult(url);
            }
            base.OnActionExecuting(filterContext);
        }
    }
}