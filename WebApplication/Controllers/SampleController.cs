using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Application;
using Models;
using WebApplication.Filters;

namespace WebApplication.Controllers
{
    //[SkipCheckLogin]
    public class SampleController : Controller
    {
        private SampleService sampleService;
        private ImageFontService imageFontService;
        private log4net.ILog log = log4net.LogManager.GetLogger("SampleController");

        public SampleController()
        {
            sampleService = new SampleService();
            imageFontService = new ImageFontService();
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

        // GET: Sample
        //public ActionResult Index()
        //{
        //    List<Sample> samples = sampleService.ListAll(null, null, null, null, true);
        //    return View(samples);
        //}

        //GET: Query
        public ActionResult Index(FormCollection collection)  //int imageType, int style, int ifHasBgImage, string keywords
        {
            List<Sample> samples = null;
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
                    samples = sampleService.ListAll(enumImageType, enumImageStyle, booLIfHasBgImage, keywords, true);
                }
                else
                {
                    samples = sampleService.ListAll(null, null, null, null, true);
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
            
                List<ImageText> mainTexts = new List<ImageText>();
                ImageText imageText = null;
                ImageFont imageFont = null;
                for (int i = 1; i < 5; i++)
                {
                    if (!string.IsNullOrEmpty(collection["Text" + i]))
                    {
                        imageText = new ImageText();
                        imageText.Text = collection["Text" + i];
                        int fontId = int.Parse(collection["Font" + i]);
                        imageFont = imageFontService.GetById(fontId);
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

                if (!string.IsNullOrEmpty(collection["Text5"]))  //small text
                {
                    imageText = new ImageText();
                    imageText.Text = collection["Text5"];
                    int fontId = int.Parse(collection["Font5"]);
                    imageFont = imageFontService.GetById(fontId);
                    imageText.Font = imageFont.name;
                    imageText.imageFont = imageFont;
                    imageText.FontSize = int.Parse(collection["FontSize5"]);
                    imageText.PositionX = int.Parse(collection["PositionX5"]);
                    imageText.PositionY = int.Parse(collection["PositionY5"]);
                    imageText.Type = (int)EnumTextType.SmallText;
                    imageText.Order = Convert.ToBoolean(int.Parse(collection["FontOrder"]));
                    sample.IfHasSmallText = true;
                    sample.SmallText = imageText;
                }
                 
                #endregion
                if (type == "保存")
                {
                    if (string.IsNullOrEmpty(sample.ImageUrl))
                    {
                        throw new Exception("请先生成样式图片！");
                    }
                    sampleService.Insert(sample);
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
         

        // GET: Sample/Edit/5
        public ActionResult Update(int id)
        {
            Sample sample = sampleService.GetSample(id, true);
            InitData();
          
            List<SelectListItem> sli = new List<SelectListItem>();
            if (sample.IfHasBgImg)
            {
                sli.Add(new SelectListItem() { Value = "0", Text = "无背景图" });
                sli.Add(new SelectListItem() { Value = "1", Text = "有背景图", Selected = true });

            }
            else
            {
                sli.Add(new SelectListItem() { Value = "0", Text = "无背景图", Selected = true });
                sli.Add(new SelectListItem() { Value = "1", Text = "有背景图" });
            }
           var a = sli.Select(t => t.Value == sample.IfHasBgImg.ToString()).FirstOrDefault();
            ViewBag.IfHasBgImg = sli;
            return View(sample);
        }

        // POST: Sample/Edit/5
        [HttpPost]
        public ActionResult Update(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
    }
}
