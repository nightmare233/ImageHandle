using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using Models;

namespace WebApplication.Common
{
    public class Utils
    {
        public static string Encrypt(string password)
        {
            password = password.ToLower();
            Byte[] clearBytes = new UnicodeEncoding().GetBytes(password);
            Byte[] hashedBytes = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(clearBytes);

            return BitConverter.ToString(hashedBytes);
        }

        public static string ReverseCharArray(string s)
        {
            char[] array = s.ToCharArray();
            String reverse = "";
            for (int i = array.Length - 1; i >= 0; i--)
            {
                reverse += array[i];
            }
            return reverse;
        }

        public static Sample SetSize(int sizeInt, Sample sample)
        {
            switch (sizeInt) {
                case 1:
                    sample.ImageSizeX = 165;
                    sample.ImageSizeY = 71;
                    break;
                case 2:
                    sample.ImageSizeX = 142;
                    sample.ImageSizeY = 72;
                    break;
                case 3:
                    sample.ImageSizeX = 177;
                    sample.ImageSizeY = 94;
                    break;
                case 4:
                    sample.ImageSizeX = 154;
                    sample.ImageSizeY = 59;
                    break;
                case 5:
                    sample.ImageSizeX = 165;
                    sample.ImageSizeY = 94;
                    break;
            }
            return sample;
        }

//光敏印章  10*35mm 像素尺寸为：413*118 
//光敏印章  10*50mm 像素尺寸为：591*118 
//光敏印章  16*32mm 像素尺寸为：378*189
//光敏印章  20*30mm 像素尺寸为：354*236
//光敏印章  20*40mm 像素尺寸为：472*236
//光敏印章  20*50mm 像素尺寸为：591*236
//光敏印章  15*70mm 像素尺寸为：827*177
//光敏印章  25*65mm 像素尺寸为：768*295
//光敏印章  25*50mm 像素尺寸为：591*295
//光敏印章  30*50mm 像素尺寸为：591*354
//光敏印章  30*60mm 像素尺寸为：709*354
//光敏印章  20*70mm 像素尺寸为：827*236
//光敏印章  35*75mm 像素尺寸为：886*413
//光敏印章  50*80mm 像素尺寸为：945*591
//光敏印章  35*105mm 像素尺寸为：1240*413
//光敏印章  60*90mm 像素尺寸为：1063*709
//光敏印章  20*20mm 像素尺寸为：236*236
//光敏印章  25*25mm 像素尺寸为：295*295
//光敏印章  45*65mm 像素尺寸为：768*531
//光敏印章  80*105mm 像素尺寸为：1240*945

    }
}