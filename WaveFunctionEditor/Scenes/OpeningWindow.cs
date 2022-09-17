using System.Numerics;
using Cairo;
using WaveFunction.Shared;

namespace WaveFunctionEditor.Scenes
{
    public class OpeningWindow : IScene<Context>
    {
        public void Render(Context cr, Vector2 size)
        {
            if (cr == null) throw new ArgumentNullException(nameof(cr));

            cr.LineWidth = 9;
            cr.SetSourceRGB(0.7, 0.2, 0.0);

            cr.Translate(size.X / 2, size.Y / 2);
            cr.Arc(0, 0, (size.X < size.Y ? size.X : size.Y) / 2 - 10, 0, 2 * Math.PI);
            cr.StrokePreserve();
            cr.Translate(-size.X / 2, -size.Y / 2);

            cr.SetSourceRGB(0.3, 0.4, 0.6);
            cr.Fill();
        }

        public void Update(int frameCount, Vector2 pos)
        {
            //Unused
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
    }
}
