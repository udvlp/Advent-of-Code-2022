namespace AoC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(@"..\..\input.txt");
            int width = lines[0].Length;
            int height = lines.Length;
            var map = new int[height, width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    map[y, x] = lines[y][x] - '0';
                }
            }

            int maxscore = 0;

            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    int scenicscore = 1;

                    // left
                    int dist = 0;
                    for (int d = x - 1; d >= 0; d--)
                    {
                        dist++;
                        if (map[y, d] >= map[y, x])
                        {
                            break;
                        }
                    }
                    scenicscore *= dist;

                    // right
                    dist = 0;
                    for (int d = x + 1; d < width; d++)
                    {
                        dist++;
                        if (map[y, d] >= map[y, x])
                        {
                            break;
                        }
                    }
                    scenicscore *= dist;

                    // top
                    dist = 0;
                    for (int d = y - 1; d >= 0; d--)
                    {
                        dist++;
                        if (map[d, x] >= map[y, x])
                        {
                            break;
                        }
                    }
                    scenicscore *= dist;

                    // bottom
                    dist = 0;
                    for (int d = y + 1; d < height; d++)
                    {
                        dist++;
                        if (map[d, x] >= map[y, x])
                        {
                            break;
                        }
                    }
                    scenicscore *= dist;

                    if (scenicscore > maxscore) maxscore = scenicscore;
                }
            }

            Console.WriteLine(maxscore);
        }
    }
}