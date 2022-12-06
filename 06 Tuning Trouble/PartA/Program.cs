namespace AoC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sr = new StreamReader(@"..\..\..\input.txt");
            string line = sr.ReadLine();
            for (int i = 3; i < line.Length; i++)
            {
                if (line[i] != line[i - 1]
                    && line[i] != line[i - 2]
                    && line[i] != line[i - 3]
                    && line[i - 1] != line[i - 2]
                    && line[i - 1] != line[i - 3]
                    && line[i - 2] != line[i - 3])
                {
                    Console.WriteLine(i+1);
                    break;
                }
            }
        }
    }
}