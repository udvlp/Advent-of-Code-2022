namespace AoC
{
    internal class Program
    {
        static int x = 2;
        static int cyclenumber = 1;

        static void Main(string[] args)
        {
            var sr = new StreamReader(@"..\..\input.txt");
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                var parts = line.Split(' ');
                if (parts[0] == "noop")
                {
                    cycle();
                }
                else if (parts[0] == "addx")
                {
                    int p = int.Parse(parts[1]);
                    for (int i = 0; i < 2; i++)
                    {
                        cycle();
                    }
                    x += p;
                }
            }
        }

        static void cycle()
        {
            int h = (cyclenumber - 1) % 40 + 1;
            if (h >= x - 1 && h <= x + 1) Console.Write("#"); else Console.Write(".");
            if (h == 40) Console.WriteLine();
            cyclenumber++;
        }
    }
}