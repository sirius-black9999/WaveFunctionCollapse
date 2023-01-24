using System.Numerics;
using Cairo;
using WaveFunction.MagicSystemSketch;

namespace SpellCasting
{
    public static class SpellWheel
    {
        private static Vector3 ScaleBy(Vector3 input, Vector2 scalar) =>
            new Vector3(input.X * scalar.X, input.Y * scalar.Y, input.Z);

        private static readonly Dictionary<Element, Vector2> GlyphPositions = new Dictionary<Element, Vector2>()
        {
            { Element.Solidum, new Vector2(0.5f, 0.2f) },
            { Element.Calor, new Vector2(0.5f, 0.8f) },
            { Element.Entropia, new Vector2(0.2f, 0.5f) },
            { Element.Lumines, new Vector2(0.8f, 0.5f) },
            { Element.Natura, new Vector2(0.3f, 0.3f) },
            { Element.Densitas, new Vector2(0.7f, 0.7f) },
            { Element.Harmonius, new Vector2(0.3f, 0.7f) },
            { Element.Motus, new Vector2(0.7f, 0.3f) },
        };

        private static Vector3 GlyphPos(Element e, double depth, Vector2 size) =>
            ScaleBy(new Vector3(GlyphPositions[e].X, GlyphPositions[e].Y, (float)depth), size);

        public static void Draw(Context cr, Vector2 size, double depth, Gesture gest)
        {
            foreach (var element in Enum.GetValues<Element>())
            {
                MakeRune(element).Draw(cr, GlyphPos(element, depth, size), gest.Has(element));
            }
        }

        public static Glyph MakeRune(Element e) =>
            new Dictionary<Element, Glyph>()
            {
                { Element.Solidum, SolidumGlyph },
                { Element.Calor, FebrisGlyph },
                { Element.Entropia, OrdinemGlyph },
                { Element.Lumines, LuminesGlyph },
                { Element.Natura, VariasGlyph },
                { Element.Densitas, InertiaeGlyph },
                { Element.Harmonius, SubsidiumGlyph },
                { Element.Motus, SpatiumGlyph },
            }[e];

        private static readonly int GlyphSize = 20;

        private static Vector2 GlyphLocal(float x, float y) => 
            new Vector2(x * GlyphSize, y * GlyphSize);

        public static GlyphMaker BaseGlyph(Vector3 negative, Vector3 neutral, Vector3 positive) =>
            Glyph.Make()
                .WithColor(negative, Glyph.Position.Negative)
                .WithColor(neutral, Glyph.Position.Neutral)
                .WithColor(positive, Glyph.Position.Positive)
                .WithLine(GlyphLocal(-1, -1), GlyphLocal(1, -1))
                .WithLine(GlyphLocal(1, -1), GlyphLocal(1, 1))
                .WithLine(GlyphLocal(1, 1), GlyphLocal(-1, 1))
                .WithLine(GlyphLocal(-1, 1), GlyphLocal(-1, -1));

        public static Glyph SolidumGlyph =>
            BaseGlyph(Colors.SolidumNegative, Colors.SolidumNeutral, Colors.SolidumPositive)
                .WithLinePos(GlyphLocal(-0.6f, 0.7f), GlyphLocal(-0.6f, 0.2f))
                .WithLinePos(GlyphLocal(0.6f, 0.7f), GlyphLocal(0.6f, 0.2f))
                .WithLinePos(GlyphLocal(0, 0.6f), GlyphLocal(0, -0.7f))
                .WithLinePos(GlyphLocal(-0.6f, 0.7f), GlyphLocal(0.6f, 0.5f))
                .WithLineNeg(GlyphLocal(-0.6f, 0.7f), GlyphLocal(-0.4f, 0))
                .WithLineNeg(GlyphLocal(-0.4f, -0.7f), GlyphLocal(-0.4f, 0))
                .WithLineNeg(GlyphLocal(0.4f, 0.7f), GlyphLocal(0.4f, -0.7f))
                .WithLineNeg(GlyphLocal(0, 0.5f), GlyphLocal(0, -0.7f))
                .Finish();

