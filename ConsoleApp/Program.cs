using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
            //string sourceImg = @"D:\pictures\SquareBg1.png";
            //string destImg = @"D:\pictures\dest1.png";

            //ImageHandle.CombinImage(sourceImg,destImg);
            //ImageHandle.SquareImage(sourceImg, null);
            Sample sample = ImageHandle.CreateYuanXing();
            ImageHelp.CreateImage(sample, true);


        }
	}
}
