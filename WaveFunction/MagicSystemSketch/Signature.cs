using System.Numerics;

namespace WaveFunction.MagicSystemSketch
{
    public sealed class Signature
    {
        public Signature(float so, float fe, float or, float lu,
            float va, float @in, float su, float sp)
        {
            ulong dat = 0;
            dat = dat.Encode(Element.Solidum, so);
            dat = dat.Encode(Element.Febris, fe);
            dat = dat.Encode(Element.Ordinem, or);
            dat = dat.Encode(Element.Lumines, lu);
            dat = dat.Encode(Element.Varias, va);
            dat = dat.Encode(Element.Inertiae, @in);
            dat = dat.Encode(Element.Subsidium, su);
            _data = dat.Encode(Element.Spatium, sp);
        }

        public Signature(float[] elem) :
            this(elem[0], elem[1], elem[2], elem[3],
                elem[4], elem[5], elem[6], elem[7])
        {
        }

        public Signature MixedWith(Vector4 color, Element target)
        {
            var r = color.X;
            var g = color.Y;
            var b = color.Z;
            var strength = -r + b;
            strength += g * Math.Sign(strength);
            var temp = _data;
            return new Signature(temp.Encode(target, strength + this[target]));
        }

        public float this[Element aspect] => _data.Decode(aspect);
        public float Solidum => _data.Decode(Element.Solidum);
        public float Febris => _data.Decode(Element.Febris);
        public float Ordinem => _data.Decode(Element.Ordinem);
        public float Lumines => _data.Decode(Element.Lumines);
        public float Varias => _data.Decode(Element.Varias);
        public float Inertiae => _data.Decode(Element.Inertiae);
        public float Subsidium => _data.Decode(Element.Subsidium);
        public float Spatium => _data.Decode(Element.Spatium);
        public Vector3 Color => this.DataToCol();

        private Signature(ulong data)
        {
            _data = data;
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
            $"[{(int)(100 * Solidum)}, {(int)(100 * Febris)}, {(int)(100 * Ordinem)}, {(int)(100 * Lumines)}," +
            $"{(int)(100 * Varias)}, {(int)(100 * Inertiae)}, {(int)(100 * Subsidium)}, {(int)(100 * Spatium)}]";


        private readonly ulong _data;
    }

//----------------------------------------------------------------------------------------------------------------------------------
    static class SignatureExtensions
    {
        public static Vector3 DataToCol(this Signature data)
        {
            //we have 64 bits of data to work with
            //that can be divided into 2 groups of 21 and 1 group of 22
            //
            //we have 8 elements, for 2 groups of 2 and 1 group of 3
            //each element is represented by 8 bits, for 2 groups of 16 and 1 group of 24 bits
            //
            //we'll use:

            float ignis = Math.Clamp(data.Febris, 0, 1), //fire
                hydris = Math.Clamp(-data.Febris, 0, 1), // water
                tellus = Math.Clamp(data.Solidum, 0, 1), // earth
                aeolis = Math.Clamp(-data.Solidum, 0, 1), // air
                empyrus = Math.Clamp(data.Ordinem, 0, 1), // chaos
                vitrio = Math.Clamp(-data.Ordinem, 0, 1), // order
                luminus = Math.Clamp(data.Lumines, 0, 1), // light
                noctis = Math.Clamp(-data.Lumines, 0, 1), // dark
                spatius = Math.Clamp(data.Varias, 0, 1), // Space
                tempus = Math.Clamp(-data.Varias, 0, 1), // Time
                gravitas = Math.Clamp(data.Inertiae, 0, 1), // Heavy
                levitas = Math.Clamp(-data.Inertiae, 0, 1), // light
                auxillus = Math.Clamp(data.Subsidium, 0, 1), // Helpful
                malus = Math.Clamp(-data.Subsidium, 0, 1), // Harmful
                iuxta = Math.Clamp(data.Spatium, 0, 1), // Nearby
                disis = Math.Clamp(-data.Spatium, 0, 1); // Distant

            //rgrrrrgr
            //bbbbgbrg
            return new Vector3((ignis + empyrus + luminus + spatius + gravitas + malus + iuxta) / 7f,
                (tellus + tempus + auxillus + disis) / 4f,
                (hydris + aeolis + vitrio + noctis + levitas) / 5f);
        }

        public static Signature MixedWith(this Signature lhs, Signature rhs)
        {
            var solidum = Math.Atanh(lhs.Solidum) + Math.Atanh(rhs.Solidum);
            var febris = Math.Atanh(lhs.Febris) + Math.Atanh(rhs.Febris);
            var ordinem = Math.Atanh(lhs.Ordinem) + Math.Atanh(rhs.Ordinem);
            var lumines = Math.Atanh(lhs.Lumines) + Math.Atanh(rhs.Lumines);

            var varias = Math.Atanh(lhs.Varias) + Math.Atanh(rhs.Varias);
            var inertiae = Math.Atanh(lhs.Inertiae) + Math.Atanh(rhs.Inertiae);
            var subsidium = Math.Atanh(lhs.Subsidium) + Math.Atanh(rhs.Subsidium);
            var spatium = Math.Atanh(lhs.Spatium) + Math.Atanh(rhs.Spatium);
            return new Signature((float)solidum, (float)febris, (float)ordinem, (float)lumines,
                (float)varias, (float)inertiae, (float)subsidium, (float)spatium);
        }

        public static ulong Encode(this ulong storage, Element target, float input)
        {
            var index = (int)target;
            var compacted = Math.Tanh(input);
            var toInsert = (byte)((compacted + 1) * 127);
            var temp = storage & ~((ulong)0xff << (index * 8));
            return temp | ((ulong)toInsert << (index * 8));
        }

        public static float Decode(this ulong storage, Element target)
        {
            var index = (int)target;
            var value = (byte)((storage & ((ulong)0xff << (index * 8))) >> (index * 8));
            return (value - 127) / 127f;
        }
    }
}
