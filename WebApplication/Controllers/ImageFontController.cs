using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Filters;
using WebApplication.Application;
using Models;

namespace WebApplication.Controllers
{
    [SkipCheckLogin]
    public class ImageFontController : Controller
    {
        private ImageFontService imageFontService;

        public ImageFontController()
        {
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
                HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
                if (files.Count == 0)
                    return Json("Faild", JsonRequestBehavior.AllowGet);
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
                var fullFileName = $"/UploadFiles/FontFiles/{Guid.NewGuid() + "_" + files[0].FileName}";
                if (!System.IO.File.Exists(fullFileName))
                {
                    files[0].SaveAs(Server.MapPath(fullFileName));
                }
                //string fileName = files[0].FileName.Substring(files[0].FileName.LastIndexOf("\\") + 1, files[0].FileName.Length - files[0].FileName.LastIndexOf("\\") - 1);
                //string fileSize = GetFileSize(files[0].ContentLength);
                return Content(fullFileName);
            }
        
            ImageFont imageFont = new ImageFont();
            imageFont.name = collection["Name"];
            imageFont.ifSystem = false;
            var ifSystem = int.Parse(collection["ifSystem"]);
            if(0 == ifSystem)
            {
                imageFont.ifSystem = false;
                imageFont.url = collection["imageFont"];
            }
            else
            {
                imageFont.ifSystem = true;
                imageFont.url = ""; 
            }
            imageFontService.Add(imageFont);
            return RedirectToAction("Index");
      
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
                return Content("success");
            }
            catch (Exception ex)
            {
                return Json("Faild," + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}