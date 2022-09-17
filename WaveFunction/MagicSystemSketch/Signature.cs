using System.Diagnostics;
using System.Numerics;

namespace WaveFunction.MagicSystemSketch
{
    public sealed class Signature
    {
        public Signature(float so, float fe, float or, float lu, float va, float @in, float su, float sp)
        {
            ulong dat = 0;
            dat = Encode(dat, Element.Solidum, so);
            dat = Encode(dat, Element.Febris, fe);
            dat = Encode(dat, Element.Ordinem, or);
            dat = Encode(dat, Element.Lumines, lu);
            dat = Encode(dat, Element.Varias, va);
            dat = Encode(dat, Element.Inertiae, @in);
            dat = Encode(dat, Element.Subsidium, su);
            _data = Encode(dat, Element.Spatium, sp);
        }

        public Signature(float[] elem) : this(elem[0], elem[1], elem[2], elem[3], elem[4], elem[5], elem[6], elem[7])
        {
        }

        private Signature(ulong data)
        {
            _data = data;
        }

        public float this[Element aspect] => Decode(aspect);
        public float Solidum => Decode(Element.Solidum);
        public float Febris => Decode(Element.Febris);
        public float Ordinem => Decode(Element.Ordinem);
        public float Lumines => Decode(Element.Lumines);
        public float Varias => Decode(Element.Varias);
        public float Inertiae => Decode(Element.Inertiae);
        public float Subsidium => Decode(Element.Subsidium);
        public float Spatium => Decode(Element.Spatium);
        public Vector3 Color => DataToCol();

        private Vector3 DataToCol()
        {
            //we have 64 bits of data to work with
            //that can be divided into 2 groups of 21 and 1 group of 22
            //
            //we have 8 elements, for 2 groups of 2 and 1 group of 3
            //each element is represented by 8 bits, for 2 groups of 16 and 1 group of 24 bits
            //
            //we'll use:

            float Ignis = Math.Clamp(Febris, 0, 1), //fire
                Hydris = Math.Clamp(-Febris, 0, 1), // water
                Tellus = Math.Clamp(Solidum, 0, 1), // earth
                Aeolis = Math.Clamp(-Solidum, 0, 1), // air
                Empyrus = Math.Clamp(Ordinem, 0, 1), // chaos
                Vitrio = Math.Clamp(-Ordinem, 0, 1), // order
                Luminus = Math.Clamp(Lumines, 0, 1), // light
                Noctis = Math.Clamp(-Lumines, 0, 1), // dark
                Spatius = Math.Clamp(Varias, 0, 1), // Space
                Tempus = Math.Clamp(-Varias, 0, 1), // Time
                Gravitas = Math.Clamp(Inertiae, 0, 1), // Heavy
                Levitas = Math.Clamp(-Inertiae, 0, 1), // light
                Auxillus = Math.Clamp(Subsidium, 0, 1), // Helpful
                Malus = Math.Clamp(-Subsidium, 0, 1), // Harmful
                Iuxta = Math.Clamp(Spatium, 0, 1), // Nearby
                Disis = Math.Clamp(-Spatium, 0, 1); // Distant

            //rgrrrrgr
            //bbbbgbrg
            return new Vector3((Ignis + Empyrus + Luminus + Spatius + Gravitas + Malus + Iuxta) / 7f,
                (Tellus + Tempus + Auxillus + Disis) / 4f,
                (Hydris + Aeolis + Vitrio + Noctis + Levitas) / 5f);
        }

        public Signature MixedWith(Vector3 color, Element target)
        {
            var r = color.X;
            var g = color.Y;
            var b = color.Z;
            var strength = -r + b;
            strength += g * Math.Sign(strength);
            var temp = _data;
            return new Signature(Encode(temp, target, strength + this[target]));
        }

        private readonly ulong _data;

        private static ulong Encode(ulong storage, Element target, float input)
        {
            var index = (int)target;
            var compacted = Math.Tanh(input);
            var toInsert = (byte)((compacted + 1) * 127);
            storage &= ~((ulong)0xff << (index * 8));
            return storage | ((ulong)toInsert << (index * 8));
        }

        private float Decode(Element target)
        {
            var index = (int)target;
            var value = (byte)((_data & ((ulong)0xff << (index * 8))) >> (index * 8));
            return (value - 127) / 127f;
        }

        public override bool Equals(object? obj)
        {
            var other = obj as Signature;
            if (other == null) return false;

            return Equals(other);
        }

        private bool Equals(Signature other) =>
            Math.Abs(Solidum - other.Solidum) < 0.1 &&
            Math.Abs(Febris - other.Febris) < 0.1 &&
            Math.Abs(Ordinem - other.Ordinem) < 0.1 &&
            Math.Abs(Lumines - other.Lumines) < 0.1 &&
            Math.Abs(Varias - other.Varias) < 0.1 &&
            Math.Abs(Inertiae - other.Inertiae) < 0.1 &&
            Math.Abs(Subsidium - other.Subsidium) < 0.1 &&
            Math.Abs(Spatium - other.Spatium) < 0.1;

        public override int GetHashCode() => _data.GetHashCode();

        public override string ToString() =>
            $"[{(int)(100 * Solidum)}, {(int)(100 * Febris)}, {(int)(100 * Ordinem)}, {(int)(100 * Lumines)}, {(int)(100 * Varias)}, {(int)(100 * Inertiae)}, {(int)(100 * Subsidium)}, {(int)(100 * Spatium)}]";
    }
}
