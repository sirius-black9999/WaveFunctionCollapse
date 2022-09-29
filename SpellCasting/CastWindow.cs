using System.Numerics;
using Cairo;
using WaveFunction.MagicSystemSketch;
using WaveFunction.Shared;

namespace SpellCasting
{
    public class CastWindow : IScene<Context>
    {
        public void Render(Context cr, Vector2 size)
        {
            _size = size;
            DrawCursor(cr, size);
            DrawWheel(cr, size);
        }

        private Vector2 _size;

        private void DrawWheel(Context cr, Vector2 size)
        {
            SpellWheel.Draw(cr, size, Depth(), _currentRune);
        }

        private void DrawCursor(Context cr, Vector2 size)
        {
            cr.SetSourceRGB(0.0, 0.0, 0.0);
            cr.Rectangle(0, 0, size.X, size.Y);
            cr.Fill();

            var pos = GetMousePos();
            var depth = Depth();
            var held = IsPressed();
            cr.SetSourceRGB(Math.Clamp(-depth, 0, 1), Math.Clamp(depth, 0, 1), held ? 1 : 0.2);

            cr.Arc(pos.X, pos.Y, 10, 0, 2 * Math.PI);
            cr.Fill();

            for (var index = 1; index < _positions.Count; index++)
            {
                var start = _positions[index - 1];
                var end = _positions[index];
                cr.LineWidth = 1;
                cr.SetSourceRGB(Math.Clamp(-start.Z, 0, 1), Math.Clamp(start.Z, 0, 1), 1 - Math.Abs(start.Z));
                cr.MoveTo(start.X, start.Y);
                cr.SetSourceRGB(Math.Clamp(-end.Z, 0, 1), Math.Clamp(end.Z, 0, 1), 1 - Math.Abs(end.Z));
                cr.LineTo(end.X, end.Y);
                cr.Stroke();
            }
        }


        public void Update(int frameCount, Vector2 pos)
        {
            if (IsPressed())
            {
                var depth = Depth();

                _positions.Add(new Vector3(pos.X, pos.Y, (float)depth));
                SpellWheel.Overlaps(_currentRune, pos, _size, Depth());
            }
            else
            {
                if (_positions.Any())
                {
                    TryCast();
                }

                _positions.Clear();
            }
        }

        private void TryCast()
        {
            if (_currentRune.Any)
            {
                _casting.Inscribe(_currentRune.Resolve());
                _currentRune = new Gesture();
            }
            else if (_casting.Any)
            {
                var cast = _book.Cast(_casting);
                Console.WriteLine($"\nCasting {cast.Name}\n" +
                                  $"Hardness: {cast.Hardness}\t Heat: {cast.Heat}\n" +
                                  $"Entropy: {cast.Entropy}\t" +
                                  $"Luminance: {cast.Luminance}\n" +
                                  $"Manifold: {cast.Manifold}\t" +
                                  $"Density: {cast.Density}\n" +
                                  $"Risk: {cast.Risk}\t" +
                                  $"Range: {cast.Range}\n" +
                                  $"Stability: {cast.Stability}");
                _casting = new Incantation();
            }
        }

        public void MouseClick(Vector2 pos)
        {
            //Unused
        }

        public void MouseRelease(Vector2 pos)
        {
            //Unused
        }

        public void MouseDrag(Vector2 pos)
        {
            //Unused
        }

        public void MouseScroll(bool up)
        {
            //Unused
        }

        private readonly SpellBook _book = Bookmaker.NewBook(SpellBooks.Enchants);
        private Incantation _casting = new Incantation();
        private Gesture _currentRune = new Gesture();
        private readonly List<Vector3> _positions = new List<Vector3>();
        public Func<Vector2> GetMousePos { get; set; } = static () => new Vector2();
        public Func<bool> IsPressed { get; set; } = static () => false;
        public Func<double> Depth { get; set; } = static () => 0;
    }
}
