using System.Collections;
using System.Numerics;
using Cairo;
using GLib;

namespace SpellCasting
{
    public class Glyph
    {
        public enum Position
        {
            Positive,
            Neutral,
            Negative
        }

        private Vector3 _highCol;
        private Vector3 _midCol;
        private Vector3 _lowCol;
        private List<Tuple<Vector2, Vector2>> _linesPos = new List<Tuple<Vector2, Vector2>>();
        private List<Tuple<Vector2, Vector2>> _linesNeg = new List<Tuple<Vector2, Vector2>>();

        public Glyph(GlyphMaker maker)
        {
            _highCol = maker.HighCol;
            _midCol = maker.MidCol;
            _lowCol = maker.LowCol;
            _linesPos = maker.LinesPos;
            _linesNeg = maker.LinesNeg;
        }

        public void Draw(Context gr, Vector3 pos, bool contained)
        {
            var depth = pos.Z;
            var col = _midCol;
            var lines = _linesNeg;
            if (depth > 0)
            {
                col = Vector3.Lerp(_midCol, _highCol, depth);
                lines = _linesPos;
            }
            if (depth < 0)
                col = Vector3.Lerp(_midCol, _lowCol, -depth);

            if (contained)
                col /= 2;
            foreach (var line in lines)
            {
                gr.SetSourceRGB(col.X, col.Y, col.Z);
                gr.MoveTo(line.Item1.X + pos.X, line.Item1.Y + pos.Y);
                gr.LineTo(line.Item2.X + pos.X, line.Item2.Y + pos.Y);
                gr.Stroke();
            }
        }

        public static GlyphMaker Make()
        {
            return new GlyphMaker();
        }
    }

    public class GlyphMaker
    {
        public Glyph Finish()
        {
            return new Glyph(this);
        }

        public GlyphMaker WithColor(Vector3 color, Glyph.Position At)
        {
            new Dictionary<Glyph.Position, Action<Vector3>>()
            {
                { Glyph.Position.Positive, col => HighCol = col },
                { Glyph.Position.Neutral, col => MidCol = col },
                { Glyph.Position.Negative, col => LowCol = col },
            }[At](color);
            return this;
        }

        public GlyphMaker WithLine(Vector2 begin, Vector2 end) => WithLinePos(begin, end).WithLineNeg(begin, end);

        public GlyphMaker WithLinePos(Vector2 begin, Vector2 end)
        {
            LinesPos.Add(new Tuple<Vector2, Vector2>(begin, end));
            return this;
        }

        public GlyphMaker WithLineNeg(Vector2 begin, Vector2 end)
        {
            LinesNeg.Add(new Tuple<Vector2, Vector2>(begin, end));
            return this;
        }

        public Vector3 HighCol { get; private set; }
        public Vector3 MidCol { get; private set; }
        public Vector3 LowCol { get; private set; }
        public List<Tuple<Vector2, Vector2>> LinesPos { get; } = new List<Tuple<Vector2, Vector2>>();
        public List<Tuple<Vector2, Vector2>> LinesNeg { get; } = new List<Tuple<Vector2, Vector2>>();
    }
}
