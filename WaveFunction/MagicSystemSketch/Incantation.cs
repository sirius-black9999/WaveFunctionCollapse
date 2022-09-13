
namespace WaveFunction.MagicSystemSketch
{
    public class Incantation
    {
        public Incantation Inscribe(Rune resolve)
        {
            runes.Add(resolve);
            return this;
        }
        public Incantation Inscribe(Rune[] resolve)
                 {
                     runes.AddRange(resolve);
                     return this;
                 }

        private List<Rune> runes = new List<Rune>();
        public bool Any => runes.Any();
        public double Element(Element ele)
        {
            return runes.Sum(rune => rune.PushForce(ele));
        }
    }
}
