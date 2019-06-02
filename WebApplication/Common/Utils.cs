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
    }
}