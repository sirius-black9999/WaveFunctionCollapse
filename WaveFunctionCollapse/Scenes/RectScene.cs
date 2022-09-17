using System.Numerics;
using Cairo;
using WaveFunction.Shared;

namespace WaveFunctionCollapse.Scenes
{
    public class RectScene : IScene<Context>
    {
        public void Render(Context cr, Vector2 size)
        {
            if (cr == null) throw new ArgumentNullException(nameof(cr));

            cr.SetSourceRGB(0.2, 0.23, 0.9);
            cr.Rectangle(10, 15, 90, 60);
            cr.Fill();

            cr.SetSourceRGB(0.9, 0.1, 0.1);
            cr.Rectangle(130, 15, 90, 60);
            cr.Fill();

            cr.SetSourceRGB(0.4, 0.9, 0.4);
            cr.Rectangle(250, 15, 90, 60);
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
