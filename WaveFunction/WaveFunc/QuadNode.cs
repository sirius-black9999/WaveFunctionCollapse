namespace WaveFunction.WaveFunc
{
    public enum Corner
    {
        TopLeft,
        TopRight,
        BotLeft,
        BotRight
    }

    public class QuadNode
    {
        public QuadNode this[Corner c]
        {
            get => Nodes.ContainsKey(c) ? Nodes[c] : new QuadNode();
            set => Nodes[c] = value;
        }

        private Dictionary<Corner, QuadNode> Nodes { get; } = new Dictionary<Corner, QuadNode>();

        public static QuadNode MakeFull() => MakeFull(static () => new QuadNode());
        public static QuadNode MakeFull(Func<QuadNode> fillWith) => new QuadNode().Fill(fillWith);
        public QuadNode Fill() => Fill(static () => new QuadNode());

        public QuadNode Fill(Func<QuadNode> fillWith)
        {
            this[Corner.TopLeft] = fillWith();
            this[Corner.TopRight] = fillWith();
            this[Corner.BotLeft] = fillWith();
            this[Corner.BotRight] = fillWith();
            return this;
        }
    }
}
