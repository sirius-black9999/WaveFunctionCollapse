using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace TilemapBuilder
{
    public static class TilesetMerger
    {
        private static readonly byte[] Pixels = new byte[4096 * 4096 * 4];

        public static void Run()
        {
            var fileList = Directory.GetFiles("../../../../Assets/MapOut");
            Console.WriteLine(fileList.Length);
            var resultImg = Image.LoadPixelData<Argb32>(Pixels, 4096, 4096);
            var pt = new Point(0, 0);
            foreach (var img in fileList.Where(s => s.EndsWith(".png")))
            {
                using var image = Image.Load<Argb32>(img);
                var imgPix = new Argb32[8 * 8];
                int i = 0;
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        imgPix[i++] = image[x, y];
                    }
                }

                if (imgPix.Min(argb32 => argb32.A) > 254 || imgPix.Max(argb32 => argb32.A) == 0) continue;

                resultImg.Mutate(target => target.DrawImage(image, pt, 1));
                pt.X += 8;
                if (pt.X == 4096)
                {
                    pt.X = 0;
                    pt.Y += 8;
                }

                if (pt.Y == 4096)
                {
                    break;
                }
            }

            resultImg.SaveAsPng("../../../../Assets/FullMap.png");
        }
    }
}
