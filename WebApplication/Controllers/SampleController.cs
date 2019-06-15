using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Application;
using Models;
using WebApplication.Filters;
using WebApplication.Common;

namespace WebApplication.Controllers
{
    //[SkipCheckLogin]
    public class SampleController : Controller
    {
        private SampleService sampleService;
        private ImageFontService imageFontService;
        private LogService logService;
        private log4net.ILog log = log4net.LogManager.GetLogger("SampleController");

        public SampleController()
        {
            sampleService = new SampleService();
            imageFontService = new ImageFontService();
            logService = new LogService();
        }

        private void InitData()
        { 
            //font list
            List<ImageFont> imageFonts = imageFontService.GetAll();
            List<SelectListItem> fontList = new List<SelectListItem>();
            foreach(var item in imageFonts)
            {
                fontList.Add(new SelectListItem { Text = item.name, Value = item.id.ToString() }); 
            }
            ViewBag.FontList = fontList;
            //type list
            List<ImageType> imageTypeList = ImageType.GetAll();
            ViewBag.imageTypes = imageTypeList;
            ViewBag.ImageUrl = "";
        }
         
        //GET: Query
        public ActionResult Index(FormCollection collection)  //int imageType, int style, int ifHasBgImage, string keywords
        {
            List<Sample> samples = null;
            InitData();
            try
            {
                if (collection.Count > 0)
                {
                    EnumImageType? enumImageType = null;
                    EnumImageStyle? enumImageStyle = null;
                    int imageType = int.Parse(collection["ImageType"]);
                    int style = int.Parse(collection["Style"]);
                    int ifHasBgImage = int.Parse(collection["IfHasBgImage"]);
                    string keywords = collection["Keywords"];
                    string font = collection["Font"].ToString();
                    if (!string.IsNullOrEmpty(font))
                    {
                       int fontId = int.Parse(font);
                       var Font = imageFontService.GetById(fontId);
                       font = Font.name;
                    }
                
                    bool? booLIfHasBgImage = null;
                    if (imageType != 1)
                    {
                        enumImageType = (EnumImageType)imageType;
                    }
                    if (style != -1)
                    {
                        enumImageStyle = (EnumImageStyle)style;
                    }
                    if (ifHasBgImage != -1)
                    {
                        booLIfHasBgImage = Convert.ToBoolean(ifHasBgImage);
                    }
                    samples = sampleService.ListAll(enumImageType, enumImageStyle, booLIfHasBgImage, keywords, true, font, 100, 0);
                }
                else
                {
                    samples = sampleService.ListAll(null, null, null, null, true, "", 100, 0);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return View(samples);
        }

        // GET: Sample/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Sample/Create
        public ActionResult Create()
        {
            InitData();
            return View();
        }

        // POST: create sample
        [HttpPost]
        public ActionResult Create(FormCollection collection, string type)
        {
            try
            {
                #region init sample data 
                Sample sample = new Sample();
                sample.IfHasBgImg = Convert.ToBoolean(int.Parse(collection["IfHasBgImage"]));
                if (!sample.IfHasBgImg)
                    sample.BgImage = "";
                else
                    sample.BgImage = collection["BgImage"];
                if (type == "UploadFile")  //处理上传背景图片
                {
                    HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
                    if (files.Count == 0)
                    {
                        return Json(new { status = "Fail", message = "请先上传文件！" }, JsonRequestBehavior.AllowGet);
                    }
                    try
                    {
                        var fullFileName = $"/UploadFiles/BgImages/{Guid.NewGuid() + "_" + files[0].FileName}";
                        if (!System.IO.File.Exists(fullFileName))
                        {
                            files[0].SaveAs(Server.MapPath(fullFileName));
                        }
                        sample.BgImage = fullFileName;
                        sample.IfHasBgImg = true;
                        return Json(fullFileName);
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex.Message);
                        return Json(new { status = "Fail", message = ex.Message }, JsonRequestBehavior.AllowGet);
                    }

                }

                sample.Name = collection["Name"];
                sample.ImageType = (EnumImageType)int.Parse(collection["ImageType"]);
                sample.Style = (EnumImageStyle)int.Parse(collection["Style"]);
                sample.ImageUrl = collection["ImageUrl"];
                string sizeStr = collection["Size"];
                sample = Utils.SetSize(sizeStr, sample);

                List<ImageText> mainTexts = new List<ImageText>();
                List<ImageText> smallTexts = new List<ImageText>();
                ImageText imageText = null;
                ImageFont imageFont = null;
                int fontId = int.Parse(collection["Font"]);
                imageFont = imageFontService.GetById(fontId);
                sample.Font = imageFont.name;//第一个字的字体作为sample的字体，用于搜索过滤。
                sample.ImageFont = imageFont;

                var mainTextArray = collection.AllKeys.Where(t => t.StartsWith("Text"));  //把所有Text开头的拿出来，遍历。
                foreach (string str in mainTextArray)
                {
                    int i = Convert.ToInt32(str.Substring(4, 1)); //Text1 取后面的数字
                    string text = collection["Text" + i];
                    if (!string.IsNullOrEmpty(text))
                    {
                        imageText = new ImageText();
                        imageText.Text = text;
                        imageText.Font = imageFont.name;
                        imageText.imageFont = imageFont;
                        imageText.FontSize = int.Parse(collection["FontSize" + i]);
                        imageText.PositionX = int.Parse(collection["PositionX" + i]);
                        imageText.PositionY = int.Parse(collection["PositionY" + i]);
                        imageText.Type = (int)EnumTextType.MainText;
                        imageText.Order = true;
                        mainTexts.Add(imageText);
                    }
                }
                sample.MainText = mainTexts;
                sample.MainTextNumber = mainTexts.Count; 
                var smallTextArray = collection.AllKeys.Where(t => t.StartsWith("SmallText"));  //把所有SmallText开头的拿出来，遍历。
                foreach (string str in smallTextArray)
                {
                    int i = Convert.ToInt32(str.Substring(9, 1)); //SmallText1 取后面的数字
                    string text = collection["SmallText" + i];
                    if (!string.IsNullOrEmpty(text))  //small text
                    {
                        imageText = new ImageText();
                        imageText.Text = text;
                        imageText.Font = imageFont.name;
                        imageText.imageFont = imageFont;
                        imageText.FontSize = int.Parse(collection["SmallFontSize" + i]);
                        imageText.PositionX = int.Parse(collection["SmallPositionX" + i]);
                        imageText.PositionY = int.Parse(collection["SmallPositionY" + i]);
                        imageText.Type = (int)EnumTextType.SmallText;
                        imageText.Order = Convert.ToBoolean(int.Parse(collection["SmallFontOrder" + i]));
                        smallTexts.Add(imageText);
                    }
                }
                sample.SmallText = smallTexts;
                if(smallTexts.Count>0)
                {
                    sample.IfHasSmallText = true;
                    sample.SmallTextNumber = smallTexts.Count;
                }
                
                #endregion
                if (type == "保存")
                {
                    if (string.IsNullOrEmpty(sample.ImageUrl))
                    {
                        throw new Exception("请先生成样式图片！");
                    }
                    sampleService.Insert(sample);
                    var logs = new Logs { Action = EnumAction.新建预设样式, Detail = sample.Name, UserId = UserHelper.GetCurrentUser.Id, Time = DateTime.Now };
                    logService.Insert(logs);
                    return RedirectToAction("Index");
                }
                else if (type == "CreateImage")
                {
                    try
                    {
                        InitData();
                        if (!CheckNameUnique(sample.Name))
                        {
                            return Json(new { status = "Fail", message = "该名称已经存在，请使用唯一的名称！" }, JsonRequestBehavior.AllowGet);
                        }
                        string imageUrl = ImageHelp.CreateImage(sample, true, null);
                        return Json(imageUrl);
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex.Message);
                        return Json(new { status = "Fail", message = ex.Message }, JsonRequestBehavior.AllowGet);
                    } 
                }
                return View();
            }
            catch (Exception ex)
            {
                InitData();
                ViewBag.Message = ex.Message;
                log.Error(ex);
                return View("Create");
            }
        }

