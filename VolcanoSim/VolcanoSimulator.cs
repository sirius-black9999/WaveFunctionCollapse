
using System.Numerics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using WaveFunction.Shared;

namespace VolcanoSim
{
    public static class VolcanoSimulator
    {
        
        private static readonly Vector2 size = new Vector2(4096, 4096);
        private static readonly byte[] Pixels = new byte[(int)(size.X * size.Y * 4)];
        private static VolcanoMapDef map = new VolcanoMapDef(1024, 1024);
        public static void Run()
        {
            CopyPixels();
            var resultImg = Image.LoadPixelData<Argb32>(Pixels, 4096, 4096);
            resultImg.SaveAsPng("../../../../Assets/RenderedMap.png");
        }

        private static void CopyPixels()
        {
            size.Foreach(static pos =>
            {
                var color = map.GetPixel(pos);
                var baseInd = size.IndexOf(pos) * 4;
                Pixels[baseInd] = 255;
                Pixels[baseInd + 1] = (byte)(color.X * 255);
                Pixels[baseInd + 2] = (byte)(color.Y * 255);
                Pixels[baseInd + 3] = (byte)(color.Z * 255);
            });
        }
    }
}
