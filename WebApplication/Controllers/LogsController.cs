using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Application;
using WebApplication.Common;
using Models;

namespace WebApplication.Controllers
{
    public class LogsController : Controller
    {
        private LogService _logsService;

        public LogsController()
        {
            _logsService = new LogService();
        }
         
        public ActionResult Index()
        {
            string beginDateStr = Request["begindate"];
            string endDateStr = Request["enddate"];
            DateTime beginDate = DateTime.Now.AddDays(-3);
            DateTime endDate = DateTime.Now;
            if (!string.IsNullOrEmpty(beginDateStr))
            {
                beginDate = DateTime.Parse(beginDateStr);
            }
            if (!string.IsNullOrEmpty(endDateStr))
            {
                endDate = DateTime.Parse(endDateStr);
            }
            UserService userService = new UserService();
            List<Logs> logs = _logsService.ListAll(beginDate, endDate);
            logs.ForEach(t => t.UserName = userService.GetUserById(t.UserId).Name);
            ViewBag.count = logs.Count;
            return View(logs);
        }
         
    }
}
