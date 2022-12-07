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
                HashSet<char>? set = null;
                for (int g = 0; g < 3; g++)
                {
                    string? l = sr.ReadLine();
                    HashSet<char> lineset = new();
                    for (int i = 0; i < l.Length; i++)
                    {
                        lineset.Add(l[i]);
                    }
                    if (set == null)
                    {
                        set = lineset;
                    }
                    else
                    {
                        set.IntersectWith(lineset);
                    }
                }
                foreach (char c in set)
                {
                    if (c >= 'a' && c <= 'z')
                    {
                        sum += c - 'a' + 1;
                    }
                    else if (c >= 'A' && c <= 'Z')
                    {
                        sum += c - 'A' + 27;
                    }
                    break;
                }
            }
            Console.WriteLine(sum);
        }
    }
}