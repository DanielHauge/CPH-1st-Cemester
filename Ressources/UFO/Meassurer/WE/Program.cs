using System;
using System.Diagnostics;
using System.Net;

namespace Measure
{
    class Program
    {
        static void Main(string[] args)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://139.59.132.185:8080");

            Stopwatch timer = new Stopwatch();

            timer.Start();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            timer.Stop();

            TimeSpan timeTaken = timer.Elapsed;
            Console.WriteLine("Seconds: "+timeTaken.Seconds+" Miliseconds: "+timeTaken.Milliseconds);
            Console.WriteLine("Full time taken: "+timeTaken);
            Console.ReadKey();
        }
    }
}