        public static Glyph FebrisGlyph =>
            BaseGlyph(Colors.FebrisNegative, Colors.FebrisNeutral, Colors.FebrisPositive)
                .WithLinePos(GlyphLocal(0, -0.7f), GlyphLocal(-0.1f, 0))
                .WithLinePos(GlyphLocal(-0.1f, -0.7f), GlyphLocal(-0.1f, 0))
                .WithLinePos(GlyphLocal(-0.1f, 0), GlyphLocal(-0.4f, 0.7f))
                .WithLinePos(GlyphLocal(-0.1f, 0), GlyphLocal(0.4f, 0.7f))
                .WithLinePos(GlyphLocal(-0.1f, 0), GlyphLocal(0.5f, 0.7f))
                .WithLinePos(GlyphLocal(-0.3f, 0.2f), GlyphLocal(-0.5f, -0.2f))
                .WithLinePos(GlyphLocal(-0.4f, 0.2f), GlyphLocal(-0.5f, -0.2f))
                .WithLinePos(GlyphLocal(0.6f, -0.3f), GlyphLocal(0.3f, 0))
                .WithLinePos(GlyphLocal(0.5f, -0.3f), GlyphLocal(0.3f, 0))
                .WithLineNeg(GlyphLocal(-0.7f, 0), GlyphLocal(-0.2f, 0))
                .WithLineNeg(GlyphLocal(-0.2f, 0), GlyphLocal(-0.6f, 0.7f))
                .WithLineNeg(GlyphLocal(-0.5f, -0.4f), GlyphLocal(-0.3f, -0.2f))
                .WithLineNeg(GlyphLocal(0, -0.7f), GlyphLocal(0, 0.7f))
                .WithLineNeg(GlyphLocal(0, 0.7f), GlyphLocal(-0.2f, 0.9f))
                .WithLineNeg(GlyphLocal(0, 0), GlyphLocal(0.5f, -0.6f))
                .WithLineNeg(GlyphLocal(0, 0), GlyphLocal(0.7f, 0.7f))
                .Finish();

        public static Glyph OrdinemGlyph =>
            BaseGlyph(Colors.OrdinemNegative, Colors.OrdinemNeutral, Colors.OrdinemPositive)
                .WithLinePos(GlyphLocal(-0.7f, -0.7f), GlyphLocal(0.7f, -0.7f))
                .WithLinePos(GlyphLocal(-0.6f, 0), GlyphLocal(0.6f, 0))
                .WithLinePos(GlyphLocal(-0.8f, 0.7f), GlyphLocal(0.8f, 0.7f))
                .WithLinePos(GlyphLocal(0, -0.7f), GlyphLocal(0, 0.7f))
                .WithLinePos(GlyphLocal(0, 0), GlyphLocal(0.4f, 0.4f))
                .WithLineNeg(GlyphLocal(-0.7f, -0.5f), GlyphLocal(0.7f, -0.5f))
                .WithLineNeg(GlyphLocal(-0.6f, 0), GlyphLocal(0.6f, 0))
                .WithLineNeg(GlyphLocal(-0.8f, 0.7f), GlyphLocal(0.8f, 0.7f))
                .WithLineNeg(GlyphLocal(0, -0.7f), GlyphLocal(0, 0.7f))
                .WithLineNeg(GlyphLocal(-0.6f, -0.7f), GlyphLocal(-0.8f, 0))
                .Finish();

