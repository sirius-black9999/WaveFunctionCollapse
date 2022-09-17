using System.Drawing;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Color = SixLabors.ImageSharp.Color;
using Size = SixLabors.ImageSharp.Size;

namespace TilemapBuilder
{
    public static class TilesetConverter
    {
        public static void Run()
        {
            var fileList = Directory.GetDirectories("../../../../Assets/MapSrc");
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
            files.AddRange(Directory.GetFiles("../../../../Assets/MapSrc"));
            Console.WriteLine($"Found {files.Count} files");
            Console.WriteLine($"Found {files.Count(s => s.EndsWith(".png"))} png files");
            foreach (var img in files.Where(s => s.EndsWith(".png")))
            {
                using var image = Image.Load(img);

                image.Mutate(x => x
                    .Resize(8, 8));

                image.Save($"../../../../Assets/MapOut/Image {TilesetSplitter.Counter++}.png");
            }
        }
    }
}
