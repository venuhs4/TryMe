using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
//using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace TryMe
{

    class Program
    {
        public int Prop1 { get; set; }
        public string Prop2 { get; set; }
        public string Prop3 { get; set; }
        public string Prop4 { get; set; }
        public string Prop5 { get; set; }
        public string Prop6 { get; set; }
        static string symbol = "";
        static void Main()
        {
            //MultiTaskTest();
            //Debug.WriteLine(DateTime.Now.AddDays(-60).Ticks);
            //ImageTOConsole();
            float initialAmount = 3700;

            Debug.WriteLine("This is the test for git hub");
            Debug.WriteLine("This is the test for git hub - master");

            Debug.WriteLine(JsonConvert.SerializeObject("", Formatting.Indented));
        }

        private static void ImageTOConsole()
        {
            Bitmap image = new Bitmap(@"C:\Users\Public\Pictures\Sample Pictures\Penguins.jpg");

            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Console.CursorLeft = i;
                    Console.CursorTop = j;
                    var c = image.GetPixel(i, j);
                    if ((c.R + c.G + c.B) > 255 * 3 / 2)
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write(" ");
                    }

                    Console.ForegroundColor = ConsoleColor.Red;
                }
            }
        }

        public static string MoveSymbol()
        {
            symbol = symbol.Substring(1) + symbol.Substring(0, 1);
            return symbol;
        }

        private static void MultiTaskTest()
        {
            ConcurrentDictionary<int, List<Program>> list = new ConcurrentDictionary<int, List<Program>>();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Parallel.For(0, 20000, (i) =>
            {
                Stopwatch s = new Stopwatch();
                s.Start();
                //Thread.Sleep(1000);
                List<Program> l = new List<Program>();
                for (int j = 0; j < 30; j++)
                {
                    l.Add(new Program()
                    {
                        Prop1 = j,
                        Prop2 = Guid.NewGuid().ToString(),
                        Prop3 = Guid.NewGuid().ToString(),
                        Prop4 = Guid.NewGuid().ToString(),
                        Prop5 = Guid.NewGuid().ToString(),
                        Prop6 = Guid.NewGuid().ToString()
                    });
                }

                list.AddOrUpdate(i, l, (key, value) => value);
                Debug.WriteLine("Done:{0,4} - {1}", i, s.Elapsed);
            });

            Debug.WriteLine(JsonConvert.SerializeObject(list, Formatting.Indented));

            Debug.WriteLine(sw.Elapsed);
            Debug.WriteLine(DateTime.Now);
        }


    }
    public class Nomen : IEqualityComparer<Nomen>
    {
        public string Id;
        public string NomenCode;
        public string ProducerName;
        public decimal? minPrice;

        bool IEqualityComparer<Nomen>.Equals(Nomen x, Nomen y)
        {
            if (x.Id == y.Id)
                return true;

            return (x.NomenCode == y.NomenCode && x.ProducerName == y.ProducerName);
        }

        int IEqualityComparer<Nomen>.GetHashCode(Nomen obj)
        {
            return 2;
        }
    }
}