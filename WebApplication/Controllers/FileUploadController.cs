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
        public ActionResult UploadFile()
        {
            return View();
        }

        /// <summary>
        /// 附件上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadFile(FormCollection collection)
        {
            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            if (files.Count == 0) return Json("Faild", JsonRequestBehavior.AllowGet);
            //MD5 md5Hasher = new MD5CryptoServiceProvider();
            ///*计算指定Stream对象的哈希值*/
            //byte[] arrbytHashValue = md5Hasher.ComputeHash(files[0].InputStream);
            ///*由以连字符分隔的十六进制对构成的String，其中每一对表示value中对应的元素；例如“F-2C-4A”*/
            //string strHashData = System.BitConverter.ToString(arrbytHashValue).Replace("-", "");
            //string FileEextension = Path.GetExtension(files[0].FileName);
            //string uploadDate = DateTime.Now.ToString("yyyyMMdd");
            //string virtualPath = string.Format("/Data/ComponentAttachments/{0}/{1}{2}", uploadDate, strHashData, FileEextension);
            //string fullFileName = Server.MapPath(virtualPath);
            ////创建文件夹，保存文件
            //string path = Path.GetDirectoryName(fullFileName);
            //Directory.CreateDirectory(path);
            var fullFileName = $"/BgImages/{files[0].FileName + "_" + Guid.NewGuid()}";
            if (!System.IO.File.Exists(fullFileName))
            {
                files[0].SaveAs(Server.MapPath(fullFileName));
            }
            string fileName = files[0].FileName.Substring(files[0].FileName.LastIndexOf("\\") + 1, files[0].FileName.Length - files[0].FileName.LastIndexOf("\\") - 1);
            string fileSize = GetFileSize(files[0].ContentLength);
            return Json(new { FileName = fileName, FileSize = fileSize }, "text/html", JsonRequestBehavior.AllowGet);
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