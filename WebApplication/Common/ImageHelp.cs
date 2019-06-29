using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;

namespace Models
{
    public class ImageHelp
    {
        public static string CreateImage(Sample sample, bool ifSample, string taobaoId)
        {
            Image imgBack = null;
            Graphics g = null;
            SolidBrush drawBrush = new SolidBrush(Color.Red);
            switch (sample.ImageType)
            {
                case EnumImageType.方形章:
                    if (!string.IsNullOrEmpty(sample.BgImage))
                    {
                        string bgImgPath = AppDomain.CurrentDomain.BaseDirectory + sample.BgImage;
                        imgBack = Image.FromFile(bgImgPath);     //相框图片 
                        g = Graphics.FromImage(imgBack);
                    }
                    else
                    {
                        int size = 600;//296;
                        imgBack = new System.Drawing.Bitmap(size, size);
                        g = Graphics.FromImage(imgBack);
                        if (sample.Style == EnumImageStyle.阳文)
                        {
                            float width = 56.0F;
                            Pen pen = new Pen(Color.Red, width);
                            g.DrawRectangle(pen, 0, 0, size, size);
                        }
                        else if (sample.Style == EnumImageStyle.阴文)
                        {
                            g.FillRectangle(drawBrush, 0, 0, size, size);
                        }
                    }
                    break;
                case EnumImageType.圆形章:
                    if (!string.IsNullOrEmpty(sample.BgImage))
                    {
                        string bgImgPath = AppDomain.CurrentDomain.BaseDirectory + sample.BgImage;
                        imgBack = Image.FromFile(bgImgPath);     //相框图片 
                        g = Graphics.FromImage(imgBack);
                    }
                    else
                    {
                        int size = 600;//296;
                        imgBack = new System.Drawing.Bitmap(size, size);
                        g = Graphics.FromImage(imgBack);
                        if (sample.Style == EnumImageStyle.阳文)
                        {
                            Pen pen = new Pen(Color.Red, 28.0F);
                            g.DrawEllipse(pen, 14, 14, size - 28, size - 28);
                        }
                        else if (sample.Style == EnumImageStyle.阴文)
                        {
                            g.FillEllipse(drawBrush, 0, 0, size, size);
                        }
                    }
                    break;
                case EnumImageType.扁章: //没有背景图,可以调整尺寸和边框
                    int sizeX = sample.ImageSizeX;
                    int sizeY = sample.ImageSizeY;
                    imgBack = new Bitmap(sizeX, sizeY);
                    g = Graphics.FromImage(imgBack);
                    if (sample.Style == EnumImageStyle.阳文)
                    {
                        float width = 28.0F;
                        if (sample.Boarder != 0)
                        {
                            width = sample.Boarder;
                        }
                        Pen pen = new Pen(Color.Red, width);
                        g.DrawRectangle(pen, 0, 0, sizeX, sizeY);
                    }
                    else if (sample.Style == EnumImageStyle.阴文)
                    {
                        g.FillRectangle(drawBrush, 0, 0, sizeX, sizeY);
                    }
                    break;
                case EnumImageType.儿童印章: //必须要有背景图
                    if (!string.IsNullOrEmpty(sample.BgImage))
                    {
                        string bgImgPath = AppDomain.CurrentDomain.BaseDirectory + sample.BgImage;
                        imgBack = Image.FromFile(bgImgPath);     //相框图片 
                        g = Graphics.FromImage(imgBack);
                    }
                    else
                    {
                        throw new Exception("请先上传背景图片！");
                    }
                    break;
                case EnumImageType.个性签名章:  //黑色文字，纯文字，没有边框，没有背景,不区分阴文阳文。
                    sizeX = 354;
                    sizeY = 118;

                    imgBack = new System.Drawing.Bitmap(sizeX, sizeY);
                    g = Graphics.FromImage(imgBack);
                    break;
                case EnumImageType.闲章:
                    if (!string.IsNullOrEmpty(sample.BgImage))
                    {
                        string bgImgPath = AppDomain.CurrentDomain.BaseDirectory + sample.BgImage;
                        imgBack = Image.FromFile(bgImgPath);     //相框图片 
                        g = Graphics.FromImage(imgBack);
                    }
                    else
                    {
                        sizeX = 295;
                        sizeY = 413;
                        imgBack = new System.Drawing.Bitmap(sizeX, sizeY);
                        g = Graphics.FromImage(imgBack);
                        if (sample.Style == EnumImageStyle.阳文)
                        {
                            float width = 56.0F;
                            Pen pen = new Pen(Color.Red, width);
                            g.DrawRectangle(pen, 0, 0, sizeX, sizeY);
                        }
                        else if (sample.Style == EnumImageStyle.阴文)
                        {
                            g.FillRectangle(drawBrush, 0, 0, sizeX, sizeY);
                        }
                    }
                    break;
                case EnumImageType.光敏章: //可以调整尺寸和边框
                    if (!string.IsNullOrEmpty(sample.BgImage))
                    {
                        string bgImgPath = AppDomain.CurrentDomain.BaseDirectory + sample.BgImage;
                        imgBack = Image.FromFile(bgImgPath);     //相框图片 
                        g = Graphics.FromImage(imgBack);
                    }
                    else
                    {
                        sizeX = sample.ImageSizeX;
                        sizeY = sample.ImageSizeY;
                        imgBack = new System.Drawing.Bitmap(sizeX, sizeY);
                        g = Graphics.FromImage(imgBack);
                        if (sample.Style == EnumImageStyle.阳文)
                        {
                            float width = 56.0F;
                            if (sample.Boarder != 0)
                            {
                                width = sample.Boarder;
                            }
                            Pen pen = new Pen(Color.Red, width);
                            g.DrawRectangle(pen, 0, 0, sizeX, sizeY);
                        }
                        else if (sample.Style == EnumImageStyle.阴文)
                        {
                            g.FillRectangle(drawBrush, 0, 0, sizeX, sizeY);
                        }
                    } 
                    break;
            }
            /////////////////////////////////////////写主文字//////////////////////////////////////////////////////
             
            if (sample.MainText.Count == sample.MainTextNumber)
            {
                for (int i = 0; i < sample.MainTextNumber; i++)
                {
                    DrawMainText(g, sample.MainText[i], sample.Style, sample.ImageType, sample.ImageFont);
                }
            }
            else
            {
                throw new Exception("方案文字和预设文字数量不一致！");
            }
            if (sample.SmallTextNumber > 0)
            {
                for (int i = 0; i < sample.SmallTextNumber; i++)
                {
                    DrawMainText(g, sample.SmallText[i], sample.Style, sample.ImageType, sample.ImageFont);
                }
            }
            #region 测试 瞄准线，后面要删掉。
            //if(ifSample)
            //{
            //    SolidBrush drawBrush1 = new SolidBrush(Color.Green);// Create point for upper-left corner of drawing.
            //    g.DrawLine(new Pen(drawBrush1), new Point(0, 0), new Point(295, 295));
            //    g.DrawLine(new Pen(drawBrush1), new Point(0, 295), new Point(295, 0));
            //    g.DrawLine(new Pen(drawBrush1), new Point(147, 0), new Point(147, 295));
            //    g.DrawLine(new Pen(drawBrush1), new Point(0, 147), new Point(295, 147));
            //}

            #endregion

            GC.Collect();
            string filename =  ifSample ? sample.Name + "_" + ".png" : taobaoId +".png";
            string path = ifSample ? $"\\UploadFiles\\SampleImgs\\{filename}" : $"\\UploadFiles\\OutputImgs\\{filename}";
            string saveImagePath = AppDomain.CurrentDomain.BaseDirectory + path;
            if (System.IO.File.Exists(saveImagePath))
            {
                System.IO.File.Delete(saveImagePath);
            }
            imgBack.Save(saveImagePath, ImageFormat.Png);//save new image to file system.
            return filename;
        }
        #region 
        //private static void DrawMainText(Graphics g, ImageText mainText, EnumImageStyle style, EnumImageType type)
        //{
        //    FontFamily font = null; 
        //    if (!mainText.imageFont.ifSystem)
        //    {
        //        //读取字体文件             
        //        string path = AppDomain.CurrentDomain.BaseDirectory + mainText.imageFont.url;
        //        PrivateFontCollection pfc = new PrivateFontCollection();
        //        pfc.AddFontFile(path);
        //        font = pfc.Families[0];
        //    }
        //    else
        //    {
        //        font = new FontFamily(mainText.imageFont.name);
        //    }

