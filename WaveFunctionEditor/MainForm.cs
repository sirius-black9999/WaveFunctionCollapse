using System.Numerics;
using Cairo;
using Gdk;
using Gtk;
using WaveFunctionCollapse.Scenes;
using WaveFunctionEditor.Scenes;
using Action = System.Action;
using Key = Gdk.Key;
using Window = Gtk.Window;

namespace WaveFunctionEditor
{
    public class MainForm : Window
    {
        private readonly Vector2 DrawingAreaSize = new Vector2(512, 512);
        public MainForm() : base("Wave function collapse")
        {
            Fullscreen();
            SetPosition(WindowPosition.Center);
            DeleteEvent += static delegate { Application.Quit(); };
            KeyPressEvent += OnKeyPressEvent;

            SetScene(new OpeningWindow());

            var grid = new Grid();
            Add(grid);
            
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
            dArea.SetSizeRequest((int)DrawingAreaSize.X, (int)DrawingAreaSize.Y);
            grid.Add(dArea);

            var loadFile = new Button("Load file");
            loadFile.Clicked += OnLoadFileOnClicked;
            var saveFile = new Button("Save file");
            saveFile.Clicked += OnSaveFileOnClicked;
            grid.Add(loadFile);
            grid.Add(saveFile);
            
            var scenePicker = new ComboBox(new[] {"SourceImage", "Render", "Passability"});
            scenePicker.Changed += ScenePickerOnChanged;
            grid.Add(scenePicker);
            
            var TilePicker = new ComboBox(new[] {"SourceImage", "Render", "Passability"});
            scenePicker.Changed += ScenePickerOnChanged;
            
            ShowAll();
        }

        private void ScenePickerOnChanged(object? sender, EventArgs e)
        {
            var combo = sender as ComboBox;
            if (combo == null) return;

            var scenes = new List<IScene<Context>>()
            {
                new OpeningWindow(),
                new OpeningWindow(),
                new OpeningWindow()
            };

            SetScene(scenes[combo.Active]);
        }

        private void OnSaveFileOnClicked(object o, EventArgs args)
        {
            var md = new FileChooserDialog("Saving file", this, FileChooserAction.Save);
            md.Run();
            md.Destroy();
        }

        private void OnLoadFileOnClicked(object o, EventArgs args)
        {
            var md = new FileChooserDialog("Loading file", this, FileChooserAction.Open);
            md.Run();
            md.Destroy();
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
                { Key.Key_1, () => SetScene(new OpeningWindow()) },
            };
            if (evtMap.ContainsKey(args.Event.Key))
                evtMap[args.Event.Key]();
        }

        void OnDraw(object sender, DrawnArgs args)
        {
            var area = (DrawingArea)sender;
            var cr = Gdk.CairoHelper.Create(area.Window);

            _scene.Render(cr, DrawingAreaSize);
            ((IDisposable)cr.GetTarget()).Dispose();
            ((IDisposable)cr).Dispose();
            _scene.Update(frame++, mousePos());
            //Render another frame
            area.QueueDraw();
        }

        private Vector2 mousePos()
        {
            int mx, my;
            Child.GetPointer(out mx, out my);
            return new Vector2(mx, my);
        }

        private IScene<Cairo.Context> _scene;
        private int frame = 0;
    }
}
