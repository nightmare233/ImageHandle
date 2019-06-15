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
        private ImageFontService imageFontService;
        private log4net.ILog log = log4net.LogManager.GetLogger("ImageFontController");

        public FrontController()
        {
            orderService = new OrderService();
            sampleService = new SampleService();
            imageFontService = new ImageFontService();
        }

        // GET: Order/Create/  进入订单页面，先筛选，选择sample。
        [HttpGet]
        public ActionResult Create(string guid)
        {
            if (string.IsNullOrEmpty(guid) || CacheHelper.GetCache(guid) == null)
            {
                string errorMessage = "您的链接参数不正确，或者已经超时失效了。";
                return RedirectToAction("Result", "Front", new { message = errorMessage });
            }
            try
            {
                ImageTypeModel imageTypeModel = (ImageTypeModel)CacheHelper.GetCache(guid);

                List<ImageType> imageTypeList = new List<ImageType>();
                foreach (var item in imageTypeModel.ImageType)
                {
                    imageTypeList.Add(ImageType.GetAll().Find(t => t.Id == item));
                }
                ViewBag.imageTypes = imageTypeList;
                //font list
                List<ImageFont> imageFonts = imageFontService.GetAll();
                List<SelectListItem> fontList = new List<SelectListItem>();
                foreach (var item in imageFonts)
                {
                    fontList.Add(new SelectListItem { Text = item.name, Value = item.id.ToString() });
                }
                ViewBag.FontList = fontList;

                Order order = new Order();
                return View(order);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                string errorMessage = ex.Message;
                return RedirectToAction("Result", "Front", new { message = errorMessage });
            }
            
        }

        //进入Sample列表页面
        public ActionResult Samples(int type, int style, int ifHasBgImage, int fontId, string retURL)
        {
            //把用户选过的值传过来，选完之后再传回去。
            List<Sample> samples = null;
            bool? ifHasBgImageBool = null;
            if (ifHasBgImage == 1) ifHasBgImageBool = true;
            else if (ifHasBgImage == 2) ifHasBgImageBool = false;
            string font = "";
            if (fontId != 0)
            {
                var Font = imageFontService.GetById(fontId);
                font = Font.name;
            } 
            samples = sampleService.ListAll((EnumImageType)type, (EnumImageStyle)style, ifHasBgImageBool, null, false, font, 40, 0);
            ViewBag.type1 = type;
            ViewBag.style1 = style;
            ViewBag.ifHasBgImage1 = ifHasBgImage;
            ViewBag.retURL1 = retURL.Substring(retURL.IndexOf("=") + 1, 36);
            return View(samples);
        }

        //选完方案后，进入订单页面，输入文字，然后提交订单。
        public ActionResult Create2(int type, int style, int ifHasBgImage, string guid, int sampleId)
        {
            if (string.IsNullOrEmpty(guid) || CacheHelper.GetCache(guid) == null)
            {
                string errorMessage = "您的链接参数不正确，或者已经超时失效了。";
                return RedirectToAction("Result", "Front", new { message = errorMessage });
            } 
            Sample sample = sampleService.GetSample(sampleId, false);
            return View(sample);
        }
        // POST: Order/Create 输入文字之后，提交订单
        [HttpPost]
        public ActionResult Create2(FormCollection collection, string type)
        {
            Order order = new Order();
            order.SampleId = int.Parse(collection["SampleId"]);
            order.TaobaoId = collection["TaobaoId"];
            order.ImageUrl = collection["ImageUrl"];
            order.MainText = collection["MainText"];
            order.SmallText = collection["SmallText"];
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

                    return RedirectToAction("Result", "Front", new { message = "订单提交成功!" });
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    log.Error(ex);
                    return View("Create2", order);
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
                    order.Sample = sampleService.GetSample(order.SampleId, true);
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
                            return Json(new { status = "Fail", message = "请输入副文字！" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            if (order.SmallText.Length > 11)
                            {
                                return Json(new { status = "Fail", message = "副文字不能超过11个字！" }, JsonRequestBehavior.AllowGet);
                            }
                            
                            for (int i = 0; i < order.Sample.SmallTextNumber; i++)
                            {
                                order.Sample.SmallText[i].Text = order.SmallText[i].ToString();
                            }
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
         
        [HttpGet]
        public ActionResult Result(string message)
        {
            ViewBag.Message = message;
            return View();
        }
         
    }
}
