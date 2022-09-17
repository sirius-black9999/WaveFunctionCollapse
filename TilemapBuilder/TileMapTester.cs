using System.Numerics;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using WaveFunction.MagicSystemSketch;
using WaveFunction.Shared;

namespace TilemapBuilder
{
    public static class TileMapTester
    {
        private static readonly Vector2 TileSize = new Vector2(8, 8);
        public static readonly string Filename = "../../../../Plot.csv";

        private static List<Signature> _signaturesN = new List<Signature>();
        private static List<Signature> _signaturesE = new List<Signature>();
        private static List<Signature> _signaturesS = new List<Signature>();
        private static List<Signature> _signaturesW = new List<Signature>();
        private static List<Signature> _signaturesC = new List<Signature>();

        private static readonly float[][] DistinctN = new float[8][];
        private static readonly float[][] DistinctE = new float[8][];
        private static readonly float[][] DistinctS = new float[8][];
        private static readonly float[][] DistinctW = new float[8][];
        private static readonly float[][] DistinctC = new float[8][];

        public static void Run()
        {
            using var image = Image.Load<Argb32>("../../../../Assets/FullMap.png");

            TestImage(image, TestTile);
            Console.WriteLine($"Found {_signaturesC.Count} tiles");
            foreach (var element in Enum.GetValues<Element>())
            {
                var min = _signaturesC.Min(signature1 => signature1[element]);
                var avg = _signaturesC.Average(signature1 => signature1[element]);
                var max = _signaturesC.Max(signature1 => signature1[element]);
                Console.WriteLine("----------------------------------------------------------------------");
                Console.Write(
                    $"Minimum {element.ToString().Substring(0, 6)}: N:{(int)(100 * _signaturesN.Min(signature => signature[element]))}");
                Console.Write($"\tE:{(int)(100 * _signaturesE.Min(signature => signature[element]))}");
                Console.Write($"\tS:{(int)(100 * _signaturesS.Min(signature => signature[element]))}");
                Console.Write($"\tW:{(int)(100 * _signaturesW.Min(signature => signature[element]))}");
                Console.Write($"\tC:{(int)(100 * _signaturesC.Min(signature => signature[element]))}");
                Console.WriteLine(
                    $"\t EX: {_signaturesC.First(signature => Math.Abs(signature[element] - min) < 0.1f)}");
                Console.Write(
                    $"Average {element.ToString().Substring(0, 6)}: N:{(int)(100 * _signaturesN.Average(signature => signature[element]))}");
                Console.Write($"\tE:{(int)(100 * _signaturesE.Average(signature => signature[element]))}");
                Console.Write($"\tS:{(int)(100 * _signaturesS.Average(signature => signature[element]))}");
                Console.Write($"\tW:{(int)(100 * _signaturesW.Average(signature => signature[element]))}");
                Console.Write($"\tC:{(int)(100 * _signaturesC.Average(signature => signature[element]))}");
                Console.WriteLine(
                    $"\t EX: {_signaturesC.First(signature => Math.Abs(signature[element] - avg) < 0.1f)}");
                Console.Write(
                    $"Maximum {element.ToString().Substring(0, 6)}: N:{(int)(100 * _signaturesN.Max(signature => signature[element]))}");
                Console.Write($"\tE:{(int)(100 * _signaturesE.Max(signature => signature[element]))}");
                Console.Write($"\tS:{(int)(100 * _signaturesS.Max(signature => signature[element]))}");
                Console.Write($"\tW:{(int)(100 * _signaturesW.Max(signature => signature[element]))}");
                Console.Write($"\tC:{(int)(100 * _signaturesC.Max(signature => signature[element]))}");
                Console.WriteLine(
                    $"\t EX: {_signaturesC.First(signature => Math.Abs(signature[element] - max) < 0.1f)}");
            }

            Console.WriteLine("----------------------------------------------------------------------");

            Console.Write($"Dead N: {_signaturesN.Count(DeadSignature)} \n");
            Console.Write($"Dead E: {_signaturesE.Count(DeadSignature)} \n");
            Console.Write($"Dead S: {_signaturesS.Count(DeadSignature)} \n");
            Console.Write($"Dead W: {_signaturesW.Count(DeadSignature)} \n");
            Console.Write($"Dead C: {_signaturesC.Count(DeadSignature)} \n");
            for (int i = 0; i < 8; i++)
            {
                float Selector(Signature x)
                {
                    return x[(Element)i];
                }

                DistinctN[i] = _signaturesN.Select(Selector).Distinct().OrderBy(Nop).ToArray();
                DistinctE[i] = _signaturesE.Select(Selector).Distinct().OrderBy(Nop).ToArray();
                DistinctS[i] = _signaturesS.Select(Selector).Distinct().OrderBy(Nop).ToArray();
                DistinctW[i] = _signaturesW.Select(Selector).Distinct().OrderBy(Nop).ToArray();
                DistinctC[i] = _signaturesC.Select(Selector).Distinct().OrderBy(Nop).ToArray();
            }

            if (File.Exists(Filename))
                File.Delete(Filename);
            File.WriteAllText(Filename, "index, V1, N1, N2, N3, N4, N5, N6, N7, N8, " +
                                        "V1, E1, E2, E3, E4, E5, E6, E7, E8, " +
                                        "V1, S1, S2, S3, S4, S5, S6, S7, S8, " +
                                        "V1, W1, W2, W3, W4, W5, W6, W7, W8, " +
                                        "V1, C1, C2, C3, C4, C5, C6, C7, C8\n");
            WriteFileLine();
        }

