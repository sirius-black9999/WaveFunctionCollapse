using System.Numerics;

namespace WaveFunction.MagicSystemSketch
{
    public sealed class Signature
    {
        public Signature(float so, float ca, float en, float lu,
            float na, float de, float ha, float mo)
        {
            ulong dat = 0;
            dat = dat.Encode(Element.Solidum, so);
            dat = dat.Encode(Element.Calor, ca);
            dat = dat.Encode(Element.Entropia, en);
            dat = dat.Encode(Element.Lumines, lu);
            dat = dat.Encode(Element.Natura, na);
            dat = dat.Encode(Element.Densitas, de);
            dat = dat.Encode(Element.Harmonius, ha);
            _data = dat.Encode(Element.Motus, mo);
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
            return SetElement(target, strength + this[target]);
        }
        public Signature SetElement(Element target,float value)
        {
            return new Signature(_data.Encode(target, value));
        }

        public float this[Element aspect] => _data.Decode(aspect);
        public float Solidum => _data.Decode(Element.Solidum);
        public float Calor => _data.Decode(Element.Calor);
        public float Entropia => _data.Decode(Element.Entropia);
        public float Lumines => _data.Decode(Element.Lumines);
        public float Natura => _data.Decode(Element.Natura);
        public float Densitas => _data.Decode(Element.Densitas);
        public float Harmonius => _data.Decode(Element.Harmonius);
        public float Motus => _data.Decode(Element.Motus);

        public Vector3 SignatureColor => this.DataToCol();

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
            Math.Abs(Calor - other.Calor) < 0.1 &&
            Math.Abs(Entropia - other.Entropia) < 0.1 &&
            Math.Abs(Lumines - other.Lumines) < 0.1 &&
            Math.Abs(Natura - other.Natura) < 0.1 &&
            Math.Abs(Densitas - other.Densitas) < 0.1 &&
            Math.Abs(Harmonius - other.Harmonius) < 0.1 &&
            Math.Abs(Motus - other.Motus) < 0.1;

        public override int GetHashCode() => _data.GetHashCode();

        public override string ToString() =>
            $"[{(int)(100 * Solidum)}, {(int)(100 * Calor)}, {(int)(100 * Entropia)}, {(int)(100 * Lumines)}," +
            $"{(int)(100 * Natura)}, {(int)(100 * Densitas)}, {(int)(100 * Harmonius)}, {(int)(100 * Motus)}]";


        private readonly ulong _data;
        
        public static implicit operator Vector8(Signature sig)
        {
            return new Vector8(sig.Solidum, sig.Calor, sig.Entropia, sig.Lumines, sig.Natura, sig.Densitas, sig.Harmonius, sig.Motus);
        }
        public static implicit operator Signature(Vector8 vec)
        {
            return new Signature(vec.X, vec.Y, vec.Z, vec.W, vec.V, vec.U, vec.T, vec.S);
        }
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

            float ignis = Math.Clamp(data.Calor, 0, 1), //fire
                hydris = Math.Clamp(-data.Calor, 0, 1), // water
                tellus = Math.Clamp(data.Solidum, 0, 1), // earth
                aeolis = Math.Clamp(-data.Solidum, 0, 1), // air
                empyrus = Math.Clamp(data.Entropia, 0, 1), // chaos
                vitrio = Math.Clamp(-data.Entropia, 0, 1), // order
                luminus = Math.Clamp(data.Lumines, 0, 1), // light
                noctis = Math.Clamp(-data.Lumines, 0, 1), // dark
                spatius = Math.Clamp(data.Natura, 0, 1), // Space
                tempus = Math.Clamp(-data.Natura, 0, 1), // Time
                gravitas = Math.Clamp(data.Densitas, 0, 1), // Heavy
                levitas = Math.Clamp(-data.Densitas, 0, 1), // light
                auxillus = Math.Clamp(data.Harmonius, 0, 1), // Helpful
                malus = Math.Clamp(-data.Harmonius, 0, 1), // Harmful
                iuxta = Math.Clamp(data.Motus, 0, 1), // Nearby
                disis = Math.Clamp(-data.Motus, 0, 1); // Distant

            //rgrrrrgr
            //bbbbgbrg
            return new Vector3(
                (ignis + empyrus + luminus + spatius + gravitas + malus +
                 iuxta) /
                7f,
                (tellus + tempus + auxillus + disis) / 4f,
                (hydris + aeolis + vitrio + noctis + levitas) / 5f);
        }

        public static Signature MixedWith(this Signature lhs, Signature rhs)
        {
            var solidum = Math.Atanh(lhs.Solidum) + Math.Atanh(rhs.Solidum);
            var febris = Math.Atanh(lhs.Calor) + Math.Atanh(rhs.Calor);
            var ordinem = Math.Atanh(lhs.Entropia) + Math.Atanh(rhs.Entropia);
            var lumines = Math.Atanh(lhs.Lumines) + Math.Atanh(rhs.Lumines);

            var varias = Math.Atanh(lhs.Natura) + Math.Atanh(rhs.Natura);
            var inertiae = Math.Atanh(lhs.Densitas) + Math.Atanh(rhs.Densitas);
            var subsidium =
                Math.Atanh(lhs.Harmonius) + Math.Atanh(rhs.Harmonius);
            var spatium = Math.Atanh(lhs.Motus) + Math.Atanh(rhs.Motus);
            return new Signature((float)solidum, (float)febris, (float)ordinem,
                (float)lumines,
                (float)varias, (float)inertiae, (float)subsidium,
                (float)spatium);
        }

        public static ulong Encode(this ulong storage, Element target,
            float input)
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
            var value =
                (byte)((storage & ((ulong)0xff << (index * 8))) >> (index * 8));
            return (float)Math.Atanh((value - 127) / 127f);
        }
    }
}
