using System.Numerics;
using Cairo;
using Gdk;
using Gtk;
using WaveFunction.Shared;
using WaveFunctionEditor.Scenes;
using Action = System.Action;
using Key = Gdk.Key;
using Window = Gtk.Window;

namespace WaveFunctionEditor
{
    public class MainForm : Window
    {
        private readonly Vector2 _drawingAreaSize = new Vector2(512, 512);
public MainForm() : base("Wave function collapse")
        {
            Fullscreen();
            SetPosition(WindowPosition.Center);
            DeleteEvent += static (_, _) => Application.Quit();
            KeyPressEvent += OnKeyPressEvent;
            _scene = new OpeningWindow();
            var grid = new Grid();
            Add(grid);
            var dArea = new DrawingArea();
            dArea.Events |= EventMask.ScrollMask;
            dArea.Events |= EventMask.ButtonPressMask;
            dArea.Events |= EventMask.ButtonReleaseMask;
            dArea.Events |= EventMask.Button1MotionMask;
            dArea.Drawn += OnDraw;
            dArea.ScrollEvent += (_, args) => _scene.MouseScroll(args.Event.Direction == ScrollDirection.Up); // Add mouse scroll event
            dArea.ButtonPressEvent += (_, _) => _scene.MouseClick(MousePos()); // Add mouse click event
            dArea.ButtonReleaseEvent += (_, _) => _scene.MouseRelease(MousePos()); // Add mouse release event
            dArea.DragMotion += (_, _) => _scene.MouseDrag(MousePos()); // Add mouse drag event
            dArea.SetSizeRequest((int)_drawingAreaSize.X, (int)_drawingAreaSize.Y);
            grid.Add(dArea);
            var loadFile = new Button("Load file");
            loadFile.Clicked += OnLoadFileOnClicked;
            var saveFile = new Button("Save file");
            saveFile.Clicked += OnSaveFileOnClicked;
            grid.Add(loadFile);
            grid.Add(saveFile);
            var scenePicker = new ComboBox(new[] { "SourceImage", "Render", "Passability" });
            scenePicker.Changed += ScenePickerOnChanged;
            grid.Add(scenePicker);
            ShowAll();
        }
        private void ScenePickerOnChanged(object? sender, EventArgs e)
        {
            if (sender == null) throw new ArgumentNullException(nameof(sender));
            if (e == null) throw new ArgumentNullException(nameof(e));

            if (sender is not ComboBox combo) return;

            var scenes = new List<IScene<Context>>
            {
                new OpeningWindow(),
                new OpeningWindow(),
                new OpeningWindow()
            };

            SetScene(scenes[combo.Active]);
        }

        private void OnSaveFileOnClicked(object? o, EventArgs args)
        {
            if (o == null) throw new ArgumentNullException(nameof(o));
            if (args == null) throw new ArgumentNullException(nameof(args));

            var md = new FileChooserDialog("Saving file", this, FileChooserAction.Save);
            md.Run();
            md.Destroy();
        }

        private void OnLoadFileOnClicked(object? o, EventArgs args)
        {
            if (o == null) throw new ArgumentNullException(nameof(o));
            if (args == null) throw new ArgumentNullException(nameof(args));

            var md = new FileChooserDialog("Loading file", this, FileChooserAction.Open);
            md.Run();
            md.Destroy();
        }


        public void SetScene(IScene<Context> scene)
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
                { Key.Key_1, () => SetScene(new OpeningWindow()) },
            };
            if (evtMap.ContainsKey(args.Event.Key))
                evtMap[args.Event.Key]();
        }

        void OnDraw(object sender, DrawnArgs args)
        {
            if (sender == null) throw new ArgumentNullException(nameof(sender));
            if (args == null) throw new ArgumentNullException(nameof(args));

            var area = (DrawingArea)sender;
#pragma warning disable CS0612
            var cr = Gdk.CairoHelper.Create(area.Window);
#pragma warning restore CS0612

            _scene.Render(cr, _drawingAreaSize);
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

        private IScene<Context> _scene;
        private int _frame;
    }
}
