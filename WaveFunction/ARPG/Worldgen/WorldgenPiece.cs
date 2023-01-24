using WaveFunction.ARPG.Items;
using WaveFunction.MapGen;

namespace WaveFunction.ARPG.Worldgen
{
    public class WorldgenPiece
    {
        public static WorldgenBuilder Make => new WorldgenBuilder();
        public IItemBase PointSlot { get; set; }

        public BezierCurve ResultingCurve()
        {
            return new BezierCurve(curveNodes());
        }

        private BezierCurveNode[] curveNodes()
        {
            var ret = new[]
            {
                new BezierCurveNode(PointSlot.Fundament)
            };
            return Next != null
                ? ret.Union(Next.curveNodes()).ToArray()
                : ret;
        }

        public WorldgenPiece? Next { get; set; }
    }

    public class WorldgenBuilder
    {
        private readonly List<IItemBase> _worldgenItems = new List<IItemBase>();

        public WorldgenBuilder Add(IItemBase toAdd)
        {
            _worldgenItems.Add(toAdd);
            return this;
        }

        public WorldgenPiece Finish()
        {
            var root = new WorldgenPiece()
            {
                PointSlot = _worldgenItems[0]
            };
            var current = root;
            for (int i = 1; i < _worldgenItems.Count; i++)
            {
                current.Next = new WorldgenPiece()
                {
                    PointSlot = _worldgenItems[i]
                };
                current = current.Next;
            }

            return root;
        }
    }
}
