using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Filters;
using WebApplication.Application;
using Models;
using WebApplication.Common;

namespace WebApplication.Controllers
{
    //[SkipCheckLogin]
    public class ImageFontController : Controller
    {
        private ImageFontService imageFontService;
        private LogService logService;
        private log4net.ILog log = log4net.LogManager.GetLogger("ImageFontController");

        public ImageFontController()
        {
            logService = new LogService();
            imageFontService = new ImageFontService();
        }

        public ActionResult Index()
        {
            List<ImageFont> imageFonts = imageFontService.GetAll();
            return View("Index", imageFonts);
        }

        public ActionResult UploadFile()
        {
            return View();
        }

        /// <summary>
        /// 附件上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadFile(FormCollection collection, string type)
        {
            if (type == "UploadFile")
            {
                try
                {
                    HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
                    if (files.Count == 0)
                        return Json(new { status = "Fail", message = "请先上传文件！"}, JsonRequestBehavior.AllowGet); 
                    var fullFileName = $"/UploadFiles/FontFiles/{Guid.NewGuid() + "_" + files[0].FileName}";
                    if (!System.IO.File.Exists(fullFileName))
                    {
                        files[0].SaveAs(Server.MapPath(fullFileName));
                    } 
                    return Content(fullFileName);
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                    return Json(new { status = "Fail", message = ex.Message}, JsonRequestBehavior.AllowGet);
                } 
            }

            try
            {
                ImageFont imageFont = new ImageFont();
                imageFont.name = collection["Name"];
                imageFont.ifSystem = false;
                var ifSystem = int.Parse(collection["ifSystem"]);
                if (0 == ifSystem) //非系统字体
                {
                    imageFont.ifSystem = false;
                    imageFont.url = collection["imageFont"];
                    if (string.IsNullOrEmpty(imageFont.url))
                    {
                        throw new Exception("请先上传字体文件！");
                    }
                }
                else//系统字体
                {
                    imageFont.ifSystem = true;
                    imageFont.url = "";
                }
                imageFontService.Add(imageFont);
                var logs = new Logs { Action = EnumAction.新建字体, Detail = imageFont.name, UserId = UserHelper.GetCurrentUser.Id, Time = DateTime.Now };
                logService.Insert(logs);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return View();
            }
        }
        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private string GetFileSize(long bytes)
        {
            long kblength = 1024;
            long mbLength = 1024 * 1024;
            if (bytes < kblength)
                return bytes.ToString() + "B";
            if (bytes < mbLength)
                return decimal.Round(decimal.Divide(bytes, kblength), 2).ToString() + "KB";
            else
                return decimal.Round(decimal.Divide(bytes, mbLength), 2).ToString() + "MB";
        }
         
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                imageFontService.Delete(id);
                var logs = new Logs { Action = EnumAction.删除字体, Detail = "id:"+id, UserId = UserHelper.GetCurrentUser.Id, Time = DateTime.Now };
                logService.Insert(logs);
                return Content("success");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return Json(new { status = "Fail", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}