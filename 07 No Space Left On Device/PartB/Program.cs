namespace AoC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sr = new StreamReader(@"..\..\input.txt");
            List<int> dirsizes = new();
            const int total = 70000000;
            const int needed = 30000000;
            int used = recurse();
            int free = total - used;
            dirsizes.Sort();
            foreach (int n in dirsizes)
            {
                if (n > needed-free)
                {
                    Console.WriteLine(n);
                    break;
                }
            }

            int recurse()
            {
                int result = 0;
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] parts = line.Split(' ');
                    if (parts.Length > 0)
                    {
                        if (parts[0] == "$")
                        {
                            if (parts[1] == "cd")
                            {
                                if (parts[2] == "..")
                                {
                                    break;
                                }
                                else
                                {
                                    result += recurse();
                                }
                            }
                            else if (parts[1] == "ls")
                            {
                                // unused
                            }

                        }
                        else if (parts[0] == "dir")
                        {
                            // Dir
                            // unused
                        }
                        else
                        {
                            // File
                            result += int.Parse(parts[0]);
                        }
                    }
                }
                dirsizes.Add(result);
                return result;
            }
        }
    }
}