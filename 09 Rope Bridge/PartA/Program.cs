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

            (int x, int y) head = (0, 0);
            (int x, int y) tail = (0, 0);

            visited.Add(tail);
            foreach (var step in steps)
            {
                for (int i = 0; i < step.count; i++)
                {
                    var (dx, dy) = directions[step.direction];
                    head = (head.x + dx, head.y + dy);
                    if (int.Abs(head.y - tail.y) > 1 || int.Abs(head.x - tail.x) > 1)
                    { 
                        tail.y += int.Sign(head.y - tail.y);
                        tail.x += int.Sign(head.x - tail.x);
                    }
                    visited.Add(tail);
                }
            }

            Console.WriteLine(visited.Count);
        }
    }
}