        public static Glyph LuminesGlyph =>
            BaseGlyph(Colors.LuminesNegative, Colors.LuminesNeutral, Colors.LuminesPositive)
                .WithLinePos(GlyphLocal(-0.5f, -0.5f), GlyphLocal(0.5f, -0.5f))
                .WithLinePos(GlyphLocal(-0.5f, 0.5f), GlyphLocal(0.5f, 0.5f))
                .WithLinePos(GlyphLocal(-0.5f, 0.1f), GlyphLocal(0.5f, 0.1f))
                .WithLinePos(GlyphLocal(-0.5f, -0.5f), GlyphLocal(-0.5f, 0.7f))
                .WithLinePos(GlyphLocal(0.5f, -0.5f), GlyphLocal(0.5f, 0.7f))
                .WithLineNeg(GlyphLocal(-0.3f, -0.7f), GlyphLocal(0.7f, -0.7f))
                .WithLineNeg(GlyphLocal(-0.3f, -0.4f), GlyphLocal(0.7f, -0.4f))
                .WithLineNeg(GlyphLocal(-0.3f, 0.1f), GlyphLocal(0.7f, 0.1f))
                .WithLineNeg(GlyphLocal(-0.3f, -0.7f), GlyphLocal(-0.3f, 0.1f))
                .WithLineNeg(GlyphLocal(-0.3f, 0.1f), GlyphLocal(-0.7f, 0.7f))
                .WithLineNeg(GlyphLocal(0.7f, -0.7f), GlyphLocal(0.7f, 0.7f))
                .WithLineNeg(GlyphLocal(0.7f, 0.7f), GlyphLocal(0.5f, 0.5f))
                .Finish();

        public static Glyph VariasGlyph =>
            BaseGlyph(Colors.VariasNegative, Colors.VariasNeutral, Colors.VariasPositive)
                .WithLinePos(GlyphLocal(-0.6f, -0.5f), GlyphLocal(0.6f, -0.5f))
                .WithLinePos(GlyphLocal(-0.5f, -0.7f), GlyphLocal(-0.7f, -0.3f))
                .WithLinePos(GlyphLocal(0.7f, -0.7f), GlyphLocal(0.5f, -0.3f))
                .WithLinePos(GlyphLocal(0, -0.5f), GlyphLocal(0, -0.8f))
                .WithLinePos(GlyphLocal(-0.2f, -0.3f), GlyphLocal(0.2f, -0.3f))
                .WithLinePos(GlyphLocal(-0.5f, 0), GlyphLocal(0.5f, 0))
                .WithLinePos(GlyphLocal(0, -0.3f), GlyphLocal(0, 0.7f))
                .WithLinePos(GlyphLocal(0, 0.7f), GlyphLocal(-0.2f, 0.5f))
                .WithLineNeg(GlyphLocal(-0.7f, -0.3f), GlyphLocal(-0.3f, -0.8f))
                .WithLineNeg(GlyphLocal(-0.5f, -0.6f), GlyphLocal(0.5f, -0.6f))
                .WithLineNeg(GlyphLocal(0, -0.6f), GlyphLocal(0, 0.7f))
                .WithLineNeg(GlyphLocal(-0.3f, -0.3f), GlyphLocal(0.3f, -0.3f))
                .WithLineNeg(GlyphLocal(-0.8f, 0), GlyphLocal(0.8f, 0))
                .WithLineNeg(GlyphLocal(-0.3f, -0.3f), GlyphLocal(-0.3f, 0))
                .Finish();

        public static Glyph InertiaeGlyph =>
            BaseGlyph(Colors.InertiaeNegative, Colors.InertiaeNeutral, Colors.InertiaePositive)
                .WithLinePos(GlyphLocal(-0.6f, -0.6f), GlyphLocal(0.6f, -0.6f))
                .WithLinePos(GlyphLocal(0, -0.6f), GlyphLocal(0, 0.6f))
                .WithLinePos(GlyphLocal(0, -0.2f), GlyphLocal(0.4f, 0))
                .WithLineNeg(GlyphLocal(-0.6f, 0.6f), GlyphLocal(0.6f, 0.6f))
                .WithLineNeg(GlyphLocal(0, -0.6f), GlyphLocal(0, 0.6f))
                .WithLineNeg(GlyphLocal(0, 0), GlyphLocal(0.4f, 0))
                .Finish();

