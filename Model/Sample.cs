using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model
{
    public class Sample
    {
        public EnumImageType ImageType { get; set; }  //印章类型, 圆形章,方章
        public int ImageSizeX { get; set; } //尺寸,长
        public int ImageSizeY { get; set; } //尺寸2， 宽，方和圆形章为null
        public EnumImageStyle Style { get; set; }  //印章样式， 阴文阳文
        public string ImageUrl { get; set; } //生成的样品图片地址。
        public int BgImage { get; set; } //印章背景图片，阴文和阳文没有图案， 为null
        public int MainTextNumber { get; set; }
        public MainText[] MainText { get; set; }
        public SmallText SmallText { get; set; }
    }

    public class MainText
    {
        public string Text { get; set; } //印章文字， 文字是客户输入的。
        public int Font { get; set; }  //印章字体， 宋体
        public int PositionX { get; set; } //第1个字的X坐标位置。
        public int PositionY { get; set; } //第1个字的Y坐标位置。
        public int FontSize { get; set; }
    }

    public class SmallText
    {
        public string Text { get; set; }
        public int Font { get; set; }  //印章字体， 宋体
        public int PositionX { get; set; } //第1个字的X坐标位置。
        public int PositionY { get; set; } //第1个字的Y坐标位置。
        public int FontSize { get; set; }
        public bool Order { get; set; } //文字的顺序，从左到右还是从右到左。
    }

}