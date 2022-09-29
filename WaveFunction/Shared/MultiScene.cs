using System.Numerics;

namespace WaveFunction.Shared
{
    public class MultiScene<T> : IScene<T>
    {
        public MultiScene(params IScene<T>[] scenes)
        {
            _scenes = scenes;
        }
        
        public void Render(T cr, Vector2 size)
        {
            All((s) => s.Render(cr, size));
        }

        public void Update(int frameCount, Vector2 pos)
        {
            All((s) => s.Update(frameCount, pos));
        }

        public void MouseClick(Vector2 pos)
        {
            All((s) => s.MouseClick(pos));
        }

        public void MouseRelease(Vector2 pos)
        {
            All((s) => s.MouseRelease(pos));
        }

        public void MouseDrag(Vector2 pos)
        {
            All((s) => s.MouseDrag(pos));
        }

        public void MouseScroll(bool up)
        {
            All((s) => s.MouseScroll(up));
        }

        private void All(Action<IScene<T>> perform)
        {
            foreach (var scene in _scenes)
            {
                perform(scene);
            }
        }

        private readonly IScene<T>[] _scenes;
    }
}
