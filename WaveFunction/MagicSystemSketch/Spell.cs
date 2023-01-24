namespace WaveFunction.MagicSystemSketch
{
    public class Spell
    {
        public Spell(string name, params Aspect[] aspects)
        {
            foreach (var element in Enum.GetValues<Element>())
            {
                _aspects.Add(element, 0);
            }

            Name = name;
            foreach (var aspect in aspects)
            {
                var ele = aspect.IsElement();
                double expected = 1;
                if (aspect == ele.Negative()) expected *= -1;

                _aspects[ele] += expected;
            }
        }

        public Spell(string name, Incantation template)
        {
            Name = name;
            foreach (var element in Enum.GetValues<Element>())
            {
                _aspects.Add(element, template.Element(element));
            }
        }

        public double CastChance(Incantation cast)
        {
            double strength = 0;
            foreach (var aspect in _aspects)
            {
                strength += 1 - Math.Abs(cast.Element(aspect.Key) - aspect.Value);
            }

            return Math.Max(0, strength / _aspects.Count);
        }

        public Spell CastWith(Incantation cast) =>
            new Spell(Name, cast)
            {
                Hardness = cast.Element(Element.Solidum) - _aspects[Element.Solidum],
                Heat = cast.Element(Element.Calor) - _aspects[Element.Calor],
                Entropy = cast.Element(Element.Entropia) - _aspects[Element.Entropia],
                Luminance = cast.Element(Element.Lumines) - _aspects[Element.Lumines],
                Manifold = cast.Element(Element.Natura) - _aspects[Element.Natura],
                Density = cast.Element(Element.Densitas) - _aspects[Element.Densitas],
                Risk = cast.Element(Element.Harmonius) - _aspects[Element.Harmonius],
                Range = cast.Element(Element.Motus) - _aspects[Element.Motus],
                Stability = CastChance(cast)
            };

        public string Name { get; }

        private readonly Dictionary<Element, double> _aspects = new Dictionary<Element, double>();

        public double Hardness { get; private set; }
        public double Heat { get; private set; }
        public double Entropy { get; private set; }
        public double Luminance { get; private set; }
        public double Manifold { get; private set; }
        public double Density { get; private set; }
        public double Risk { get; private set; }
        public double Range { get; private set; }
        public double Stability { get; private set; }
    }
}
