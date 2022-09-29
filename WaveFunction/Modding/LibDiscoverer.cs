using System.Reflection;

namespace WaveFunction.Modding
{
    public static class LibDiscoverer
    {
        public static IPlugin[] GetPluginsForFolder(string mods) =>
            Directory
                .GetFiles(Directory.GetCurrentDirectory()+mods)
                .Where(static f => f.EndsWith(".dll"))
                .Select(Assembly.LoadFile)
                .SelectMany(static assembly => assembly.GetTypes())
                .Where(static type => type.GetInterface(nameof(IPlugin)) != null)
                .Select(static type => Activator.CreateInstance(type) as IPlugin)
                .Where(static plugin => plugin != null)
                // ReSharper disable once NullableWarningSuppressionIsUsed
                .ToArray()!;
    }
}
