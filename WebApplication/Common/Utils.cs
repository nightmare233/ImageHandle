using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

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
         
    }
}