namespace WaveFunction.MagicSystemSketch
{
    public class Incantation
    {
        public Incantation Inscribe(Rune resolve)
        {
            _runes.Add(resolve);
            return this;
        }

        public Incantation Inscribe(Rune[] resolve)
        {
            _runes.AddRange(resolve);
            return this;
        }

        public bool Any => _runes.Any();

        public double Element(Element ele)
        {
            return _runes.Sum(rune => rune.PushForce(ele));
        }
        private readonly List<Rune> _runes = new List<Rune>();
    }
}
