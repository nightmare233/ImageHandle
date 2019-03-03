using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Filters;

namespace WebApplication.Controllers
{
    [SkipCheckLogin]
    public class FileUploadController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AjaxFileUpload(HttpPostedFileBase file)
        {
            //var myfile = Request.Files["file"]; //也可以拿到
            var path = $"/BgImages/{file.FileName + "_" + Guid.NewGuid()}";
            file.SaveAs(Server.MapPath(path));      //保存文件
            return Content(path);
        }

        //[HttpPost]
        //public ActionResult AjaxFileUpload()
        //{
        //    //var myfile = Request.Files["file"]; //也可以拿到
        //    //var path = $"/BgImages/{file.FileName + "_" + Guid.NewGuid()}";
        //    //file.SaveAs(Server.MapPath(path));      //保存文件
        //    //return Content(path);
        //    return Content("ddd");
        //}
    }
}