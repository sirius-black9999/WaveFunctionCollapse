namespace WaveFunction.MagicSystemSketch
{
    public class Gesture
    {
        public Gesture Record(Aspect asp) => Record(asp, 1);

        public Gesture Record(Aspect asp, double strength)
        {
            if (!Has(asp))
                Aspects.Add(asp, strength);
            return this;
        }

        public bool Has(Aspect a) => Aspects.ContainsKey(a);
        public bool Has(Element a) => Has(a.Positive()) || Has(a.Negative());

        public Rune Resolve() => new Rune(this);

        public Dictionary<Aspect, double> Aspects { get; } = new Dictionary<Aspect, double>();

        public bool Any => Aspects.Any();
    }
}
