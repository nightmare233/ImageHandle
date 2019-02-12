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

        public FrontController()
        {
            orderService = new OrderService();
        }

        // GET: Order/Create/
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

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(Order order)
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
                orderService.Save(order);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public ActionResult Error(string message)
        {
            ViewBag.Message = message;
            return View();
        }
    }
}
