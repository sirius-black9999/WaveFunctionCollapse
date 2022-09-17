using WaveFunction.Shared;

namespace WaveFunctionTest.ARPG
{
    class SeqRng : IRng
    {
        public double Next() => _i++;
        private int _i;
    }
}