using System.Numerics;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using WaveFunction.ARPG.Battle;
using WaveFunction.ARPG.Tiles;
using WaveFunction.MagicSystemSketch;
using WaveFunction.Shared;

namespace TilemapBuilder
{
    public static class TileMapTester
    {
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

            Vector4[,] pix = new Vector4[image.Width, image.Height];
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    var old = image[x, y];
                    var r = old.R / 255f;
                    var g = old.G / 255f;
                    var b = old.B / 255f;
                    var a = old.A / 255f;
                    pix[x, y] = new Vector4(r, g, b, a);
                }
            }

            var set = Tileset.AsTileset(pix);
            _signaturesN = set.Tiles.Select(static s => s.Signatures[NavDir.North]).ToList();
            _signaturesE = set.Tiles.Select(static s => s.Signatures[NavDir.East]).ToList();
            _signaturesS = set.Tiles.Select(static s => s.Signatures[NavDir.South]).ToList();
            _signaturesW = set.Tiles.Select(static s => s.Signatures[NavDir.West]).ToList();
            _signaturesC = set.Tiles.Select(static s => s.Signatures[NavDir.Central]).ToList();
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
    }
}
