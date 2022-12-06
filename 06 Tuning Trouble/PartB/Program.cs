namespace AoC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int len = 14;
            var sr = new StreamReader(@"..\..\..\input.txt");
            string line = sr.ReadLine();
            for (int i = len - 1; i < line.Length; i++)
            {
                bool diff = true;
                for (int j = i; j > i - len; j--)
                {
                    for (int k = j - 1; k > i - len; k--)
                    {
                        if (line[j] == line[k])
                        {
                            diff = false;
                            break;
                        }
                    }
                    if (!diff) break;
                }
                if (diff)
                {
                    Console.WriteLine(i + 1);
                    break;
                }
            }
        }
    }
}