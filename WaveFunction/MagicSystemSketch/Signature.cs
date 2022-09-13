using System.Diagnostics;

namespace WaveFunction.MagicSystemSketch
{
    public class Signature
    {
        public Signature(params float[] elements)
        {
            Debug.Assert(elements.Length == 8);
            Solidum = elements[0];
            Febris = elements[1];
            Ordinem = elements[2];
            Lumines = elements[3];
            Varias = elements[4];
            Inertiae = elements[5];
            Subsidium = elements[6];
            Spatium = elements[7];
        }

        private static readonly Dictionary<Element, Func<Signature, float>> map =
            new Dictionary<Element, Func<Signature, float>>()
            {
                { Element.Solidum, sig => sig.Solidum },
                { Element.Febris, sig => sig.Febris },
                { Element.Ordinem, sig => sig.Ordinem },
                { Element.Lumines, sig => sig.Lumines },
                { Element.Varias, sig => sig.Varias },
                { Element.Inertiae, sig => sig.Inertiae },
                { Element.Subsidium, sig => sig.Subsidium },
                { Element.Spatium, sig => sig.Spatium }
            };

        public float this[Element aspect] => map[aspect](this);

        public float Solidum { get; }
        public float Febris { get; }
        public float Ordinem { get; }
        public float Lumines { get; }
        public float Varias { get; }
        public float Inertiae { get; }
        public float Subsidium { get; }
        public float Spatium { get; }
    }
}
