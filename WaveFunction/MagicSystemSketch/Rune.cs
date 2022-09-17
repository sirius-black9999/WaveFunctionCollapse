namespace WaveFunction.MagicSystemSketch
{
    public class Rune
    {
        public Rune(Gesture gest)
        {
            _aspects = gest.Aspects;
        }


        public double PushForce(Element element)
        {
            if (_aspects.ContainsKey(element.Positive()))
                return _aspects[element.Positive()];

            if (_aspects.ContainsKey(element.Negative()))
                return -_aspects[element.Negative()];

            return 0;
        }

        private readonly Dictionary<Aspect, double> _aspects;
    }
}
