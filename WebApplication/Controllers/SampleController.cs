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
    [SkipCheckLogin]
    public class SampleController : Controller
    {
        private SampleService sampleService;
        private ImageFontService imageFontService;

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
        public ActionResult Index()
        {
            List<Sample> samples = sampleService.ListAll(null, null, null, true);
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

        // POST: Sample/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, string type)
        {
            try
            {
                #region init sample data
                Sample sample = new Sample();
                sample.Name = collection["Name"];
                sample.ImageType = (EnumImageType)int.Parse(collection["ImageType"]);
                sample.Style = (EnumImageStyle)int.Parse(collection["Style"]);
                sample.IfHasBgImg = Convert.ToBoolean(int.Parse(collection["IfHasBgImage"]));
                sample.ImageUrl = collection["ImageUrl"];
                if (sample.IfHasBgImg)
                    sample.BgImage = ""; //todo. 上传
                else
                    sample.BgImage = "";

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
                        if (imageFont.ifSystem)
                        {
                            imageText.Font = imageFont.name;
                        }
                        else
                        {
                            imageText.Font = imageFont.url;  //系统字体存名字，非系统字体存地址。
                        }
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
                #endregion

                if (type == "Save")
                {
                    sampleService.Insert(sample);
                    return RedirectToAction("Index");
                }
                else if (type == "CreateImage")
                {
                    InitData();
                    string imageUrl = ImageHelp.CreateImage(sample, true);
                    var result  = Json(imageUrl);
                    return result;
                }
                return View();
            }
            catch (Exception ex)
            {
                InitData();
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult CreateImage(FormCollection collection)
        {
            try
            {
                #region init sample data
                Sample sample = new Sample();
                sample.Name = collection["Name"];
                sample.ImageType = (EnumImageType)int.Parse(collection["ImageType"]);
                sample.Style = (EnumImageStyle)int.Parse(collection["Style"]);
                sample.IfHasBgImg = Convert.ToBoolean(int.Parse(collection["IfHasBgImage"]));
                sample.ImageUrl = collection["ImageUrl"];
                if (sample.IfHasBgImg)
                    sample.BgImage = ""; //todo. 上传
                else
                    sample.BgImage = "";

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
                        if (imageFont.ifSystem)
                        {
                            imageText.Font = imageFont.name;
                        }
                        else
                        {
                            imageText.Font = imageFont.url;  //系统字体存名字，非系统字体存地址。
                        }
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
                #endregion
 
                string imageUrl = ImageHelp.CreateImage(sample, true);
                return Content(imageUrl);
            }
            catch (Exception ex)
            {
                InitData();
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: Sample/Edit/5
        public ActionResult Update(int id)
        {
            return View();
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

        // POST: Sample/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
