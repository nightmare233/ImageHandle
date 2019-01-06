using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            if ( filterContext.HttpContext.Session["LoginUser"] == null || filterContext.HttpContext.Session["LoginUser"].ToString() == "")
            {
                HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies["cookieUser"];

                if (cookie != null)
                {
                    if (cookie.Value != "")
                    {
                        string loginUser = cookie["LoginUser"];
                        if (loginUser!= null && loginUser != "")
                        {
                            filterContext.HttpContext.Session["LoginUser"] = loginUser;
                            base.OnActionExecuting(filterContext);
                            return;
                        }
                    }
                }
                 
                var Url = new UrlHelper(filterContext.RequestContext);
                var url = Url.Action("Login", "Login", new { area = "" });
                filterContext.Result = new RedirectResult(url);
            }
            base.OnActionExecuting(filterContext);
        }
    }
}