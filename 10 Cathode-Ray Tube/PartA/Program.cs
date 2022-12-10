namespace AoC
{
    internal class Program
    {
        static int x = 1;
        static int cyclenumber = 1;
        static int signalstrengthsum = 0;
        
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
            Console.WriteLine(signalstrengthsum);
        }

        static void cycle()
        {
            if ((cyclenumber + 20) % 40 == 0)
            {
                Console.WriteLine($"cycle: {cyclenumber} x: {x}");
                signalstrengthsum += cyclenumber * x;
            }
            cyclenumber++;
        }
    }
}