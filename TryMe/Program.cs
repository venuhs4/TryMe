using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace TryMe
{
    internal class Program
    {
        private static string _privateKey { get; set; }
        private static string _publicKey { get; set; }
        private static void Main()
        {
            Console.ReadLine();
            //added by Venu after read line commit
            RSA();
            //added by Venu-2
            RSA();
            Console.ReadKey();
            Console.WriteLine("asdfsaddf");
            RSA();
        }
        private static void RSA()
        {
            var rsa = new RSACryptoServiceProvider();
            _privateKey = rsa.ToXmlString(true);
            _publicKey = rsa.ToXmlString(false);

            var text = "Test1";
            Console.WriteLine("RSA // Text to encrypt: " + text);
            var enc = Encrypt(text);
            Console.WriteLine("RSA // Encrypted Text: " + enc);
            var dec = Decrypt(enc);
            Console.WriteLine("RSA // Decrypted Text: " + dec);
        }

        public static string Decrypt(string data)
        {
            var rsa = new RSACryptoServiceProvider();
            var dataArray = data.Split(new char[] { ',' });
            byte[] dataByte = new byte[dataArray.Length];
            for (int i = 0; i < dataArray.Length; i++)
            {
                dataByte[i] = Convert.ToByte(dataArray[i]);
            }

            rsa.FromXmlString(_privateKey);
            var decryptedByte = rsa.Decrypt(dataByte, false);
            return Encoding.ASCII.GetString(decryptedByte);
        }

        public static string Encrypt(string data)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(_publicKey);
            var dataToEncrypt = Encoding.ASCII.GetBytes(data);
            var encryptedByteArray = rsa.Encrypt(dataToEncrypt, false).ToArray();
            var length = encryptedByteArray.Count();
            var item = 0;
            var sb = new StringBuilder();
            foreach (var x in encryptedByteArray)
            {
                item++;
                sb.Append(x);

                if (item < length)
                    sb.Append(",");
            }

            return sb.ToString();
        }
    }
}