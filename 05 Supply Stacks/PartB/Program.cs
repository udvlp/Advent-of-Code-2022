namespace AoC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var reader = new StreamReader(@"..\..\..\input.txt");

            List<string> lines = new();

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line == "") break;
                lines.Add(line);
            }
            int numstacks = (lines[lines.Count - 1].Length + 2) / 4;

            var stacks = new Stack<char>[numstacks];
            for (int i = 0; i < numstacks; i++)
            {
                stacks[i] = new Stack<char>();
            }

            for (int i = lines.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j < numstacks; j++)
                {
                    int x = j * 4 + 1;
                    if (x < lines[i].Length && lines[i][x] != ' ')
                    {
                        stacks[j].Push(lines[i][x]);
                    }
                }
            }

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                var parts = line.Split(' ');
                if (parts[0] == "move")
                {
                    int cnt = int.Parse(parts[1]);
                    int src = int.Parse(parts[3]) - 1;
                    int dst = int.Parse(parts[5]) - 1;
                    var tmp = new Stack<char>();
                    for (int i = 0; i < cnt; i++) tmp.Push(stacks[src].Pop());
                    for (int i = 0; i < cnt; i++) stacks[dst].Push(tmp.Pop());
                }
            }

            for (int j = 0; j < numstacks; j++)
            {
                Console.Write(stacks[j].Peek());
            }
            Console.WriteLine();
        }
    }
}