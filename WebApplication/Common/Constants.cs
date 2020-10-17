using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web; 

namespace WebApplication.Common
{
    public static class Constants
    {
        public static int smallTextLimits = 15;
        public static string ImageExtension =  ConfigurationManager.AppSettings["imageExtension"].ToLower();
    }
}