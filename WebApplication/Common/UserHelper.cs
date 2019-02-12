using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace WebApplication.Common
{
    public class UserHelper
    {
        public User GetCurrentUser
        {
            get
            {
                return HttpContext.Current.Session["User"] as User;
            }     
            
        }
    }
}