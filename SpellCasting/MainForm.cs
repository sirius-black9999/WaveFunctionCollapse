using System.Numerics;
using Gdk;
using Gtk;
using WaveFunctionCollapse.Scenes;
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
            DeleteEvent += static delegate { Application.Quit(); };
            KeyPressEvent += OnKeyPressEvent;

            var dArea = new DrawingArea();
            dArea.Events |= EventMask.ScrollMask;
            dArea.Events |= EventMask.ButtonPressMask;
            dArea.Events |= EventMask.ButtonReleaseMask;
            dArea.Events |= EventMask.ButtonMotionMask;
            dArea.Drawn += OnDraw;
            dArea.ScrollEvent += OnScrollEventHandler;
            dArea.ButtonPressEvent += (o, args) => pressed = true;
            dArea.ButtonReleaseEvent += (o, args) => pressed = false;
            var castWin = new CastWindow
            {
                GetMousePos = mousePos,
                Depth = () => value,
                IsPressed = () => pressed
            };

            SetScene(castWin);

            Add(dArea);

            ShowAll();
        }

        private void OnScrollEventHandler(object o, ScrollEventArgs args)
        {
            var amount = 0.1;
            if (args.Event.Direction == ScrollDirection.Down)
                amount *= -1;
            value += amount;
            value = Math.Clamp(value, -1, 1);
        }

        private Vector2 mousePos()
        {
            int mx, my;
            Child.GetPointer(out mx, out my);
            return new Vector2(mx, my);
        }

        private bool pressed;
        private double value;

        public void SetScene(IScene<Cairo.Context> scene)
        {
            _scene = scene;
        }

        void OnKeyPressEvent(object? sender, KeyPressEventArgs args)
        {
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
            var cr = Gdk.CairoHelper.Create(area.Window);
            var size = new Vector2(Allocation.Width, Allocation.Height);

            _scene.Render(cr, size);
            ((IDisposable)cr.GetTarget()).Dispose();
            ((IDisposable)cr).Dispose();
            _scene.Update(frame++, mousePos());
            //Render another frame
            area.QueueDraw();
        }

        private IScene<Cairo.Context> _scene;
        private int frame = 0;
    }
}
