using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Application;
using WebApplication.Models;
using WebApplication.Common;

namespace WebApplication.Controllers
{
    public class OrderController : Controller
    {
        private OrderService orderService; 

        public OrderController()
        {
            orderService = new OrderService();
        }

        // GET: Order
        public ActionResult Index()
        {
            var orders = orderService.ListAll();
            return View(orders);
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Order/Create/
        [HttpGet]
        public ActionResult Create(string guid)
        {
            if (string.IsNullOrEmpty(guid))
            {
                string errorMessage = "您的链接参数不正确，或者已经超时失效了。";
                return RedirectToAction("Error", "Order", new { message = errorMessage});
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
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: Order/Edit/5
        public ActionResult Update(int id)
        {
            return View();
        }

        // POST: Order/Edit/5
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

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
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

        public ActionResult Add()
        {
            ViewData["ImageTypeList"] = ImageType.GetAll(); 
            OrderForm orderForm = new OrderForm();
            orderForm.ImageTypes = new ImageTypeModel();
            return View(orderForm);
        }

        [HttpPost]
        public ActionResult Add(OrderForm orderForm)
        {
            orderForm.ExpireTime = DateTime.Now.AddMinutes(20);
            orderForm.formGuid = Guid.NewGuid();
            orderForm.URL = string.Format(@"http://{0}\Order\Create?guid={1}", Request.Url.Authority, orderForm.formGuid);
            CacheHelper.SetCache(orderForm.formGuid.ToString(), orderForm.ImageTypes);
            ViewData["ImageTypeList"] = ImageType.GetAll();
            return View(orderForm);
        }

        [HttpGet]
        public ActionResult Error(string message)
        {
            ViewBag.Message = message;
            return View();
        }
    }
}

 