// See https://aka.ms/new-console-template for more information


using Gtk;

namespace WaveFunctionEditor;

internal static class Program
{
    public static void Main(string[] args)
    {
        Application.Init();
        new MainForm(); //NOSONAR

        Application.Run();
    }
}
