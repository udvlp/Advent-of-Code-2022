using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AoC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        struct Field
        {
            public int elevation;
            public bool visited;
            public (int x, int y) prev;
        }

        DrawingVisual dv;
        RenderTargetBitmap rendertargetbitmap;
        Field[,] map = null;
        int mapwidth = 0;
        int mapheight = 0;
        int mappixelsize = 0;
        (int x, int y) pointS;
        (int x, int y) pointE;
        bool running = true;
        Stopwatch stopwatch = new();
        int runpart = 0;

        public MainWindow()
        {
            InitializeComponent();

            var lines = File.ReadAllLines(@"..\..\example.txt");
            map = new Field[lines[0].Length, lines.Length];
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (lines[y][x] == 'S')
                    {
                        map[x, y].elevation = 1;
                        pointS = (x, y);
                    }
                    else if (lines[y][x] == 'E')
                    {
                        map[x, y].elevation = 26;
                        pointE = (x, y);
                    }
                    else
                    {
                        map[x, y].elevation = lines[y][x] - 'a' + 1;
                    }
                }
            }
            lines = null;
            mapwidth = map.GetLength(0);
            mapheight = map.GetLength(1);


            dv = new DrawingVisual();
            stopwatch.Start();
            CompositionTarget.Rendering += CompositionTarget_Rendering;

            new Thread(Run).Start();
        }

        private void Run()
        {

            while (runpart == 0)
            {
                if (!running) return;
                Thread.Sleep(50);
            }

            Queue<(int x, int y)> queue = new();

            if (runpart == 1)
            {
                map[pointS.x, pointS.y].visited = true;
                queue.Enqueue(pointS);
            }
            else if (runpart == 2)
            {
                map[pointE.x, pointE.y].visited = true;
                queue.Enqueue(pointE);
            }

            (int dx, int dy)[] directions = { (0, -1), (1, 0), (0, 1), (-1, 0) };
            int iteration = 0;
            while (running)
            {
                iteration++;
                if (queue.Count == 0) break;
                if (iteration % 20 == 0) Thread.Sleep(20);
                var p = queue.Dequeue();

                if ((runpart == 1 && p == pointE) || (runpart == 2 && map[p.x, p.y].elevation == 1))
                {
                    int steps = 0;
                    var n = p;
                    while ((runpart == 1 && n != pointS) || (runpart == 2 && n != pointE))
                    {
                        map[n.x, n.y].elevation = 27;
                        n = map[n.x, n.y].prev;
                        steps++;
                    }
                    Debug.WriteLine(steps);
                    break;
                }

                foreach (var dir in directions)
                {
                    (int x, int y) n = (p.x + dir.dx, p.y + dir.dy);
                    if (n.x >= 0 && n.x < mapwidth && n.y >= 0 && n.y < mapheight)
                    {
                        if (!map[n.x, n.y].visited)
                        {
                            if ((runpart == 1 && map[n.x, n.y].elevation <= map[p.x, p.y].elevation + 1) ||
                                (runpart == 2 && map[n.x, n.y].elevation >= map[p.x, p.y].elevation - 1))
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

        private void CompositionTarget_Rendering(object? sender, EventArgs e)
        {
            if (stopwatch.ElapsedMilliseconds >= 1000 / 25)
            {
                stopwatch.Restart();
                Render();
            }
        }

        private void Render()
        {
            if (map == null || double.IsNaN(image.Width)) return;

            using DrawingContext dc = dv.RenderOpen();
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    Brush brush;
                    if ((x, y) == pointS)
                    {
                        brush = new SolidColorBrush(Colors.White);
                    }
                    else if ((x, y) == pointE)
                    {
                        brush = new SolidColorBrush(Colors.Black);
                    }
                    else if (map[x, y].elevation == 0)
                    {
                        brush = new SolidColorBrush(Colors.Transparent);
                    }
                    else
                    {
                        (int saturation, int lightness) = map[x, y].visited
                            ? (90, 50)
                            : (40, map[x, y].elevation * 2 + 10);
                        brush = new SolidColorBrush(HSLToRGB(240 - map[x, y].elevation * 10, saturation, lightness));
                    }
                    dc.DrawRectangle(brush, null, new Rect(x * mappixelsize, y * mappixelsize, mappixelsize, mappixelsize));
                }
            }
            dc.Close();

            rendertargetbitmap.Render(dv);

            image.Source = rendertargetbitmap;
        }


        private void ResizeImage()
        {
            int newmappixelsize = int.Min((int)grid.ActualWidth / mapwidth, (int)grid.ActualHeight / mapheight);
            if (newmappixelsize != mappixelsize)
            {
                mappixelsize = newmappixelsize;
                image.Width = mappixelsize * mapwidth;
                image.Height = mappixelsize * mapheight;
                image.Margin = new Thickness(0, 0, 0, 0);
                rendertargetbitmap = new RenderTargetBitmap((int)image.Width, (int)image.Height, 0, 0, PixelFormats.Pbgra32);
            }
        }

        private void grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResizeImage();

        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            runpart = 1;
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            runpart = 2;
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            runpart = 0;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            running = false;
        }

        /// <summary>
        /// Converts HSL to RGB.
        /// </summary>
        /// <param name="hue">Hue 0-360</param>
        /// <param name="saturation">Saturation 0-100</param>
        /// <param name="luminance">Luminance 0-100</param>
        /// <returns>Color</returns>
        public static Color HSLToRGB(double hue, double saturation, double luminance)
        {
            (byte red, byte green, byte blue) rgb;
            hue %= 360;
            saturation /= 100;
            luminance /= 100;
            double c = (1 - Math.Abs(2 * luminance - 1)) * saturation;
            double x = c * (1 - Math.Abs((hue / 60) % 2 - 1));
            double m = luminance - c / 2;
            if (hue >= 0 && hue < 60)
            {
                rgb = ((byte)((c + m) * 255), (byte)((x + m) * 255), (byte)(m * 255));
            }
            else if (hue >= 60 && hue < 120)
            {
                rgb = ((byte)((x + m) * 255), (byte)((c + m) * 255), (byte)(m * 255));
            }
            else if (hue >= 120 && hue < 180)
            {
                rgb = ((byte)(m * 255), (byte)((c + m) * 255), (byte)((x + m) * 255));
            }
            else if (hue >= 180 && hue < 240)
            {
                rgb = ((byte)(m * 255), (byte)((x + m) * 255), (byte)((c + m) * 255));
            }
            else if (hue >= 240 && hue < 300)
            {
                rgb = ((byte)((x + m) * 255), (byte)(m * 255), (byte)((c + m) * 255));
            }
            else
            {
                rgb = ((byte)((c + m) * 255), (byte)(m * 255), (byte)((x + m) * 255));
            }
            return Color.FromRgb(rgb.red, rgb.green, rgb.blue);
        }

    }
}
