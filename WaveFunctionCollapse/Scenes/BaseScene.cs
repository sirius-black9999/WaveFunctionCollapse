using System.Numerics;

namespace WaveFunctionCollapse.Scenes
{
    public class BaseScene : IScene<Cairo.Context>
    {
        public void Render(Cairo.Context cr, Vector2 size)
        {
            cr.LineWidth = 9;
            cr.SetSourceRGB(0.7, 0.2, 0.0);
            
            cr.Translate(size.X / 2, size.Y / 2);
            cr.Arc(0, 0, (size.X < size.Y ? size.X : size.Y) / 2 - 10, 0, 2 * Math.PI);
            cr.StrokePreserve();
            cr.Translate(-size.X / 2, -size.Y / 2);

            cr.SetSourceRGB(0.3, 0.4, 0.6);
            cr.Fill();
        }

        public void Update(int frameCount,Vector2 pos)
        {
            //unused
        }

        public void MouseClick(Vector2 pos)
        {
            Console.WriteLine($"Clicked at [{pos.X},{pos.Y}]");
        }

        public void MouseRelease(Vector2 pos)
        {
            Console.WriteLine($"Released at [{pos.X},{pos.Y}]");
        }

        public void MouseDrag(Vector2 pos)
        {
            Console.WriteLine($"Dragged to [{pos.X},{pos.Y}]");
        }

        public void MouseScroll(bool up)
        {
            Console.WriteLine($"Scrolled {(up ? "Up" : "Down")}");
        }
    }
}
