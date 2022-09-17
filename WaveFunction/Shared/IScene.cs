
using System.Numerics;

namespace WaveFunction.Shared
{
    public interface IScene<in TContext>
    {
        void Render(TContext cr, Vector2 size);
        void Update(int frameCount,Vector2 pos);
        void MouseClick(Vector2 pos);
        void MouseRelease(Vector2 pos);
        void MouseDrag(Vector2 pos);
        void MouseScroll(bool up);
    }
}
