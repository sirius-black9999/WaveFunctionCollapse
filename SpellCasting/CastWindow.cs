using System.Numerics;
using Cairo;
using WaveFunction.MagicSystemSketch;
using WaveFunctionCollapse.Scenes;

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
            SpellWheel.Draw(cr, size, Depth(), currentRune);
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

            for (var index = 1; index < positions.Count; index++)
            {
                var start = positions[index - 1];
                var end = positions[index];
                cr.LineWidth = 1;
                cr.SetSourceRGB(Math.Clamp(-start.Z, 0, 1), Math.Clamp(start.Z, 0, 1), 1 - Math.Abs(start.Z));
                cr.MoveTo(start.X, start.Y);
                cr.SetSourceRGB(Math.Clamp(-end.Z, 0, 1), Math.Clamp(end.Z, 0, 1), 1 - Math.Abs(end.Z));
                cr.LineTo(end.X, end.Y);
                cr.Stroke();
            }
        }


        public void Update(int frameCount, Vector2 mousePos)
        {
            if (IsPressed())
            {
                var pos = GetMousePos();
                var depth = Depth();

                positions.Add(new Vector3(pos.X, pos.Y, (float)depth));
                SpellWheel.Overlaps(currentRune, pos, _size, Depth());
            }
            else
            {
                if (positions.Any())
                {
                    if (currentRune.Any)
                    {
                        casting.Inscribe(currentRune.Resolve());
                        currentRune = new Gesture();
                    }
                    else if (casting.Any)
                    {
                        var cast = book.Cast(casting);
                        Console.WriteLine($"\nCasting {cast.Name}\n" +
                                          $"Hardness: {cast.Hardness}\t Heat: {cast.Heat}\n" +
                                          $"Entropy: {cast.Entropy}\t" +
                                          $"Luminance: {cast.Luminance}\n" +
                                          $"Manifold: {cast.Manifold}\t" +
                                          $"Density: {cast.Density}\n" +
                                          $"Risk: {cast.Risk}\t" +
                                          $"Range: {cast.Range}\n" +
                                          $"Stability: {cast.Stability}");
                        casting = new Incantation();
                    }
                }

                positions.Clear();
            }
        }

        public void MouseClick(Vector2 pos)
        {
        }

        public void MouseRelease(Vector2 pos)
        {
        }

        public void MouseDrag(Vector2 pos)
        {
        }

        public void MouseScroll(bool up)
        {
        }

        private Spellbook book = Bookmaker.NewBook();
        private Incantation casting = new Incantation();
        private Gesture currentRune = new Gesture();
        private List<Vector3> positions = new List<Vector3>();
        public Func<Vector2> GetMousePos;
        public Func<bool> IsPressed;
        public Func<double> Depth;
    }
}
