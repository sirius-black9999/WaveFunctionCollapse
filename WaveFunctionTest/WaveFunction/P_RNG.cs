
using System.Collections.Generic;
using WaveFunction;

namespace WaveFunctionTest
{
    public class PRng : IRng
    {
        public PRng(params double[] values)
        {
            _valueQueue = new Queue<double>(values);
        }
        public double Next() => _valueQueue.Dequeue();
        private readonly Queue<double> _valueQueue;
    }
}
