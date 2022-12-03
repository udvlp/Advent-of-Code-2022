using System;
using System.IO;

namespace AoC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sr = new StreamReader(@"..\..\..\input.txt");
            int sum = 0;
            int max = 0;
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (line == "")
                {
                    if (sum > max) max = sum;
                    sum = 0;
                }
                else
                {
                    sum += int.Parse(line);
                }
            }
            if (sum > max) max = sum;
            Console.WriteLine(max);
        }
    }
}