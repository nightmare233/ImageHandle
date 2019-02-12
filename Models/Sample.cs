using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class Sample
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EnumImageType ImageType { get; set; }  //印章类型, 圆形章,方章
        public int ImageSizeX { get; set; } //尺寸,长
        public int ImageSizeY { get; set; } //尺寸2， 宽，方和圆形章为null
        public EnumImageStyle Style { get; set; }  //印章样式， 阴文阳文
        public string ImageUrl { get; set; } //生成的样品图片地址。
        public string BgImage { get; set; } //印章背景图片，阴文和阳文没有图案， 为null
        public int MainTextNumber { get; set; }
        public bool IfHasSmallText { get; set; }
        public List<ImageText> MainText { get; set; }
        public ImageText SmallText { get; set; } 
    }

    public class ImageText
    {
        public int SampleId { get; set; }
        public int Type { get; set; }
        public string Text { get; set; } //印章文字， 文字是客户输入的。
        public string Font { get; set; }  //印章字体， 宋体
        public int PositionX { get; set; } //第1个字的X坐标位置。
        public int PositionY { get; set; } //第1个字的Y坐标位置。
        public int FontSize { get; set; }
        public bool Order { get; set; } //文字的顺序，从左到右还是从右到左。
    }

    //public class SmallText
    //{
    //    public int SampleId { get; set; }
    //    public string Text { get; set; }
    //    public string Font { get; set; }  //印章字体， 宋体
    //    public int PositionX { get; set; } //第1个字的X坐标位置。
    //    public int PositionY { get; set; } //第1个字的Y坐标位置。
    //    public int FontSize { get; set; }
    //    public bool Order { get; set; } //文字的顺序，从左到右还是从右到左。
    //}

}