        public ActionResult Update(int id)
        {
            Sample sample = sampleService.GetSample(id, true);
            InitData();
           
            ViewBag.VBIfHasBgImg = sample.IfHasBgImg?"1":"0";
            string font = sample.Font; 
            ViewBag.Font = font;
            //if (sample.IfHasSmallText)
            //{
            //    ViewBag.VBSmallFont = sample.SmallText.Font;
            //    ViewBag.VBSmallOrder = sample.SmallText.Order;
            //}
            //else
            //{
            //    ViewBag.VBSmallFont = "1";
            //    ViewBag.VBSmallOrder = "1";
            //}
            ViewBag.VBSmallFont = "1";  //todo 
            ViewBag.VBSmallOrder = "1";
            return View(sample);
        }

        [HttpPost]
        public ActionResult Update(FormCollection collection, string type)
        {
            try
            {
                #region init sample data 
                int sampleId = int.Parse(collection["ID"]);
                Sample sample = sampleService.GetSample(sampleId, true);
                sample.IfHasBgImg = Convert.ToBoolean(int.Parse(collection["DDLIfHasBgImg"]));
                if (!sample.IfHasBgImg)
                    sample.BgImage = "";
                else
                    sample.BgImage = collection["BgImage"];
                if (type == "UploadFile")  //处理上传背景图片
                {
                    HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
                    if (files.Count == 0)
                    {
                        return Json(new { status = "Fail", message = "请先上传文件！" }, JsonRequestBehavior.AllowGet);
                    }
                    try
                    {
                        var fullFileName = $"/UploadFiles/BgImages/{Guid.NewGuid() + "_" + files[0].FileName}";
                        if (!System.IO.File.Exists(fullFileName))
                        {
                            files[0].SaveAs(Server.MapPath(fullFileName));
                        }
                        sample.BgImage = fullFileName;
                        sample.IfHasBgImg = true;
                        return Json(fullFileName);
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex.Message);
                        return Json(new { status = "Fail", message = ex.Message }, JsonRequestBehavior.AllowGet);
                    }
                }

                sample.Name = collection["Name"];
                sample.ImageUrl = collection["ImageUrl"];
                 
                List<ImageText> mainTexts = new List<ImageText>();
                ImageText imageText = null;
                ImageFont imageFont = null;
                int fontId = int.Parse(collection["Font"]);
                imageFont = imageFontService.GetById(fontId);
                sample.Font = imageFont.name;//第一个字的字体作为sample的字体，用于搜索过滤。
                sample.ImageFont = imageFont;
                string sizeStr = collection["Size"];
                sample = Utils.SetSize(sizeStr, sample);
                for (int i = 1; i < 5; i++)
                {
                    if (!string.IsNullOrEmpty(collection["Text" + i]))
                    {
                        imageText = new ImageText();
                        imageText.Text = collection["Text" + i]; 
                        imageText.Font = imageFont.name;
                        imageText.imageFont = imageFont;
                        imageText.FontSize = int.Parse(collection["FontSize" + i]);
                        imageText.PositionX = int.Parse(collection["PositionX" + i]);
                        imageText.PositionY = int.Parse(collection["PositionY" + i]);
                        imageText.Type = (int)EnumTextType.MainText;
                        imageText.Order = true;
                        mainTexts.Add(imageText);
                    }
                }
                sample.MainText = mainTexts;
                sample.MainTextNumber = mainTexts.Count;
                // todo
                //if (!string.IsNullOrEmpty(collection["Text5"]))  //small text 
                //{
                //    imageText = new ImageText();
                //    imageText.Text = collection["Text5"]; 
                //    imageText.Font = imageFont.name;
                //    imageText.imageFont = imageFont;
                //    imageText.FontSize = int.Parse(collection["FontSize5"]);
                //    imageText.PositionX = int.Parse(collection["PositionX5"]);
                //    imageText.PositionY = int.Parse(collection["PositionY5"]);
                //    imageText.Type = (int)EnumTextType.SmallText;
                //    imageText.Order = Convert.ToBoolean(int.Parse(collection["FontOrder"]));
                //    sample.IfHasSmallText = true;
                //    sample.SmallText = imageText;
                //}

                #endregion
                if (type == "保存")
                {
                    if (string.IsNullOrEmpty(sample.ImageUrl))
                    {
                        throw new Exception("请先生成样式图片！");
                    }
                    sampleService.Update(sample);
                    return RedirectToAction("Index");
                }
                else if (type == "CreateImage")
                {
                    try
                    {
                        InitData();
                        if (!CheckUpdateNameUnique(sample.Name, sample.Id))
                        {
                            return Json(new { status = "Fail", message = "该名称已经存在，请使用唯一的名称！" }, JsonRequestBehavior.AllowGet);
                        }
                        string imageUrl = ImageHelp.CreateImage(sample, true, null);
                        return Json(imageUrl);
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex.Message);
                        return Json(new { status = "Fail", message = ex.Message }, JsonRequestBehavior.AllowGet);
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                InitData();
                ViewBag.Message = ex.Message;
                log.Error(ex);
                return View("Update");
            }
        }

        // GET: Sample/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: delete sample
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var sample = sampleService.GetSample(id, false);
                string path = AppDomain.CurrentDomain.BaseDirectory + $"\\UploadFiles\\SampleImgs\\{sample.ImageUrl}";
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                sampleService.Delete(id);
                var logs = new Logs { Action = EnumAction.删除预设样式, Detail = sample.Name, UserId = UserHelper.GetCurrentUser.Id, Time = DateTime.Now };
                logService.Insert(logs);
                return Content("success");
            }
            catch(Exception ex)
            {
                log.Error(ex);
                return Json("Fail" + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        private bool CheckNameUnique(string name)
        {
            var sample = sampleService.GetSampleByName(name, false);
            if (sample == null)
                return true;
            return false;
        }

        private bool CheckUpdateNameUnique(string name, int sampleId)
        {
            var sample = sampleService.GetSampleByName(name, false);
            if (sample == null)
                return true;
            else
            {
                if (sample.Id == sampleId) return true;
            }
            return false;
        }
    }
}
