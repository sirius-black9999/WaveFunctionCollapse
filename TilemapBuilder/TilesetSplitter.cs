using System.Diagnostics.CodeAnalysis;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace TilemapBuilder
{
    public static class TilesetSplitter
    {
        public static int Counter { get; set; }

        public static void Run()
        {
            var fileList = Directory.GetDirectories("../../../../Assets/SplitSrc");
            var queue = new Queue<string>(fileList);
            var files = new List<string>();
            while (queue.Any())
            {
                var current = queue.Dequeue();
                var subfiles = Directory.GetDirectories(current);
                foreach (var subfile in subfiles)
                {
                    queue.Enqueue(subfile);
                }

                files.AddRange(Directory.GetFiles(current));
            }

            files.AddRange(Directory.GetFiles("../../../../Assets/SplitSrc"));
            Console.WriteLine($"Found {files.Count} sheets");
            Console.WriteLine($"Found {files.Count(s => s.EndsWith(".png"))} png sheets");

            foreach (var img in files.Where(s => s.EndsWith(".png")))
            {
                using var image = Image.Load(img);
                var sz = image.Size();
                for (int tilesX = 4; tilesX < 32; tilesX++)
                {
                    for (int tilesY = 4; tilesY < 64; tilesY++)
                    {
                        //if not an integer multiple, skip
                        if (sz.Height / (float)tilesY != sz.Height / tilesY) continue;
                        if (sz.Width / (float)tilesX != sz.Width / tilesX) continue;
                        var tileSize = new Size(sz.Width / tilesX, sz.Height / tilesY);
                        //do not split non-square tiles
                        if (tileSize.Width != tileSize.Height) continue;
                        //do not split tiles too small
                        if (tileSize.Width < 4 || tileSize.Height < 4) continue;

                        splitMap(image, tileSize, new Size(tilesX, tilesY));
                    }
                }
            }
        }

        [SuppressMessage("ReSharper.DPA", "DPA0002: Excessive memory allocations in SOH",
            MessageId = "type: System.Byte[]; size: 243MB")]
        private static void splitMap(Image image, Size tileSize, Size tileCount)
        {
            for (int x = 0; x < tileCount.Width; x++)
            {
                for (int y = 0; y < tileCount.Height; y++)
                {
                    image.Clone(context => context.Crop(new Rectangle(tileSize.Width * x, tileSize.Height * y,
                            tileSize.Width, tileSize.Height)).Resize(8, 8))
                        .SaveAsPng($"../../../../Assets/MapOut/Image {Counter++}.png");
                }
            }
        }
    }
}
