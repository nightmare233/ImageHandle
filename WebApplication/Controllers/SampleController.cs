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

        public SampleController()
        {
            sampleService = new SampleService();
        }

        private void InitData()
        { 
            List<ImageType> imageTypeList = ImageType.GetAll();

            List<SelectListItem> fontList = new List<SelectListItem>();
            fontList.Add(new SelectListItem { Text = "Arial", Value = "1", Selected = true });
            fontList.Add(new SelectListItem { Value = "2", Text = "MS Yahei" });
            fontList.Add(new SelectListItem { Value = "3", Text = "Fangzheng" });
            fontList.Add(new SelectListItem { Value = "4", Text = "Canala" });

            ViewBag.imageTypes = imageTypeList;
            ViewBag.FontList = fontList;
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
        public ActionResult Create(FormCollection collection, string action)
        {
            try
            {
                #region init sample data
                Sample sample = new Sample();
                sample.ImageType = (EnumImageType)int.Parse(collection["ImageType"]);
                sample.Style = (EnumImageStyle)int.Parse(collection["Style"]);
                sample.IfHasBgImg = Convert.ToBoolean(int.Parse(collection["IfHasBgImage"]));

                List<ImageText> mainTexts = new List<ImageText>();
                ImageText imageText = null;
                for (int i = 1; i < 5; i++)
                {
                    if (!string.IsNullOrEmpty(collection["Text" + i]))
                    {
                        imageText = new ImageText();
                        imageText.Text = collection["Text" + i];
                        imageText.Font = collection["Font" + i];  //todo  convert
                        imageText.FontSize = int.Parse(collection["FontSize" + i]);
                        imageText.PositionX = int.Parse(collection["PositionX" + i]);
                        imageText.PositionY = int.Parse(collection["PositionY" + i]);
                        imageText.Type = (int)EnumTextType.MainText;
                        imageText.Order = true;
                        mainTexts.Add(imageText);
                    }
                }
                 
                sample.MainText = mainTexts;
                #endregion
                 
                if (action == "Save")
                {

                }
                else if (action == "CreateImage")
                {
                
                } 
                return RedirectToAction("Index");
            }
            catch(Exception ex)
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
