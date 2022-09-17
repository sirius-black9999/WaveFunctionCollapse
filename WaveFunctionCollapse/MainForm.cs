using System.Numerics;
using Gdk;
using Gtk;
using WaveFunctionCollapse.Scenes;
using Action = System.Action;
using Key = Gdk.Key;
using Window = Gtk.Window;

namespace WaveFunctionCollapse
{
    public class MainForm : Window
    {
        public MainForm() : base("Wave function collapse")
        {
            SetDefaultSize(512, 512);
            SetPosition(WindowPosition.Center);
            DeleteEvent += static delegate { Application.Quit(); };
            KeyPressEvent += OnKeyPressEvent;


            var dArea = new DrawingArea();
            dArea.Events |= EventMask.ScrollMask;
            dArea.Events |= EventMask.ButtonPressMask;
            dArea.Events |= EventMask.ButtonReleaseMask;
            dArea.Events |= EventMask.Button1MotionMask;
            dArea.Drawn += OnDraw;
            dArea.ScrollEvent += (o, args) => _scene?.MouseScroll(args.Event.Direction == ScrollDirection.Up);
            dArea.ButtonPressEvent += (o, args) => _scene?.MouseClick(mousePos());
            dArea.ButtonReleaseEvent += (o, args) => _scene?.MouseRelease(mousePos());
            dArea.DragMotion += (o, args) => _scene?.MouseDrag(mousePos());

            Add(dArea);

            ShowAll();
        }


        public void SetScene(IScene<Cairo.Context> scene)
        {
            _scene = scene;
        }

        void OnKeyPressEvent(object? sender, KeyPressEventArgs args)
        {
            var evtMap = new Dictionary<Key, Action>()
            {
                { Key.q, static () => Application.Quit() },
                { Key.Key_1, () => SetScene(new VoronoiScene()) },
            };
            if (evtMap.ContainsKey(args.Event.Key))
                evtMap[args.Event.Key]();
        }

        void OnDraw(object sender, EventArgs args)
        {
            var area = (DrawingArea)sender;
            var cr = Gdk.CairoHelper.Create(area.Window);
            var size = new Vector2(Allocation.Width, Allocation.Height);

            _scene.Render(cr, size);
            ((IDisposable)cr.GetTarget()).Dispose();
            ((IDisposable)cr).Dispose();
            _scene.Update(_frame++, mousePos());
            //Render another frame
            area.QueueDraw();
        }

        private Vector2 mousePos()
        {
            int mx, my;
            Child.GetPointer(out mx, out my);
            return new Vector2(mx, my);
        }

        private IScene<Cairo.Context> _scene = new VoronoiScene();
        private int _frame;
    }
}
