using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsoleApp
{
    public class Sample
    {
        public int ImageType { get; set; }  //印章类型, 圆形章,方章
        //public string ImageTypeName { get; set; }
        public int ImageSize { get; set; } //尺寸,长
        public int ImageSize2 { get; set; } //尺寸2， 宽，方和圆形章为null
        //public string FontName { get; set; }
        public int Style { get; set; }  //印章样式， 阴文阳文
        //public string StyleName { get; set; }
        public string ImageUrl { get; set; } //生成的样品图片地址。
        public int BgImage { get; set; } //印章背景图片，阴文和阳文没有图案， 为null
        //public string BgImageUrl { get; set; }
        public ImageText[] ImageTexts { get; set; }
    }

    public class ImageText
    {
        public string Text { get; set; } //印章文字， 文字是客户输入的。
        public int Font { get; set; }  //印章字体， 宋体
        public int PositionX { get; set; } //第1个字的X坐标位置。
        public int PositionY { get; set; } //第1个字的Y坐标位置。
        public bool Order { get; set; } //文字的顺序，从左到右还是从右到左。
    }

}