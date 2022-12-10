using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AoC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<char, (int dx, int dy)> directions = new()
            {
                { 'U', (0, -1) },
                { 'R', (1, 0) },
                { 'D', (0, 1) },
                { 'L', (-1, 0) }
            };

            HashSet<(int x, int y)> visited = new();

            var lines = File.ReadAllLines(@"..\..\input.txt");
            var steps = lines
               .Select(l => l.Split(' '))
               .Select(a => (direction: a[0][0], count: int.Parse(a[1])));

            (int x, int y)[] node = new (int x, int y)[10];

            visited.Add(node[node.Length - 1]);
            foreach (var step in steps)
            {
                for (int i = 0; i < step.count; i++)
                {
                    var (dx, dy) = directions[step.direction];
                    node[0] = (node[0].x + dx, node[0].y + dy);
                    for (int t = 1; t < node.Length; t++)
                    {
                        if (int.Abs(node[t-1].y - node[t].y) > 1 || int.Abs(node[t-1].x - node[t].x) > 1)
                        {
                            node[t].y += int.Sign(node[t-1].y - node[t].y);
                            node[t].x += int.Sign(node[t-1].x - node[t].x);
                        }
                    }
                    visited.Add(node[node.Length - 1]);
                }
            }

            Console.WriteLine(visited.Count);
        }
    }
}