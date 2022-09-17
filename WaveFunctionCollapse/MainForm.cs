using System.Numerics;
using Gdk;
using Gtk;
using WaveFunction.Shared;
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
            DeleteEvent += static (_, _) => Application.Quit();
            KeyPressEvent += OnKeyPressEvent;


            var dArea = new DrawingArea();
            dArea.Events |= EventMask.ScrollMask;
            dArea.Events |= EventMask.ButtonPressMask;
            dArea.Events |= EventMask.ButtonReleaseMask;
            dArea.Events |= EventMask.Button1MotionMask;
            dArea.Drawn += OnDraw;
            dArea.ScrollEvent += (_, args) => _scene.MouseScroll(args.Event.Direction == ScrollDirection.Up);
            dArea.ButtonPressEvent += (_, _) => _scene.MouseClick(MousePos());
            dArea.ButtonReleaseEvent += (_, _) => _scene.MouseRelease(MousePos());
            dArea.DragMotion += (_, _) => _scene.MouseDrag(MousePos());

            Add(dArea);

            ShowAll();
        }


        public void SetScene(IScene<Cairo.Context> scene)
        {
            _scene = scene ?? throw new ArgumentNullException(nameof(scene));
        }

        void OnKeyPressEvent(object sender, KeyPressEventArgs args)
        {
            if (sender == null) throw new ArgumentNullException(nameof(sender));
            if (args == null) throw new ArgumentNullException(nameof(args));

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
            if (sender == null) throw new ArgumentNullException(nameof(sender));
            if (args == null) throw new ArgumentNullException(nameof(args));

            var area = (DrawingArea)sender;
#pragma warning disable CS0612
            var cr = Gdk.CairoHelper.Create(area.Window);
#pragma warning restore CS0612
            var size = new Vector2(Allocation.Width, Allocation.Height);

            _scene.Render(cr, size);
            ((IDisposable)cr.GetTarget()).Dispose();
            ((IDisposable)cr).Dispose();
            _scene.Update(_frame++, MousePos());
            //Render another frame
            area.QueueDraw();
        }

        private Vector2 MousePos()
        {
            int mx, my;
#pragma warning disable CS0612
            Child.GetPointer(out mx, out my);
#pragma warning restore CS0612
            return new Vector2(mx, my);
        }

        private IScene<Cairo.Context> _scene = new VoronoiScene();
        private int _frame;
    }
}