        //    String drawString = mainText.Text; // Create font and brush.

        //    Font drawFont = new Font(font, mainText.FontSize);    //实例化字体            
        //    SolidBrush drawBrush;
        //    float x = mainText.PositionX, y = mainText.PositionY;
        //    if (type == EnumImageType.儿童印章)
        //    {
        //        drawBrush = new SolidBrush(Color.Black); //儿童印章不区分阴文和阳文，统一用黑色文字。
        //    }
        //    else
        //    {
        //        if (style == EnumImageStyle.阳文)
        //        {
        //            drawBrush = new SolidBrush(Color.Red);
        //        }
        //        else
        //        {
        //            drawBrush = new SolidBrush(Color.White);
        //        }
        //    }
        //    ////减缓锯齿 
        //    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        //    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        //    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        //    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        //    g.DrawString(drawString, drawFont, drawBrush, x, y);
        //}

        //private static void DrawSmallText(Graphics g, ImageText smallText, EnumImageStyle style, EnumImageType type)
        //{
        //    FontFamily font = null;
        //    if (!smallText.imageFont.ifSystem)
        //    {
        //        //读取字体文件             
        //        string path = AppDomain.CurrentDomain.BaseDirectory + smallText.imageFont.url;
        //        PrivateFontCollection pfc = new PrivateFontCollection();
        //        pfc.AddFontFile(path);
        //        font = pfc.Families[0];
        //    }
        //    else
        //    {
        //        font = new FontFamily(smallText.imageFont.name);
        //    }
        //    string drawString = "";
        //    if (smallText.Order)
        //    {
        //        drawString = smallText.Text; // Create font and brush.
        //    }
        //    else //反向打印
        //    {
        //        drawString = WebApplication.Common.Utils.ReverseCharArray(smallText.Text);
        //    }
        //    Font drawFont = new Font(font, smallText.FontSize); //实例化字体             
        //    SolidBrush drawBrush;
        //    float x = smallText.PositionX; float y = smallText.PositionY;
        //    if (type == EnumImageType.儿童印章)
        //    {
        //        drawBrush = new SolidBrush(Color.Black);
        //    }
        //    else
        //    {
        //        if (style == EnumImageStyle.阳文)
        //        {
        //            drawBrush = new SolidBrush(Color.Red);
        //        }
        //        else
        //        {
        //            drawBrush = new SolidBrush(Color.White);
        //        }
        //    }
        //    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        //    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        //    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        //    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        //    g.DrawString(drawString, drawFont, drawBrush, x, y);
        //}
        #endregion

