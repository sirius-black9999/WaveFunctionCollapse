using System.Numerics;
using Gdk;
using Gtk;
using WaveFunction.Shared;
using Key = Gdk.Key;
using Action = System.Action;
using Window = Gtk.Window;

namespace SpellCasting
{
    public class MainForm : Window
    {
        public MainForm() : base("Spell casting")
        {
            SetDefaultSize(800, 600);
            SetPosition(WindowPosition.Center);
            DeleteEvent += static (_, _) => Application.Quit();
            KeyPressEvent += OnKeyPressEvent;

            var dArea = new DrawingArea();
            dArea.Events |= EventMask.ScrollMask;
            dArea.Events |= EventMask.ButtonPressMask;
            dArea.Events |= EventMask.ButtonReleaseMask;
            dArea.Events |= EventMask.ButtonMotionMask;
            dArea.Drawn += OnDraw;
            dArea.ScrollEvent += OnScrollEventHandler;
            dArea.ButtonPressEvent += (_, _) => _pressed = true;
            dArea.ButtonReleaseEvent += (_, _) => _pressed = false;
            var castWin = new CastWindow
            {
                GetMousePos = MousePos,
                Depth = () => _value,
                IsPressed = () => _pressed
            };

            _scene = castWin;

            Add(dArea);

            ShowAll();
        }

        private void OnScrollEventHandler(object o, ScrollEventArgs args)
        {
            var amount = 0.1;
            if (args.Event.Direction == ScrollDirection.Down)
                amount *= -1;
            _value += amount;
            _value = Math.Clamp(_value, -1, 1);
        }

        private Vector2 MousePos()
        {
            int mx, my;
#pragma warning disable CS0612
            Child.GetPointer(out mx, out my);
#pragma warning restore CS0612
            return new Vector2(mx, my);
        }


        public void SetScene(IScene<Cairo.Context> scene)
        {
            _scene = scene;
        }

        void OnKeyPressEvent(object sender, KeyPressEventArgs args)
        {
            if (sender == null) throw new ArgumentNullException(nameof(sender));
            if (args == null) throw new ArgumentNullException(nameof(args));

            var evtMap = new Dictionary<Key, Action>()
            {
                { Key.q, static () => Application.Quit() },
            };
            if (evtMap.ContainsKey(args.Event.Key))
                evtMap[args.Event.Key]();
        }

        void OnDraw(object sender, EventArgs args)
        {
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

        private bool _pressed;
        private double _value;
        private IScene<Cairo.Context> _scene;
        private int _frame;
    }
}
