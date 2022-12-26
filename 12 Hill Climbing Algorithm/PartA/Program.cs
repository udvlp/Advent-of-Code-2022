using System.Diagnostics;

namespace AoC
{
    internal class Program
    {
        struct Field
        {
            public int height;
            public bool visited;
            public (int x, int y) prev;
        }

        static void Main(string[] args)
        {
            Field[,] map = null;
            (int x, int y) pointS = (0, 0);
            (int x, int y) pointE = (0, 0);

            var lines = File.ReadAllLines(@"..\..\input.txt");
            map = new Field[lines[0].Length, lines.Length];
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (lines[y][x] == 'S')
                    {
                        map[x, y].height = 1;
                        pointS = (x, y);
                    }
                    else if (lines[y][x] == 'E')
                    {
                        map[x, y].height = 26;
                        pointE = (x, y);
                    }
                    else
                    {
                        map[x, y].height = lines[y][x] - 'a' + 1;
                    }
                }
            }
            lines = null;
            int width = map.GetLength(0);
            int height = map.GetLength(1);

            Queue<(int x, int y)> queue = new();

            map[pointS.x, pointS.y].visited = true;
            queue.Enqueue(pointS);

            (int dx, int dy)[] directions = { (0, -1), (1, 0), (0, 1), (-1, 0) };
            int iteration = 0;
            while (queue.Count > 0)
            {
                iteration++;
                var p = queue.Dequeue();

                if (p == pointE)
                {
                    int steps = 0;
                    var n = p;
                    while (n != pointS)
                    {
                        n = map[n.x, n.y].prev;
                        steps++;
                    }
                    Console.WriteLine(steps);
                    break;
                }

                foreach (var dir in directions)
                {
                    (int x, int y) n = (p.x + dir.dx, p.y + dir.dy);
                    if (n.x >= 0 && n.x < width && n.y >= 0 && n.y < height)
                    {
                        if (!map[n.x, n.y].visited)
                        {
                            if (map[n.x, n.y].height <= map[p.x, p.y].height + 1)
                            {
                                map[n.x, n.y].visited = true;
                                map[n.x, n.y].prev = p;
                                queue.Enqueue(n);
                            }
                        }
                    }
                }

            }

        }
    }
}
