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
            List<int> cal = new List<int>();
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (line == "")
                {
                    cal.Add(sum);
                    sum = 0;
                }
                else
                {
                    sum += int.Parse(line);
                }
            }
            if (sum > 0) cal.Add(sum);
            cal.Sort((a, b) => b.CompareTo(a));
            Console.WriteLine(cal[0] + cal[1] + cal[2]);
        }
    }
}