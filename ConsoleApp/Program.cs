﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			string sourceImg = @"D:\pictures\source1.png";
			string destImg = @"D:\pictures\dest1.png";

			ImageHandle.CombinImage(sourceImg,destImg);

		}
	}
}
