using System.Diagnostics;
using System.Numerics;
using WaveFunction.ARPG.Battle;
using WaveFunction.MagicSystemSketch;
using WaveFunction.Shared;

namespace WaveFunction.ARPG.Tiles
{
    public static class TileHelpers
    {
        public static readonly Vector2 TileSize = new Vector2(8, 8);

        public static Signature GetSignatures(this Signature targetList, Vector4[] pixels, int numPixels,
            Func<int, int> toIndex)
        {
            var elements = Enum.GetValues<Element>();
            for (int i = 0; i < numPixels; i++)
            {
                targetList = targetList.MixedWith(pixels[toIndex(i) % pixels.Length], elements[i % 8]);
            }

            return targetList;
        }

        public static void BuildTileset(this Vector4[,] image, Action<Vector4[]> testFunc)
        {
            var pixels = new Vector4[(int)(TileSize.X * TileSize.Y)];
            for (int y = 0; y < 4096; y += (int)TileSize.Y)
            {
                for (int x = 0; x < 4096; x += (int)TileSize.X)
                {
                    CopyTile(image, ref pixels, x, y);

                    if (pixels.Max(Alpha) == 0) continue;

                    testFunc(pixels);
                }
            }
        }


        public static byte Alpha(Vector4 x) => (byte)(x.Z * 255);

        private static void CopyTile(Vector4[,] image, ref Vector4[] pixels, int x, int y)
        {
            for (int py = 0; py < 8; py++)
            {
                for (int px = 0; px < 8; px++)
                {
                    pixels[TileSize.IndexOf(px, py)] = image[x + px, y + py];
                }
            }
        }
    }

    public class SetTile
    {
        public Dictionary<NavDir, Signature> Signatures { get; } =
            new Dictionary<NavDir, Signature>()
            {
                { NavDir.Central, new Signature(0, 0, 0, 0, 0, 0, 0, 0) },
                { NavDir.North, new Signature(0, 0, 0, 0, 0, 0, 0, 0) },
                { NavDir.East, new Signature(0, 0, 0, 0, 0, 0, 0, 0) },
                { NavDir.South, new Signature(0, 0, 0, 0, 0, 0, 0, 0) },
                { NavDir.West, new Signature(0, 0, 0, 0, 0, 0, 0, 0) },
                { NavDir.NorthEast, new Signature(0, 0, 0, 0, 0, 0, 0, 0) },
                { NavDir.SouthEast, new Signature(0, 0, 0, 0, 0, 0, 0, 0) },
                { NavDir.SouthWest, new Signature(0, 0, 0, 0, 0, 0, 0, 0) },
                { NavDir.NorthWest, new Signature(0, 0, 0, 0, 0, 0, 0, 0) },
            };

        public void FromPixels(Vector4[] pixelData)
        {
            Debug.Assert(Math.Abs(pixelData.Length - TileHelpers.TileSize.X * TileHelpers.TileSize.Y) < 0.001f);

            var centerHor = new Signature(0, 0, 0, 0, 0, 0, 0, 0);
            var centerVert = new Signature(0, 0, 0, 0, 0, 0, 0, 0);

            centerHor = centerHor.GetSignatures(pixelData, 8 * 8,
                static i => i);
            centerVert = centerVert.GetSignatures(pixelData, 8 * 8,
                static i => i * 7);

            Signatures[NavDir.Central] = centerHor.MixedWith(centerVert);

            Signatures[NavDir.North] = Signatures[NavDir.North].GetSignatures(pixelData, 8,
                static i => TileHelpers.TileSize.IndexOf(i, 0));
            Signatures[NavDir.East] = Signatures[NavDir.East].GetSignatures(pixelData, 8,
                static i => TileHelpers.TileSize.IndexOf(i, 7));
            Signatures[NavDir.South] = Signatures[NavDir.South].GetSignatures(pixelData, 8,
                static i => TileHelpers.TileSize.IndexOf(0, i));
            Signatures[NavDir.West] = Signatures[NavDir.West].GetSignatures(pixelData, 8,
                static i => TileHelpers.TileSize.IndexOf(7, i));

            Signatures[NavDir.NorthEast] = Signatures[NavDir.North]
                .MixedWith(Signatures[NavDir.East]);
            Signatures[NavDir.SouthEast] = Signatures[NavDir.South]
                .MixedWith(Signatures[NavDir.East]);
            Signatures[NavDir.SouthWest] = Signatures[NavDir.South]
                .MixedWith(Signatures[NavDir.West]);
            Signatures[NavDir.NorthWest] = Signatures[NavDir.North]
                .MixedWith(Signatures[NavDir.West]);
        }

        public Signature SideSignature(NavDir navDir) => Signatures[navDir];
    }

    public class Tileset
    {
        public List<SetTile> Tiles { get; } = new List<SetTile>();

        private void AddTile(Vector4[] pixels)
        {
            var toAdd = new SetTile();
            toAdd.FromPixels(pixels);
            Tiles.Add(toAdd);
        }

        public static Tileset AsTileset(Vector4[,] image)
        {
            var returned = new Tileset();
            image.BuildTileset(returned.AddTile);
            return returned;
        }
    }
}
