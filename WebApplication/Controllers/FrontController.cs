using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Application;
using Models;
using WebApplication.Common;
using WebApplication.Filters;

namespace WebApplication.Controllers
{
    [SkipCheckLogin]
    public class FrontController : Controller
    {
        private OrderService orderService;
        private SampleService sampleService;

        public FrontController()
        {
            orderService = new OrderService();
            sampleService = new SampleService();
        }

        // GET: Order/Create/  进入订单页面，先筛选，选择sample。
        [HttpGet]
        public ActionResult Create(string guid)
        {
            if (string.IsNullOrEmpty(guid))
            {
                string errorMessage = "您的链接参数不正确，或者已经超时失效了。";
                return RedirectToAction("Error", "Front", new { message = errorMessage });
            }
            ImageTypeModel imageTypeModel = (ImageTypeModel)CacheHelper.GetCache(guid);

            List<ImageType> imageTypeList = new List<ImageType>();
            foreach (var item in imageTypeModel.ImageType)
            {
                imageTypeList.Add(ImageType.GetAll().Find(t => t.Id == item));
            }
            ViewBag.imageTypes = imageTypeList;

            Order order = new Order();
            return View(order);
        } 

        //选完方案后，进入订单页面，输入文字，然后提交订单。
        public ActionResult Create2(int type, int style, bool ifHasBgImage, string guid, int sampleId)
        {
            if (string.IsNullOrEmpty(guid) || CacheHelper.GetCache(guid) == null)
            {
                string errorMessage = "您的链接参数不正确，或者已经超时失效了。";
                return RedirectToAction("Error", "Front", new { message = errorMessage });
            }
            Order order = new Order();
            order.SampleId = sampleId;
            Sample sample = sampleService.GetSample(sampleId, false);
            order.Sample = sample;
            return View(order);
        }
        // POST: Order/Create 输入文字之后，提交订单
        [HttpPost]
        public ActionResult Create2(Order order)
        {
            try
            {
                order.SubmitTime = DateTime.Now;
                order.Status = (int)EnumStatus.待审批;
                order.SubmitTime = DateTime.Now;
                order.ProductTime = DateTime.MinValue;
                order.AuditTime = DateTime.MinValue;
                order.DeleteTime = DateTime.MinValue;
                order.ImageUrl = string.Empty;
                // todo ...
                orderService.Save(order); // todo
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        //进入Sample列表页面
        public ActionResult Samples(int type, int style, bool ifHasBgImage, string retURL)
        {
            //把用户选过的值传过来，选完之后再传回去。
            List<Sample> samples = null;
            samples = sampleService.ListAll((EnumImageType)type, (EnumImageStyle)style, false); // to do.. 是否有背景图案
            ViewBag.type1 = type;
            ViewBag.style1 = style;
            ViewBag.ifHasBgImage1 = ifHasBgImage;
            ViewBag.retURL1 = retURL.Substring(retURL.IndexOf("=")+1, 36);
            return View(samples);
        }


        [HttpGet]
        public ActionResult Error(string message)
        {
            ViewBag.Message = message;
            return View();
        }
    }
}
