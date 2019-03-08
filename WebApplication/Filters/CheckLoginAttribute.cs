using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using WebApplication.Common;

namespace WebApplication.Filters
{
    public class CheckLoginAttribute : ActionFilterAttribute
    {
      
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(SkipCheckLoginAttribute), false))
            {
                base.OnActionExecuting(filterContext);
                return;
            }
            if (filterContext.HttpContext.Session["User"] == null)
            { 
                var Url = new UrlHelper(filterContext.RequestContext);
                var url = Url.Action("Login", "Login", new { area = "" });
                filterContext.Result = new RedirectResult(url);
            }
            else
            {
                var role = UserHelper.GetCurrentUser.Role;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}