        //5-19需求修改，一个印章只有一个字体
        private static void DrawMainText(Graphics g, ImageText mainText, EnumImageStyle style, EnumImageType type, ImageFont imageFont)
        {
            FontFamily font = null;
            if (!imageFont.ifSystem)
            {
                //读取字体文件             
                string path = AppDomain.CurrentDomain.BaseDirectory + imageFont.url;
                PrivateFontCollection pfc = new PrivateFontCollection();
                pfc.AddFontFile(path);
                font = pfc.Families[0];
            }
            else
            {
                font = new FontFamily(imageFont.name);
            }

            String drawString = mainText.Text; // Create font and brush.

            Font drawFont = new Font(font, mainText.FontSize);    //实例化字体            
            SolidBrush drawBrush;
            float x = mainText.PositionX, y = mainText.PositionY;
            if (type == EnumImageType.儿童印章 || type == EnumImageType.个性签名章)
            {
                drawBrush = new SolidBrush(Color.Black); //儿童印章和个性签名章 不区分阴文和阳文，统一用黑色文字。
            }
            else
            {
                if (style == EnumImageStyle.阳文)
                {
                    drawBrush = new SolidBrush(Color.Red);
                }
                else
                {
                    drawBrush = new SolidBrush(Color.White);
                }
            }
            ////减缓锯齿 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.DrawString(drawString, drawFont, drawBrush, x, y);
        }

        private static void DrawSmallText(Graphics g, ImageText smallText, EnumImageStyle style, EnumImageType type, ImageFont imageFont)
        {
            FontFamily font = null;
            if (!imageFont.ifSystem)
            {
                //读取字体文件             
                string path = AppDomain.CurrentDomain.BaseDirectory + imageFont.url;
                PrivateFontCollection pfc = new PrivateFontCollection();
                pfc.AddFontFile(path);
                font = pfc.Families[0];
            }
            else
            {
                font = new FontFamily(imageFont.name);
            }
            string drawString = "";
            if (smallText.Order)
            {
                drawString = smallText.Text; // Create font and brush.
            }
            else //反向打印
            {
                drawString = WebApplication.Common.Utils.ReverseCharArray(smallText.Text);
            }
            Font drawFont = new Font(font, smallText.FontSize); //实例化字体             
            SolidBrush drawBrush;
            float x = smallText.PositionX; float y = smallText.PositionY;
            if (type == EnumImageType.儿童印章)
            {
                drawBrush = new SolidBrush(Color.Black);
            }
            else
            {
                if (style == EnumImageStyle.阳文)
                {
                    drawBrush = new SolidBrush(Color.Red);
                }
                else
                {
                    drawBrush = new SolidBrush(Color.White);
                }
            }
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.DrawString(drawString, drawFont, drawBrush, x, y);
        }
    }
}
