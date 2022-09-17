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
        private static readonly Vector2 tileSize = new Vector2(8, 8);
        private static readonly string filename = "../../../../Plot.csv";

        private static List<Signature> SignaturesN = new List<Signature>();
        private static List<Signature> SignaturesE = new List<Signature>();
        private static List<Signature> SignaturesS = new List<Signature>();
        private static List<Signature> SignaturesW = new List<Signature>();
        private static List<Signature> SignaturesC = new List<Signature>();

        private static float[][] DistinctN = new float[8][];
        private static float[][] DistinctE = new float[8][];
        private static float[][] DistinctS = new float[8][];
        private static float[][] DistinctW = new float[8][];
        private static float[][] DistinctC = new float[8][];

        public static void Run()
        {
            using var image = Image.Load<Argb32>("../../../../Assets/FullMap.png");

            TestImage(image, TestTile);
            Console.WriteLine($"Found {SignaturesC.Count} tiles");
            foreach (var element in Enum.GetValues<Element>())
            {
                var min = SignaturesC.Min(signature1 => signature1[element]);
                var avg = SignaturesC.Average(signature1 => signature1[element]);
                var max = SignaturesC.Max(signature1 => signature1[element]);
                Console.WriteLine("----------------------------------------------------------------------");
                Console.Write(
                    $"Minimum {element.ToString().Substring(0, 6)}: N:{(int)(100 * SignaturesN.Min(signature => signature[element]))}");
                Console.Write($"\tE:{(int)(100 * SignaturesE.Min(signature => signature[element]))}");
                Console.Write($"\tS:{(int)(100 * SignaturesS.Min(signature => signature[element]))}");
                Console.Write($"\tW:{(int)(100 * SignaturesW.Min(signature => signature[element]))}");
                Console.Write($"\tC:{(int)(100 * SignaturesC.Min(signature => signature[element]))}");
                Console.WriteLine(
                    $"\t EX: {SignaturesC.First(signature => Math.Abs(signature[element] - min) < 0.1f)}");
                Console.Write(
                    $"Average {element.ToString().Substring(0, 6)}: N:{(int)(100 * SignaturesN.Average(signature => signature[element]))}");
                Console.Write($"\tE:{(int)(100 * SignaturesE.Average(signature => signature[element]))}");
                Console.Write($"\tS:{(int)(100 * SignaturesS.Average(signature => signature[element]))}");
                Console.Write($"\tW:{(int)(100 * SignaturesW.Average(signature => signature[element]))}");
                Console.Write($"\tC:{(int)(100 * SignaturesC.Average(signature => signature[element]))}");
                Console.WriteLine(
                    $"\t EX: {SignaturesC.First(signature => Math.Abs(signature[element] - avg) < 0.1f)}");
                Console.Write(
                    $"Maximum {element.ToString().Substring(0, 6)}: N:{(int)(100 * SignaturesN.Max(signature => signature[element]))}");
                Console.Write($"\tE:{(int)(100 * SignaturesE.Max(signature => signature[element]))}");
                Console.Write($"\tS:{(int)(100 * SignaturesS.Max(signature => signature[element]))}");
                Console.Write($"\tW:{(int)(100 * SignaturesW.Max(signature => signature[element]))}");
                Console.Write($"\tC:{(int)(100 * SignaturesC.Max(signature => signature[element]))}");
                Console.WriteLine(
                    $"\t EX: {SignaturesC.First(signature => Math.Abs(signature[element] - max) < 0.1f)}");
            }

            Console.WriteLine("----------------------------------------------------------------------");

            Console.Write($"Dead N: {SignaturesN.Count(x => Enum.GetValues<Element>().Sum(e => x[e]) == 0)} \n");
            Console.Write($"Dead E: {SignaturesE.Count(x => Enum.GetValues<Element>().Sum(e => x[e]) == 0)} \n");
            Console.Write($"Dead S: {SignaturesS.Count(x => Enum.GetValues<Element>().Sum(e => x[e]) == 0)} \n");
            Console.Write($"Dead W: {SignaturesW.Count(x => Enum.GetValues<Element>().Sum(e => x[e]) == 0)} \n");
            Console.Write($"Dead C: {SignaturesC.Count(x => Enum.GetValues<Element>().Sum(e => x[e]) == 0)} \n");
            for (int i = 0; i < 8; i++)
            {
                DistinctN[i] = SignaturesN.Select(x => x[(Element)i]).Distinct().OrderBy(f => f).ToArray();
                DistinctE[i] = SignaturesE.Select(x => x[(Element)i]).Distinct().OrderBy(f => f).ToArray();
                DistinctS[i] = SignaturesS.Select(x => x[(Element)i]).Distinct().OrderBy(f => f).ToArray();
                DistinctW[i] = SignaturesW.Select(x => x[(Element)i]).Distinct().OrderBy(f => f).ToArray();
                DistinctC[i] = SignaturesC.Select(x => x[(Element)i]).Distinct().OrderBy(f => f).ToArray();
            }

            if (File.Exists(filename))
                File.Delete(filename);
            File.WriteAllText(filename, "index, V1, N1, N2, N3, N4, N5, N6, N7, N8, " +
                                        "V1, E1, E2, E3, E4, E5, E6, E7, E8, " +
                                        "V1, S1, S2, S3, S4, S5, S6, S7, S8, " +
                                        "V1, W1, W2, W3, W4, W5, W6, W7, W8, " +
                                        "V1, C1, C2, C3, C4, C5, C6, C7, C8\n");
            WriteFileLine();
        }

        private static void WriteFileLine()
        {
            for (int i = 0; i < 255; i++)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(i);
                WriteList(sb, SignaturesN, DistinctN, i);
                WriteList(sb, SignaturesE, DistinctE, i);
                WriteList(sb, SignaturesS, DistinctS, i);
                WriteList(sb, SignaturesW, DistinctW, i);
                WriteList(sb, SignaturesC, DistinctC, i);
                sb.Append('\n');
                File.AppendAllText(filename, sb.ToString());
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
            
            var pixels = new Argb32[(int)(tileSize.X * tileSize.Y)];
            for (int y = 0; y < 4096; y += 8)
            {
                for (int x = 0; x < 4096; x += 8)
                {
                    for (int py = 0; py < 8; py++)
                    {
                        for (int px = 0; px < 8; px++)
                        {
                            pixels[tileSize.IndexOf(px, py)] = image[x + px, y + py];
                        }
                    }

                    if (pixels.Max(x => x.A) == 0) continue;
                    testFunc(pixels);
                }
            }
        }

        private static void TestTile(Argb32[] pixels)
        {
            GetSignatures(pixels, 8, i => tileSize.IndexOf(i, 0), ref SignaturesN);
            GetSignatures(pixels, 8, i => tileSize.IndexOf(i, 7), ref SignaturesS);
            GetSignatures(pixels, 8, i => tileSize.IndexOf(0, i), ref SignaturesW);
            GetSignatures(pixels, 8, i => tileSize.IndexOf(7, i), ref SignaturesE);
            GetSignatures(pixels, 8 * 8, i => i, ref SignaturesC);
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
