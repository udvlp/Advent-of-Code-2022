namespace AoC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sr = new StreamReader(@"..\..\input.txt");
            int sum = 0;
            while (!sr.EndOfStream)
            {
                var set = new HashSet<char>();
                string? l = sr.ReadLine();
                for (int i = 0; i < l.Length / 2; i++)
                {
                    set.Add(l[i]);
                }
                for (int i = l.Length / 2; i < l.Length; i++)
                {
                    if (set.Contains(l[i]))
                    {
                        if (l[i] >= 'a' && l[i] <= 'z')
                        {
                            sum += l[i] - 'a' + 1;
                        }
                        else if (l[i] >= 'A' && l[i] <= 'Z')
                        {
                            sum += l[i] - 'A' + 27;
                        }
                        break;
                    }
                }
            }
            Console.WriteLine(sum);
        }
    }
}