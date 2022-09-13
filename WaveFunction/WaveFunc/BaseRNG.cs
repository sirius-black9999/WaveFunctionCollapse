
namespace WaveFunction.WaveFunc
{
    public class BaseRng : RNG
    {
        public double next() => _r.NextDouble();
        private readonly Random _r = new Random();
    }
}
