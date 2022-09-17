namespace WaveFunction.WaveFunc.Extensions
{
    public static class Helpers
    {
        public static double Entropy<T>(this IEnumerable<BagEntry<T>> entries)
        {
            var bagEntries = entries as BagEntry<T>[] ?? entries.ToArray();
            if (!bagEntries.Any()) return 0;

            return bagEntries.Sum(entry =>
            {
                var scaled = entry.Weight / bagEntries.TotalWeight();
                if (scaled == 0)
                    return 0;

                return -scaled * Math.Log2(scaled);
            });
        }

        public static double TotalWeight<T>(this IEnumerable<BagEntry<T>> entries)
        {
            return entries.Sum(static kvp => kvp.Weight);
        }
    }
}
