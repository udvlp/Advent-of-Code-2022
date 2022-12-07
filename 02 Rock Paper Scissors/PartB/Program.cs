namespace AoC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var points = new Dictionary<string, int>()
            {
                { "A X", 3 + 0 },
                { "A Y", 1 + 3 },
                { "A Z", 2 + 6 },
                { "B X", 1 + 0 },
                { "B Y", 2 + 3 },
                { "B Z", 3 + 6 },
                { "C X", 2 + 0 },
                { "C Y", 3 + 3 },
                { "C Z", 1 + 6 }
            };
            var sr = new StreamReader(@"..\..\input.txt");
            int sum = 0;
            while (!sr.EndOfStream)
            {
                string? l = sr.ReadLine();
                sum += points[l];
            }
            Console.WriteLine(sum);
        }
    }
}