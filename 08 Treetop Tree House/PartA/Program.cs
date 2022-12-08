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

            int visible = 2 * width + 2 * height - 4;

            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    // left
                    bool v = true;
                    for (int d = x - 1; d >= 0; d--)
                    {
                        if (map[y, d] >= map[y, x])
                        {
                            v = false;
                            break;
                        }
                    }
                    if (v)
                    {
                        visible++;
                        continue;
                    }
                    // right
                    v = true;
                    for (int d = x + 1; d < width; d++)
                    {
                        if (map[y, d] >= map[y, x])
                        {
                            v = false;
                            break;
                        }
                    }
                    if (v)
                    {
                        visible++;
                        continue;
                    }
                    // top
                    v = true;
                    for (int d = y - 1; d >= 0; d--)
                    {
                        if (map[d, x] >= map[y, x])
                        {
                            v = false;
                            break;
                        }
                    }
                    if (v)
                    {
                        visible++;
                        continue;
                    }
                    // bottom
                    v = true;
                    for (int d = y + 1; d < height; d++)
                    {
                        if (map[d, x] >= map[y, x])
                        {
                            v = false;
                            break;
                        }
                    }
                    if (v)

                    {
                        visible++;
                        continue;
                    }
                }
            }

            Console.WriteLine(visible);
        }
    }
}