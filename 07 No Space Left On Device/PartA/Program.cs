namespace AoC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sr = new StreamReader(@"..\..\input.txt");
            int atmost100000 = 0;
            recurse();
            Console.WriteLine(atmost100000);

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
                if (result <= 100000) atmost100000 += result;
                return result;
            }
        }
    }
}