
using WaveFunction.Shared;

namespace WaveFunction.WaveFunc
{
    public class BaseRng : IRng
    {
        public double Next() => _r.NextDouble();
        private readonly Random _r = new Random();
    }
}
