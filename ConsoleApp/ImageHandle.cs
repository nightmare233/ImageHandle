using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace ConsoleApp
{
	public static class ImageHandle
	{

        public static Image SquareImage(string sourceImg, string destImg)
        { 
            Image imgBack = Image.FromFile(sourceImg);     //相框图片 
            Graphics g = Graphics.FromImage(imgBack);
            //Graphics g = Graphics.

            if (destImg != null)
            {
                Image img = System.Drawing.Image.FromFile(destImg);        //照片图片
                                                                           //从指定的System.Drawing.Image创建新的System.Drawing.Graphics       
                                                                           //Graphics g = Graphics.FromImage(imgBack);
                                                                           //g.DrawImage(imgBack, 0, 0, 148, 124);      // g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);
                g.FillRectangle(System.Drawing.Brushes.Black, -50, -50, (int)212, ((int)203));//相片四周刷一层黑色边框，这里没有，需要调尺寸
                                                                                              //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);
                g.DrawImage(img, -50, -50, 212, 203);
            }

            SolidBrush drawBrush = new SolidBrush(Color.Red);// Create point for upper-left corner of drawing.
            float boarder = 10F; //边框
            float blank = 10F;
            int textSize = 50;
            float half = 98.0F;
            //customize font family
            //路径             
            //string path = @"D:\pictures\经典繁角隶_0.TTF";
            string path = @"D:\pictures\AdobeHeitiStd-Regular.otf";
            //读取字体文件             
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(path);
            var font = pfc.Families[0];
            //写字1
            String drawString = "国"; // Create font and brush.
            //实例化字体             
            Font drawFont = new Font(font, textSize);
            
            //设置字体            
            //Font drawFont = new Font("Arial", textSize);
            
            float x = half; float y = boarder + blank;// Draw string to screen.

            //写字2
            String drawString2 = "圆"; // Create font and brush.
            Font drawFont2 = new Font(font, textSize);
            float x2 = half; float y2 = half + blank;// Draw string to screen.

            //写字3
            String drawString3 = "围"; // Create font and brush.
            Font drawFont3 = new Font(font, textSize);
            float x3 = boarder;  float y3 = 0.0F + boarder + blank;// Draw string to screen.

            //写字4
            String drawString4 = "园"; // Create font and brush.
            Font drawFont4 = new Font(font, textSize); 
            float x4 = boarder; float y4= half + blank;// Draw string to screen.

            g.DrawString(drawString, drawFont, drawBrush, x, y);

            g.DrawString(drawString2, drawFont2, drawBrush, x2, y2);

            g.DrawString(drawString3, drawFont3, drawBrush, x3, y3);

            g.DrawString(drawString4, drawFont4, drawBrush, x4, y4);

            SolidBrush drawBrush1 = new SolidBrush(Color.Green);// Create point for upper-left corner of drawing.
            g.DrawLine(new Pen(drawBrush1), new Point(0, 0), new Point(196, 196));
            g.DrawLine(new Pen(drawBrush1), new Point(0, 196), new Point(196, 0));
            g.DrawLine(new Pen(drawBrush1), new Point(98, 0), new Point(98, 196));
            g.DrawLine(new Pen(drawBrush1), new Point(0, 98), new Point(196, 98));
            GC.Collect();
            string filename = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ms");
            string saveImagePath = $"D:/pictures/output/{filename}.png";
            //save new image to file system.
            imgBack.Save(saveImagePath, ImageFormat.Png);
            return imgBack;
        }

        public static Image CircleImage(string sourceImg, string destImg)
        {
            Image imgBack = Image.FromFile(sourceImg);     //相框图片 
            Graphics g = Graphics.FromImage(imgBack);

            if (destImg != null)
            {
                Image img = System.Drawing.Image.FromFile(destImg);        //照片图片
                                                                           //从指定的System.Drawing.Image创建新的System.Drawing.Graphics       
                                                                           //Graphics g = Graphics.FromImage(imgBack);
                                                                           //g.DrawImage(imgBack, 0, 0, 148, 124);      // g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);
                g.FillRectangle(System.Drawing.Brushes.Black, -50, -50, (int)212, ((int)203));//相片四周刷一层黑色边框，这里没有，需要调尺寸
                                                                                              //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);
                g.DrawImage(img, -50, -50, 212, 203);
            }

            SolidBrush drawBrush = new SolidBrush(Color.Red);// Create point for upper-left corner of drawing.
            float boarder = 10F; //边框
            float blank = 10F;
            int textSize = 50;
            float half = 98.0F;
            //customize font family
            //路径             
            //string path = @"D:\pictures\经典繁角隶_0.TTF";
            string path = @"D:\pictures\AdobeHeitiStd-Regular.otf";
            //读取字体文件             
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(path);
            var font = pfc.Families[0];
            //写字1
            String drawString = "国"; // Create font and brush.
            //实例化字体             
            Font drawFont = new Font(font, textSize);

            //设置字体            
            //Font drawFont = new Font("Arial", textSize);

            float x = half; float y = boarder + blank;// Draw string to screen.

            //写字2
            String drawString2 = "圆"; // Create font and brush.
            Font drawFont2 = new Font(font, textSize);
            float x2 = half; float y2 = half + blank;// Draw string to screen.

            //写字3
            String drawString3 = "围"; // Create font and brush.
            Font drawFont3 = new Font(font, textSize);
            float x3 = boarder; float y3 = 0.0F + boarder + blank;// Draw string to screen.

            //写字4
            String drawString4 = "园"; // Create font and brush.
            Font drawFont4 = new Font(font, textSize);
            float x4 = boarder; float y4 = half + blank;// Draw string to screen.

            g.DrawString(drawString, drawFont, drawBrush, x, y);

            g.DrawString(drawString2, drawFont2, drawBrush, x2, y2);

            g.DrawString(drawString3, drawFont3, drawBrush, x3, y3);

            g.DrawString(drawString4, drawFont4, drawBrush, x4, y4);

            SolidBrush drawBrush1 = new SolidBrush(Color.Green);// Create point for upper-left corner of drawing.
            g.DrawLine(new Pen(drawBrush1), new Point(0, 0), new Point(196, 196));
            g.DrawLine(new Pen(drawBrush1), new Point(0, 196), new Point(196, 0));
            g.DrawLine(new Pen(drawBrush1), new Point(98, 0), new Point(98, 196));
            g.DrawLine(new Pen(drawBrush1), new Point(0, 98), new Point(196, 98));
            GC.Collect();
            string filename = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ms");
            string saveImagePath = $"D:/pictures/output/{filename}.png";
            //save new image to file system.
            imgBack.Save(saveImagePath, ImageFormat.Png);
            return imgBack;
        }

        /// <summary>
        /// 图片裁剪，生成新图，保存在同一目录下,名字加_new，格式1.png  新图1_new.png
        /// </summary>
        /// <param name="picPath">要修改图片完整路径</param>
        /// <param name="x">修改起点x坐标</param>
        /// <param name="y">修改起点y坐标</param>
        /// <param name="width">新图宽度</param>
        /// <param name="height">新图高度</param>
        public static void caijianpic(String picPath, int x, int y, int width, int height)
		{
			//图片路径
			String oldPath = picPath;
			//新图片路径
			String newPath = System.IO.Path.GetExtension(oldPath);
			//计算新的文件名，在旧文件名后加_new
			newPath = oldPath.Substring(0, oldPath.Length - newPath.Length) + "_new" + newPath;
			//定义截取矩形
			System.Drawing.Rectangle cropArea = new System.Drawing.Rectangle(x, y, width, height);
			//要截取的区域大小
			//加载图片
			System.Drawing.Image img = System.Drawing.Image.FromStream(new System.IO.MemoryStream(System.IO.File.ReadAllBytes(oldPath)));
			//判断超出的位置否
			if ((img.Width < x + width) || img.Height < y + height)
			{
				Console.WriteLine("裁剪尺寸超出原有尺寸！");
				img.Dispose();
				return;
			}
			//定义Bitmap对象
			System.Drawing.Bitmap bmpImage = new System.Drawing.Bitmap(img);
			//进行裁剪
			System.Drawing.Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
			//保存成新文件
			bmpCrop.Save(newPath);
			//释放对象
			img.Dispose(); bmpCrop.Dispose();
		}
		/// <summary>
		/// 调用此函数后使此两种图片合并，类似相册，有个
		/// 背景图，中间贴自己的目标图片
		/// </summary>
		/// <param name="sourceImg">粘贴的源图片</param>
		/// <param name="destImg">粘贴的目标图片</param>
		public static Image CombinImage(string sourceImg, string destImg)
		{
			Image imgBack = Image.FromFile(sourceImg);     //相框图片 
			Image img = System.Drawing.Image.FromFile(destImg);        //照片图片
																	   //从指定的System.Drawing.Image创建新的System.Drawing.Graphics       
			Graphics g = Graphics.FromImage(imgBack);
			//g.DrawImage(imgBack, 0, 0, 148, 124);      // g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);
			g.FillRectangle(System.Drawing.Brushes.Black, -50, -50, (int)212, ((int)203));//相片四周刷一层黑色边框，这里没有，需要调尺寸
																						  //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);
			g.DrawImage(img, -50, -50, 212, 203);
			String drawString = "章子怡"; // Create font and brush.

			Font drawFont = new Font("Arial", 28);

			SolidBrush drawBrush = new SolidBrush(Color.Black);// Create point for upper-left corner of drawing.

			float x = 40.0F; float y = 60.0F;// Draw string to screen.

			g.DrawString(drawString, drawFont, drawBrush, x, y);



			GC.Collect();
            string filename = Guid.NewGuid().ToString();
			string saveImagePath = $"D:/pictures/{filename}.png";
			//save new image to file system.
			imgBack.Save(saveImagePath, ImageFormat.Png);
			return imgBack;
		}

        public static Sample CreateFangXing()
        {
            int blank = 7;
            int textSize = 80;
            int half = 148;
            int boarder = 0;

            List<ImageText> ImageTexts = new List<ImageText>();
            ImageText ImageText1 = new ImageText {
                Font = @"D:\pictures\AdobeHeitiStd-Regular.otf",
                PositionX = half,
                PositionY = boarder + blank,
                FontSize = textSize,
                Text = "国"
            };
            ImageText ImageText2 = new ImageText
            {
                Font = @"D:\pictures\AdobeHeitiStd-Regular.otf",
                PositionX = half ,
                PositionY = half + blank,
                FontSize = textSize,
                Text = "圆"
            };
            ImageText ImageText3 = new ImageText
            {
                Font = @"D:\pictures\AdobeHeitiStd-Regular.otf",
                PositionX = boarder,
                PositionY = boarder + blank,
                FontSize = textSize,
                Text = "园"
            };
            ImageText ImageText4 = new ImageText
            {
                Font = @"D:\pictures\AdobeHeitiStd-Regular.otf",
                PositionX = boarder,
                PositionY = half + blank,
                FontSize = textSize,
                Text = "围"
            };
            ImageTexts.Add(ImageText1);
            ImageTexts.Add(ImageText2);
            ImageTexts.Add(ImageText3);
            ImageTexts.Add(ImageText4);

            ImageText smallText = new ImageText
            {
                Text = "",
                Font = "",
                FontSize = 28,
                PositionX = 100,
                PositionY = 100
            };

            Sample sample = new Sample()
            {
                ImageType = EnumImageType.方形章,
                Style = EnumImageStyle.阳文,
                ImageSizeX = 30,
                ImageSizeY = 0,
                MainTextNumber = 4,
                BgImage = @"D:\pictures\fang-yang.png",
                MainText = ImageTexts,
                SmallText = smallText,
                IfHasSmallText = false
            };
             
            return sample;
        }
        public static Sample CreateYuanXing()
        {
            int blank = 25;
            int textSize = 60;
            int half = 148;
            int boarder = 0;

            List<ImageText> ImageTexts = new List<ImageText>();
            ImageText ImageText1 = new ImageText
            {
                Font = @"D:\pictures\AdobeHeitiStd-Regular.otf",
                PositionX = half,
                PositionY = boarder + blank,
                FontSize = textSize,
                Text = "国"
            };
            ImageText ImageText2 = new ImageText
            {
                Font = @"D:\pictures\AdobeHeitiStd-Regular.otf",
                PositionX = half,
                PositionY = half + blank,
                FontSize = textSize,
                Text = "圆"
            };
            ImageText ImageText3 = new ImageText
            {
                Font = @"D:\pictures\AdobeHeitiStd-Regular.otf",
                PositionX = boarder,
                PositionY = boarder + blank,
                FontSize = textSize,
                Text = "园"
            };
            ImageText ImageText4 = new ImageText
            {
                Font = @"D:\pictures\AdobeHeitiStd-Regular.otf",
                PositionX = boarder,
                PositionY = half + blank,
                FontSize = textSize,
                Text = "围"
            };
            ImageTexts.Add(ImageText1);
            ImageTexts.Add(ImageText2);
            ImageTexts.Add(ImageText3);
            ImageTexts.Add(ImageText4);

            ImageText smallText = new ImageText
            {
                Text = "",
                Font = "",
                FontSize = 28,
                PositionX = 100,
                PositionY = 100
            };

            Sample sample = new Sample()
            {
                ImageType = EnumImageType.圆形章,
                Style = EnumImageStyle.阴文,
                ImageSizeX = 30,
                ImageSizeY = 0,
                MainTextNumber = 4,
                BgImage = "", //@"D:\pictures\fang-yang.png",
                MainText = ImageTexts,
                SmallText = smallText,
                IfHasSmallText = false
            };

            return sample;
        }

        public static void CreateImage(Sample sample)
        {
            Image imgBack = null;
            Graphics g = null;
            SolidBrush drawBrush = new SolidBrush(Color.Red);
            int size = 296;
            if (sample.ImageType == EnumImageType.方形章)
            {
                if (!string.IsNullOrEmpty(sample.BgImage))
                {
                    imgBack = Image.FromFile(sample.BgImage);     //相框图片 
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
                    imgBack = Image.FromFile(sample.BgImage);     //相框图片 
                    g = Graphics.FromImage(imgBack);
                }
                else
                {
                    imgBack = new System.Drawing.Bitmap(size, size);
                    g = Graphics.FromImage(imgBack);
                    if (sample.Style == EnumImageStyle.阳文)
                    {
                        Pen pen = new Pen(Color.Red, 14.0F);
                        g.DrawEllipse(pen, 7, 7, size-14, size-14);
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
            for (int i = 0; i < sample.MainTextNumber; i++)
            {
                DrawImageText(g, sample.MainText[i], sample.Style);
            }
            SolidBrush drawBrush1 = new SolidBrush(Color.Green);// Create point for upper-left corner of drawing.
            g.DrawLine(new Pen(drawBrush1), new Point(0, 0), new Point(295, 295));
            g.DrawLine(new Pen(drawBrush1), new Point(0, 295), new Point(295, 0));
            g.DrawLine(new Pen(drawBrush1), new Point(147, 0), new Point(147, 295));
            g.DrawLine(new Pen(drawBrush1), new Point(0, 147), new Point(295, 147));
            GC.Collect();
            string filename = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ms");
            string saveImagePath = $"D:/pictures/output/{filename}.png";
            //save new image to file system.
            imgBack.Save(saveImagePath, ImageFormat.Png);
        }

        private static void DrawImageText(Graphics g, ImageText ImageText, EnumImageStyle style)
        {
            string path = ImageText.Font;
            //读取字体文件             
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(path);
            FontFamily font = pfc.Families[0];
            //写字1
            String drawString = ImageText.Text; // Create font and brush.
                                     //实例化字体             
            Font drawFont = new Font(font, ImageText.FontSize);
            SolidBrush drawBrush;
            float x = ImageText.PositionX; float y = ImageText.PositionY;
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
            string path = smallText.Font;
            //读取字体文件             
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(path);
            FontFamily font = pfc.Families[0];
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
