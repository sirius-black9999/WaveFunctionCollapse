// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using TilemapBuilder;

// TilesetConverter.Run();
// TilesetSplitter.Run();
// TilesetMerger.Run();
Stopwatch sw = new Stopwatch();
sw.Start();
TileMapTester.Run();
sw.Stop();
Console.WriteLine(sw.Elapsed);
