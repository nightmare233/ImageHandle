using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
	public static class ImageHandle
	{

        public static Image SquareImage(string sourceImg, string destImg)
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
            int textSize = 48;
            float half = 98.0F;
            //写字1
            String drawString = "国"; // Create font and brush.
            Font drawFont = new Font("Arial", textSize);
            //float x = half + blank; float y = 0.0F + boarder + blank;// Draw string to screen.
            float x = half; float y = boarder + blank;// Draw string to screen.

            //写字2
            String drawString2 = "圆"; // Create font and brush.
            Font drawFont2 = new Font("Arial", textSize);
            float x2 = half; float y2 = half + blank;// Draw string to screen.

            //写字3
            String drawString3 = "围"; // Create font and brush.
            Font drawFont3 = new Font("Arial", textSize);
            float x3 = boarder;  float y3 = 0.0F + boarder + blank;// Draw string to screen.

            //写字4
            String drawString4 = "园"; // Create font and brush.
            Font drawFont4 = new Font("Arial", textSize); 
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
            string filename = Guid.NewGuid().ToString();
            string saveImagePath = $"D:/pictures/output/{filename}.png";
            //save new image to file system.
            imgBack.Save(saveImagePath, ImageFormat.Png);
            return imgBack;
        }

        public static Image CircleImage()
        {
            return null;
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
	}
}
