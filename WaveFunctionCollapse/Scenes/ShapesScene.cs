using System.Numerics;

namespace WaveFunctionCollapse.Scenes
{
    public class ShapesScene : IScene<Cairo.Context>
    {
        public void Render(Cairo.Context cc, Vector2 size)
        {
            cc.SetSourceRGB(0.2, 0.23, 0.9);
            cc.LineWidth = 1;

            cc.Rectangle(20 + _animPos, 20, 120, 80);
            cc.Rectangle(180 + _animPos, 20, 80, 80);
            cc.StrokePreserve();
            cc.SetSourceRGB(1, 1, 1);
            cc.Fill();

            cc.SetSourceRGB(0.2, 0.23, 0.9);
            cc.Arc(330, 60, 40, 0, 2 * Math.PI);
            cc.StrokePreserve();
            cc.SetSourceRGB(1, 1, 1);
            cc.Fill();

            cc.SetSourceRGB(0.2, 0.23, 0.9);
            cc.Arc(90, 160, 40, Math.PI / 4, Math.PI);
            cc.ClosePath();
            cc.StrokePreserve();
            cc.SetSourceRGB(1, 1, 1);
            cc.Fill();

            cc.SetSourceRGB(0.2, 0.23, 0.9);
            cc.Translate(220 - _animPos, 180);
            cc.Scale(1, 0.7);
            cc.Arc(0, 0, 50, 0, 2 * Math.PI);
            cc.StrokePreserve();
            cc.SetSourceRGB(1, 1, 1);
            cc.Fill();
        }

        public void Update(int frameCount, Vector2 pos)
        {
            _animPos = Math.Sin(frameCount / 100f) * 100;
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

        private double _animPos;
    }
}
