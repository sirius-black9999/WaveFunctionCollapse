
using System.Collections.Generic;
using WaveFunction;

namespace WaveFunctionTest
{
    public class P_RNG : RNG
    {
        public P_RNG(params double[] values)
        {
            _valueQueue = new Queue<double>(values);
        }
        public double next() => _valueQueue.Dequeue();
        private readonly Queue<double> _valueQueue;
    }
}
