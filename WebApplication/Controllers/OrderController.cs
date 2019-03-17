using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Application;
using Models;
using WebApplication.Common;
using WebApplication.Filters;
using System.IO;

namespace WebApplication.Controllers
{
    public class OrderController : Controller
    {
        private OrderService orderService;
        private log4net.ILog log = log4net.LogManager.GetLogger("OrderController");

        public OrderController()
        {
            orderService = new OrderService();
        }

        // GET: Order
        public ActionResult Index()
        {
            // 管理员和客服可以对订单做任何操作。
            // 生产员只能看到已经审批后的订单。
            List<Order> orders = null;
            if (UserHelper.GetCurrentUser.Role == EnumRole.生产员.ToString())
            {
                orders = orderService.List(new int[] { (int)EnumStatus.待生产, (int)EnumStatus.生产中 });
            }
            else
            {
                orders = orderService.ListAll();
            }
            return View(orders);
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        
        // GET: Order/Edit/5
        //public ActionResult Update(int id, int status)
        //{
        //    Order order = orderService.GetOrderById(id);
        //    order.Status = status;
        //    if (status == (int)EnumStatus.待生产)
        //    {
        //        order.Auditor = 10; //to do, current login user id.
        //        order.AuditTime = DateTime.Now;
        //    }
        //    else if (status == (int)EnumStatus.生产中)
        //    {
        //        order.Productor = 10; //to do, current login user id.
        //        order.ProductTime = DateTime.Now;
        //    }
        //    else if (status == (int)EnumStatus.已删除)
        //    {
        //        order.DeleteTime = DateTime.Now;
        //    }

        //    orderService.Save(order);

        //    return View(order);
        //}

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Update(int id, int status)
        {
            Order order = orderService.GetOrderById(id);
            order.Status = status;
            if (status == (int)EnumStatus.待生产)
            {
                order.Auditor = UserHelper.GetCurrentUser.Id;
                order.AuditTime = DateTime.Now;
            }
            else if (status == (int)EnumStatus.生产中)
            {
                order.Productor = UserHelper.GetCurrentUser.Id;
                order.ProductTime = DateTime.Now;
            }
            else if (status == (int)EnumStatus.已删除)
            {
                order.DeleteTime = DateTime.Now;
            }

            orderService.Save(order); 
            return RedirectToAction("Index");
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
            catch(Exception ex)
            {
                log.Error(ex);
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
            orderForm.URL = string.Format(@"http://{0}\Front\Create?guid={1}", Request.Url.Authority, orderForm.formGuid);
            CacheHelper.SetCache(orderForm.formGuid.ToString(), orderForm.ImageTypes);
            ViewData["ImageTypeList"] = ImageType.GetAll();
            log.Debug($"new order link: {orderForm.URL}");
            return View(orderForm);
        }

        [HttpGet]
        public FileStreamResult DownloadFile(string imageURL)
        {
            string fileName = imageURL.Substring(21, imageURL.Length-21);  //UploadFilesOutputImgs lendth 21 
            string filePath = Server.MapPath("//UploadFiles//OutputImgs//"+ fileName);

            return File(new FileStream(filePath, FileMode.Open), "application/octet-stream", Server.UrlEncode(fileName));
        }
         
    }
}

 