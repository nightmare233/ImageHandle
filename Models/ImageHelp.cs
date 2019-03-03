using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text; 

namespace Models
{
    public class ImageHelp
    {
        public static string CreateImage(Sample sample, bool ifSample)
        {
            Image imgBack = null;
            Graphics g = null;
            SolidBrush drawBrush = new SolidBrush(Color.Red);
            int size = 296;
            if (sample.ImageType == EnumImageType.方形章)
            {
                if (!string.IsNullOrEmpty(sample.BgImage))
                {
                    string bgImgPath = AppDomain.CurrentDomain.BaseDirectory + sample.BgImage;
                    imgBack = Image.FromFile(bgImgPath);     //相框图片 
                    g = Graphics.FromImage(imgBack);
                }
                else
                {
                    imgBack = new System.Drawing.Bitmap(size, size);
                    g = Graphics.FromImage(imgBack);
                    if (sample.Style == EnumImageStyle.阳文)
                    {
                        Pen pen = new Pen(Color.Red, 14.0F);
                        g.DrawRectangle(pen, 0, 0, size, size);
                    }
                    else if (sample.Style == EnumImageStyle.阴文)
                    {
                        //SolidBrush drawBrush = new SolidBrush(Color.Red);// Create point for upper-left corner of drawing.
                        g.FillRectangle(drawBrush, 0, 0, size, size);
                    }
                }
            }
            else if (sample.ImageType == EnumImageType.圆形章)
            {
                if (!string.IsNullOrEmpty(sample.BgImage))
                { 
                    string bgImgPath = AppDomain.CurrentDomain.BaseDirectory + sample.BgImage;
                    imgBack = Image.FromFile(bgImgPath);     //相框图片 
                    g = Graphics.FromImage(imgBack);
                }
                else
                {
                    imgBack = new System.Drawing.Bitmap(size, size);
                    g = Graphics.FromImage(imgBack);
                    if (sample.Style == EnumImageStyle.阳文)
                    {
                        Pen pen = new Pen(Color.Red, 14.0F);
                        g.DrawEllipse(pen, 7, 7, size - 14, size - 14);
                    }
                    else if (sample.Style == EnumImageStyle.阴文)
                    {
                        //SolidBrush drawBrush = new SolidBrush(Color.Red);// Create point for upper-left corner of drawing.
                        g.FillEllipse(drawBrush, 0, 0, size, size);
                    }
                }
            }
            else if (sample.ImageType == EnumImageType.扁章)
            {

            }
            else if (sample.ImageType == EnumImageType.儿童印章)
            {

            }
            else if (sample.ImageType == EnumImageType.个性签名章)
            {

            }
            //写主文字
            if (sample.MainText.Count == sample.MainTextNumber)
            {
                for (int i = 0; i < sample.MainTextNumber; i++)
                {
                    DrawMainText(g, sample.MainText[i], sample.Style);
                }
            }
            else
            {
                throw new Exception("方案文字和预设文字数量不一致！");
            }
            if (sample.IfHasSmallText)
            {
                DrawSmallText(g, sample.SmallText, sample.Style);
            }
            
            SolidBrush drawBrush1 = new SolidBrush(Color.Green);// Create point for upper-left corner of drawing.
            g.DrawLine(new Pen(drawBrush1), new Point(0, 0), new Point(295, 295));
            g.DrawLine(new Pen(drawBrush1), new Point(0, 295), new Point(295, 0));
            g.DrawLine(new Pen(drawBrush1), new Point(147, 0), new Point(147, 295));
            g.DrawLine(new Pen(drawBrush1), new Point(0, 147), new Point(295, 147));
            GC.Collect();
            string filename = sample.ImageType.ToString() + sample.Style.ToString() + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ms")+".png";
            string path = ifSample ? $"\\SampleImgs\\{filename}" : $"\\OutputImgs\\{filename}";
            string saveImagePath = AppDomain.CurrentDomain.BaseDirectory + path;  //todo 路径
            //save new image to file system.
            imgBack.Save(saveImagePath, ImageFormat.Png);
            return path;
        }

        private static void DrawMainText(Graphics g, ImageText mainText, EnumImageStyle style)
        {
            FontFamily font = null;
            if (mainText.Font.Contains("."))
            {
                string path = mainText.Font;
                //读取字体文件             
                PrivateFontCollection pfc = new PrivateFontCollection();
                pfc.AddFontFile(path);
                font = pfc.Families[0];
            }
            else
            {
                font = new FontFamily(mainText.Font);
            }
             
            //写字1
            String drawString = mainText.Text; // Create font and brush.
                                               //实例化字体             
            Font drawFont = new Font(font, mainText.FontSize);
            SolidBrush drawBrush;
            float x = mainText.PositionX; float y = mainText.PositionY;
            if (style == EnumImageStyle.阳文)
            {
                drawBrush = new SolidBrush(Color.Red);
            }
            else
            {
                drawBrush = new SolidBrush(Color.White);
            }
            g.DrawString(drawString, drawFont, drawBrush, x, y);
        }

        private static void DrawSmallText(Graphics g, ImageText smallText, EnumImageStyle style)
        {
            FontFamily font = null;
            if (smallText.Font.Contains("."))
            {
                string path = smallText.Font;
                //读取字体文件             
                PrivateFontCollection pfc = new PrivateFontCollection();
                pfc.AddFontFile(path);
                font = pfc.Families[0];
            }
            else
            {
                font = new FontFamily(smallText.Font);
            }
            //写字1
            String drawString = smallText.Text; // Create font and brush.
                                                //实例化字体             
            Font drawFont = new Font(font, smallText.FontSize);
            SolidBrush drawBrush;
            float x = smallText.PositionX; float y = smallText.PositionY;
            if (style == EnumImageStyle.阳文)
            {
                drawBrush = new SolidBrush(Color.Red);
            }
            else
            {
                drawBrush = new SolidBrush(Color.White);
            }
            g.DrawString(drawString, drawFont, drawBrush, x, y);
        }
    }
}
