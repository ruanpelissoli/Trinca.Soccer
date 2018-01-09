using System;
using System.Security.Cryptography;
using System.Text;

namespace Trinca.Soccer.Services.Util
{
    public class Cryptography
    {
        public static string GetMd5Hash(string plainText)
        {
            var cryptoByte = new MD5CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(plainText));

            return Convert.ToBase64String(cryptoByte, 0, cryptoByte.Length);
        }
    }
}
