using System.Numerics;
using Cairo;
using WaveFunction.ARPG.Battle;
using WaveFunction.Shared;
using WaveFunction.WaveFunc;

namespace WaveFunctionCollapse.Scenes
{
    public class BattleMapScene : IScene<Context>
    {
        private readonly BattleMap _map = BattleMap.Make.Randomized(new BaseRng()).Result;

        public void Render(Context cr, Vector2 size)
        {
            if (cr == null) throw new ArgumentNullException(nameof(cr));

            _map.Size.Foreach(RenderTile, cr);
        }

        private void RenderTile(Vector2 pos, Context cr)
        {
            var col = _map.GetCol(pos);
            cr.SetSourceRGB(col.X, col.Y, col.Z);
            cr.Rectangle(pos.X * 2, pos.Y * 2, 2, 2);
            cr.Fill();
        }

        public void Update(int frameCount, Vector2 pos)
        {
            //unused
        }

        public void MouseClick(Vector2 pos)
        {
            //unused
        }

        public void MouseRelease(Vector2 pos)
        {
            //unused
        }

        public void MouseDrag(Vector2 pos)
        {
            //unused
        }

        public void MouseScroll(bool up)
        {
            //unused
        }
    }
}
