﻿// See https://aka.ms/new-console-template for more information

using Gtk;

namespace SpellCasting;

internal static class Program
{
    public static void Main(string[] args)
    {
        Application.Init();
        using var form = new MainForm();
        Application.Run();
    }
}
