namespace AoC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sr = new StreamReader(@"..\..\input.txt");
            int count = 0;
            while (!sr.EndOfStream)
            {
                string? line = sr.ReadLine();
                int[] a = line.Split('-', ',').Select(Int32.Parse).ToArray();
                if (a[0] >= a[2] && a[1] <= a[3] || a[2] >= a[0] && a[3] <= a[1])
                {
                    count++;
                }
            }
            Console.WriteLine(count);
        }
    }
}