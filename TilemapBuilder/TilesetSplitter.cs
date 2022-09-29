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
            var files = GetFileList();
            Console.WriteLine($"Found {files.Count} sheets");
            Console.WriteLine($"Found {files.Count(IsImage)} png sheets");

            foreach (var img in files.Where(IsImage))
            {
                using var image = Image.Load(img);
                var sz = image.Size();
                AttemptSplit(sz, image);
            }
        }

        /// <summary> The AttemptSplit function attempts to split the image into a grid of tiles.</summary>
        ///
        /// <param name="sz"> The size of the image to split.</param>
        /// <param name="Image"> The image to split</param>
        ///
        /// <returns> A boolean value. this function returns true if the image was split successfully and false otherwise.</returns>
        private static void AttemptSplit(Size sz, Image image)
        {
            for (int tilesX = 4; tilesX < 32; tilesX++)
            {
                for (int tilesY = 4; tilesY < 64; tilesY++)
                {
                    SplitTile(sz, image, tilesY, tilesX);
                }
            }
        }

        /// <summary> The SplitTile function splits a tile into smaller tiles.</summary>
        ///
        /// <param name="sz"> Size of the image</param>
        /// <param name="Image"> The image to split</param>
        /// <param name="int"> The int.</param>
        /// <param name="int"> The int.</param>
        ///
        /// <returns> A list of tiles.</returns>
        private static void SplitTile(Size sz, Image image, int tilesY, int tilesX)
        {
            //if not an integer multiple, skip
            // ReSharper disable once PossibleLossOfFraction
            if (Math.Abs(sz.Height / (float)tilesY - sz.Height / tilesY) > 0.001f) return;
            // ReSharper disable once PossibleLossOfFraction
            if (Math.Abs(sz.Width / (float)tilesX - sz.Width / tilesX) > 0.001f) return;

            var tileSize = new Size(sz.Width / tilesX, sz.Height / tilesY);
            //do not split non-square tiles
            if (tileSize.Width != tileSize.Height) return;
            //do not split tiles too small
            if (tileSize.Width < 4 || tileSize.Height < 4) return;

            SplitMap(image, tileSize, new Size(tilesX, tilesY));
        }

        private static List<string> GetFileList()
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
            return files;
        }

        private static bool IsImage(string s) => s.EndsWith(".png");

        [SuppressMessage("ReSharper.DPA", "DPA0002: Excessive memory allocations in SOH",
            MessageId = "type: System.Byte[]; size: 243MB")]
        private static void SplitMap(Image image, Size tileSize, Size tileCount)
        {
            for (int x = 0; x < tileCount.Width; x++)
            {
                for (int y = 0; y < tileCount.Height; y++)
                {
                    var x1 = x;
                    var y1 = y;
                    image.Clone(context => context.Crop(new Rectangle(tileSize.Width * x1, tileSize.Height * y1,
                            tileSize.Width, tileSize.Height)).Resize(8, 8))
                        .SaveAsPng($"../../../../Assets/MapOut/Image {Counter++}.png");
                }
            }
        }
    }
}
