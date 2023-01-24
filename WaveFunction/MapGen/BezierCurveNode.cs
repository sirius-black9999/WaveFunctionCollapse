
using WaveFunction.MagicSystemSketch;

namespace WaveFunction.MapGen
{
    public class BezierCurveNode
    {
        public BezierCurveNode(Signature pointSlotFundament)
        {
            Point = pointSlotFundament;
        }

        public Signature Point { get; }
    }
}
