
using System.Numerics;

namespace WaveFunctionCollapse.Scenes
{
    public interface IScene<TContext>
    {
        void Render(TContext cr, Vector2 size);
        void Update(int frameCount,Vector2 mousePos);
        void MouseClick(Vector2 pos);
        void MouseRelease(Vector2 pos);
        void MouseDrag(Vector2 pos);
        void MouseScroll(bool up);
    }
}
