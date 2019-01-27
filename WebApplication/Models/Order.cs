using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int TaobaoId { get; set; }
        public int ImageType { get; set; }  //印章类型, 圆形章
        public string ImageTypeName { get; set; }
        public int ImageSize { get; set; } //尺寸
        public int Font { get; set; }  //印章字体， 宋体
        public string FontName { get; set; }
        public int Style { get; set; }  //印章样式， 阴文阳文
        public string StyleName { get; set; }
        public string Text { get; set; } //印章文字
        public string ImageUrl { get; set; }
        public int BgImage { get; set; } //印章背景图片
        public string BgImageUrl { get; set; }
        public DateTime SubmitTime { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public DateTime AuditTime { get; set; }
        public int Auditor { get; set; }
        public string AuditorName { get; set; }
        public int Productor { get; set; }
        public string ProductorName { get; set; }
        public DateTime ProductTime { get; set; }
        public DateTime DeleteTime { get; set; }
    }

    public class ImageOrder
    {
        public int Id { get; set; }
        public int TaobaoId { get; set; }
        public Sample sample { get; set; }
    }
}