        private static T Nop<T>(T f) => f;

        private static bool DeadSignature(Signature x)
        {
            return Enum.GetValues<Element>().Sum(e => x[e]) == 0;
        }

        private static void WriteFileLine()
        {
            for (int i = 0; i < 255; i++)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(i);
                WriteList(sb, _signaturesN, DistinctN, i);
                WriteList(sb, _signaturesE, DistinctE, i);
                WriteList(sb, _signaturesS, DistinctS, i);
                WriteList(sb, _signaturesW, DistinctW, i);
                WriteList(sb, _signaturesC, DistinctC, i);
                sb.Append('\n');
                File.AppendAllText(Filename, sb.ToString());
            }
        }

        private static void WriteList(StringBuilder sb, List<Signature> signatures, float[][] distinct, int index)
        {
            sb.Append(", ");
            if (distinct[0].Length > index)
                sb.Append(distinct[0][index]);
            for (int j = 0; j < 8; j++)
            {
                sb.Append(", ");
                if (distinct[j].Length > index)
                    sb.Append(signatures.Count(x => Math.Abs(x[(Element)j] - distinct[j][index]) < 0.001f));
            }
        }

        private static void TestImage(Image<Argb32> image, Action<Argb32[]> testFunc)
        {
            var pixels = new Argb32[(int)(TileSize.X * TileSize.Y)];
            for (int y = 0; y < 4096; y += 8)
            {
                for (int x = 0; x < 4096; x += 8)
                {
                    CopyTile(image, pixels, x, y);

                    if (pixels.Max(Alpha) == 0) continue;

                    testFunc(pixels);
                }
            }
        }

        public static byte Alpha(Argb32 x) => x.A;

        private static void CopyTile(Image<Argb32> image, Argb32[] pixels, int x, int y)
        {
            for (int py = 0; py < 8; py++)
            {
                for (int px = 0; px < 8; px++)
                {
                    pixels[TileSize.IndexOf(px, py)] = image[x + px, y + py];
                }
            }
        }

        private static void TestTile(Argb32[] pixels)
        {
            GetSignatures(pixels, 8, static i => TileSize.IndexOf(i, 0), ref _signaturesN);
            GetSignatures(pixels, 8, static i => TileSize.IndexOf(i, 7), ref _signaturesS);
            GetSignatures(pixels, 8, static i => TileSize.IndexOf(0, i), ref _signaturesW);
            GetSignatures(pixels, 8, static i => TileSize.IndexOf(7, i), ref _signaturesE);
            GetSignatures(pixels, 8 * 8, Nop, ref _signaturesC);
        }

        private static void GetSignatures(Argb32[] pixels, int numPixels, Func<int, int> toIndex,
            ref List<Signature> targetList)
        {
            var sig = new Signature(0, 0, 0, 0, 0, 0, 0, 0);
            var elements = Enum.GetValues<Element>();
            var col = new Vector3();
            for (int i = 0; i < numPixels; i++)
            {
                col.X = pixels[toIndex(i)].R / 255f;
                col.Y = pixels[toIndex(i)].G / 255f;
                col.Z = pixels[toIndex(i)].B / 255f;
                sig = sig.MixedWith(col, elements[i % 8]);
            }

            targetList.Add(sig);
        }
    }
}