        public static Glyph SubsidiumGlyph =>
            BaseGlyph(Colors.SubsidiumNegative, Colors.SubsidiumNeutral, Colors.SubsidiumPositive)
                .WithLinePos(GlyphLocal(-0.4f, -0.0f), GlyphLocal(-0.6f, 0.4f))
                .WithLinePos(GlyphLocal(-0.2f, -0.0f), GlyphLocal(0.1f, 0.5f))
                .WithLinePos(GlyphLocal(0.1f, 0.5f), GlyphLocal(0.7f, 0.7f))
                .WithLinePos(GlyphLocal(0.2f, -0.1f), GlyphLocal(0.4f, 0.2f))
                .WithLinePos(GlyphLocal(0.5f, -0.3f), GlyphLocal(0.7f, 0))
                .WithLinePos(GlyphLocal(0.7f, 0.7f), GlyphLocal(0.5f, 0.5f))
                .WithLineNeg(GlyphLocal(-0.4f, 0.4f), GlyphLocal(-0.6f, 0.6f))
                .WithLineNeg(GlyphLocal(-0.2f, 0.2f), GlyphLocal(0.1f, 0.6f))
                .WithLineNeg(GlyphLocal(0.1f, 0.6f), GlyphLocal(0.7f, 0.7f))
                .WithLineNeg(GlyphLocal(0.2f, 0.3f), GlyphLocal(0.4f, 0.5f))
                .WithLineNeg(GlyphLocal(0.5f, 0.2f), GlyphLocal(0.7f, 0.4f))
                .WithLineNeg(GlyphLocal(0.7f, 0.7f), GlyphLocal(0.5f, 0.5f))
                .WithLineNeg(GlyphLocal(-0.7f, 0), GlyphLocal(0.7f, 0))
                .WithLineNeg(GlyphLocal(-0.5f, -0.2f), GlyphLocal(0.5f, -0.2f))
                .WithLineNeg(GlyphLocal(-0.5f, -0.5f), GlyphLocal(0.5f, -0.5f))
                .WithLineNeg(GlyphLocal(-0.7f, -0.7f), GlyphLocal(0.7f, -0.7f))
                .WithLineNeg(GlyphLocal(-0.5f, -0.2f), GlyphLocal(-0.5f, -0.5f))
                .WithLineNeg(GlyphLocal(0.5f, -0.2f), GlyphLocal(0.5f, -0.5f))
                .WithLineNeg(GlyphLocal(0.2f, 0), GlyphLocal(0.2f, -0.7f))
                .WithLineNeg(GlyphLocal(-0.2f, 0), GlyphLocal(-0.2f, -0.7f))
                .Finish();

        public static Glyph SpatiumGlyph =>
            BaseGlyph(Colors.SpatiumNegative, Colors.SpatiumNeutral, Colors.SpatiumPositive)
                .WithLinePos(GlyphLocal(-0.4f, -0.5f), GlyphLocal(-0.7f, 0.7f))
                .WithLinePos(GlyphLocal(-0.4f, -0.5f), GlyphLocal(0.7f, -0.5f))
                .WithLinePos(GlyphLocal(0, -0.5f), GlyphLocal(0, -0.7f))
                .WithLinePos(GlyphLocal(0, -0.2f), GlyphLocal(-0.4f, 0.5f))
                .WithLinePos(GlyphLocal(-0.4f, 0.5f), GlyphLocal(0.4f, 0.4f))
                .WithLinePos(GlyphLocal(0.2f, 0.2f), GlyphLocal(0.6f, 0.6f))
                .WithLineNeg(GlyphLocal(0, -0.7f), GlyphLocal(0, 0.6f))
                .WithLineNeg(GlyphLocal(0, 0.6f), GlyphLocal(-0.2f, 0.4f))
                .WithLineNeg(GlyphLocal(-0.3f, -0.3f), GlyphLocal(-0.4f, -0.0f))
                .WithLineNeg(GlyphLocal(0.3f, -0.3f), GlyphLocal(0.4f, -0.0f))
                .Finish();

        public static void Overlaps(Gesture currentRune, Vector2 pos, Vector2 size, double depth)
        {
            foreach (Element element in
                     from element in Enum.GetValues<Element>()
                     let position = GlyphPos(element, depth, size)
                     let glyphCoord = new Vector2(position.X, position.Y)
                     let delta = pos - glyphCoord
                     where delta.LengthSquared() < 100
                     select element)
            {
                if (depth > 0)
                    currentRune.Record(element.Positive(), depth);
                if (depth < 0)
                    currentRune.Record(element.Negative(), -depth);
            }
        }
    }
}
