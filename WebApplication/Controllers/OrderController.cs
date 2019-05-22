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
        private SampleService sampleService;
        private ImageFontService imageFontService;
        private log4net.ILog log = log4net.LogManager.GetLogger("OrderController");

        public OrderController()
        {
            sampleService = new SampleService();
            orderService = new OrderService();
            imageFontService = new ImageFontService();
        }

        // GET: Order
        public ActionResult Index(int lastdays = 1)
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
                orders = orderService.ListAll(lastdays);
            }
            ViewBag.lastdays = lastdays;
            ViewBag.count = orders.Count;
            return View(orders);
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
         

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Update(int id, int status)
        {
            try
            {
                Order order = orderService.GetOrderById(id);
                order.Status = status;
                if (status == (int)EnumStatus.待生产) //审批
                {
                    if (UserHelper.GetCurrentUser.Role == EnumRole.生产员.ToString())
                    {
                        log.Error("生产员试图点击审批！");
                    }
                    order.Auditor = UserHelper.GetCurrentUser.Id;
                    order.AuditTime = DateTime.Now;
                }
                else if (status == (int)EnumStatus.生产中)  //生产
                {
                    if (UserHelper.GetCurrentUser.Role == EnumRole.客服.ToString())
                    {
                        log.Error("客服试图点击生产！");
                    }
                    order.Productor = UserHelper.GetCurrentUser.Id;
                    order.ProductTime = DateTime.Now;
                }
                else if (status == (int)EnumStatus.已完成)
                {
                    if (UserHelper.GetCurrentUser.Role == EnumRole.客服.ToString())
                    {
                        log.Error("客服试图点击完成！");
                    }
                }
                else if (status == (int)EnumStatus.已删除)
                {
                    log.Error(UserHelper.GetCurrentUser.Name + "删除了订单：" + order.Id);
                    order.DeleteTime = DateTime.Now;
                }

                orderService.Save(order);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return RedirectToAction("Index");
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
            ViewData["ImageTypeList"] = ImageType.GetAll();
            if (orderForm.ImageTypes == null)
            {
                orderForm.ImageTypes = new ImageTypeModel();
                ViewBag.Message = "请至少选择一个印章类型！";
                return View(orderForm);
            }
            orderForm.ExpireTime = DateTime.Now.AddMinutes(20);
            orderForm.formGuid = Guid.NewGuid();
            orderForm.URL = string.Format(@"http://{0}\Front\Create?guid={1}", Request.Url.Authority, orderForm.formGuid);
            CacheHelper.SetCache(orderForm.formGuid.ToString(), orderForm.ImageTypes, 1200);
        
            log.Debug($"new order link: {orderForm.URL}");
            return View(orderForm);
        }

        [HttpGet]
        public FileStreamResult DownloadFile(string imageURL)
        {
            string fileName = imageURL;// imageURL.Substring(21, imageURL.Length-21);  //UploadFilesOutputImgs lendth 21 
            string filePath = Server.MapPath("//UploadFiles//OutputImgs//"+ fileName);

            return File(new FileStream(filePath, FileMode.Open), "application/octet-stream", Server.UrlEncode(fileName));
        }

        [HttpGet]
        public ActionResult Create()
        {
            //font list
            List<ImageFont> imageFonts = imageFontService.GetAll();
            List<SelectListItem> fontList = new List<SelectListItem>();
            foreach (var item in imageFonts)
            {
                fontList.Add(new SelectListItem { Text = item.name, Value = item.id.ToString() });
            }
            ViewBag.FontList = fontList;
            //如果sampleid不等于0.
            string sampleIdStr = Request["sampleId"];
            Sample sample = new Sample();
            if (!string.IsNullOrEmpty(sampleIdStr) && sampleIdStr != "0")
            {
                int sampleId = int.Parse(sampleIdStr);
                sample = sampleService.GetSample(sampleId, true);
            }
            return View(sample);
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection, string type)
        {
            Order order = new Order(); 
            order.SampleId = int.Parse(collection["SampleId"]);
            order.TaobaoId = order.SampleId + "_" + DateTime.Now.Ticks;
            order.ImageUrl = collection["ImageUrl"];
            order.MainText = collection["MainText"];
            order.SmallText = collection["SmallText"];
            string fontStr = collection["Font"];
            ImageFont font = null;
            order.Sample = sampleService.GetSample(order.SampleId, true);
            if (fontStr != "" && fontStr != "0") //改了sample的字体
            {
                int fontId = int.Parse(fontStr);
                font = imageFontService.GetById(fontId);
                order.Font = font.name;
                order.Sample.imageFont = font;
            }
            else//沿用sample的字体
            { 
                order.Font = order.Sample.Font;
                font = imageFontService.GetByName(order.Sample.Font);
                order.Sample.imageFont = font;
            }
            if (type == "提交订单")
            {
                try
                {
                    if (string.IsNullOrEmpty(order.ImageUrl))
                    {
                        throw new Exception("请先生成订单图片！");
                    }
                    order.SubmitTime = DateTime.Now;
                    order.Status = (int)EnumStatus.待审批;
                    order.SubmitTime = DateTime.Now;
                    order.ProductTime = DateTime.MinValue;
                    order.AuditTime = DateTime.MinValue;
                    order.DeleteTime = DateTime.MinValue;
                
                    orderService.Save(order);
                    return RedirectToAction("Create"); //继续停留在提交订单页面。
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    log.Error(ex);
                    return View("Create", order.Sample);
                }
            }
            else if (type == "CreateImage")
            {
                try
                {
                    var ifExist = orderService.CheckTaobaoIdExist(order.TaobaoId);
                    if (ifExist)
                    {
                        return Json(new { status = "Fail", message = "该淘宝订单号已经生成订单！" }, JsonRequestBehavior.AllowGet);
                    }
                    //order.Sample = sampleService.GetSample(order.SampleId, true);
                    //if (font != null) order.Sample.Font = font.name;
                    if (order.MainText.Length != order.Sample.MainTextNumber)
                    {
                        return Json(new { status = "Fail", message = "输入的文字数量不对！" }, JsonRequestBehavior.AllowGet);
                    }

                    for (int i = 0; i < order.Sample.MainTextNumber; i++)
                    {
                        order.Sample.MainText[i].Text = order.MainText[i].ToString();
                    }
                    if (order.Sample.IfHasSmallText)
                    {
                        if (string.IsNullOrEmpty(order.SmallText))
                        {
                            order.Sample.SmallText.Text = ""; //可以为空，可以没有副文字。
                        }
                        else
                        {
                            if (order.SmallText.Length > 11)
                            {
                                return Json(new { status = "Fail", message = "副文字不能超过11个字！" }, JsonRequestBehavior.AllowGet);
                            }
                            order.Sample.SmallText.Text = order.SmallText;

                        }
                    }

                    string imageUrl = ImageHelp.CreateImage(order.Sample, false, order.TaobaoId);
                    var result = Json(imageUrl);
                    return result;
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                    return Json(new { status = "Fail", message = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            return View(order);
        }

        public PartialViewResult _SampleList()
        {
            List<Sample> samples = new List<Sample>();
            string _type = Request["ImageType"];
            string _style = Request["Style"];
            //string _hasBgImage = Request["IfHasBgImage"];
            string _tag = Request["Tag"];
            string _keywords = Request["Keywords"];
            string _numberOfText = Request["NumberOfText"];
            try
            {
                EnumImageType? enumImageType = null;
                EnumImageStyle? enumImageStyle = null; 
                bool? booLIfHasBgImage = null;
                int numberOfText = 0;
                if (!string.IsNullOrEmpty(_type))
                {
                    var i = Convert.ToInt32(_type);
                    if (i > 1) enumImageType = (EnumImageType)i;
                } 
          
                if (!string.IsNullOrEmpty(_style))
                {
                    var i = Convert.ToInt32(_style);
                    if (i > -1)  enumImageStyle = (EnumImageStyle)i;
                }

                //if (!string.IsNullOrEmpty(_hasBgImage))
                //{
                //    var i = Convert.ToInt32(_hasBgImage);
                //    if(i>-1) booLIfHasBgImage = Convert.ToBoolean(i);
                //}
                if (string.IsNullOrEmpty(_keywords) && _tag != "0" && !string.IsNullOrEmpty(_tag))
                {
                    _keywords = _tag; //如果tag有值，keyword没值，就把tag当成keyword。主要用于儿童印章。
                }
                if (!string.IsNullOrEmpty(_numberOfText))
                {
                    numberOfText = Convert.ToInt32(_numberOfText); 
                }

                int pageSize = 60; //默认显示60个
                samples = sampleService.ListAll(enumImageType, enumImageStyle, booLIfHasBgImage, _keywords, true, "", pageSize, numberOfText); 
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return PartialView(samples);
        }
    }
}

 