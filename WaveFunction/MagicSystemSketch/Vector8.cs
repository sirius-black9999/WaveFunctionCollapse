using System.Numerics;

namespace WaveFunction.MagicSystemSketch
{
    public class Vector8
    {
        public Vector8(float x, float y, float z, float w, float v, float u,
            float t, float s)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
            V = v;
            U = u;
            T = t;
            S = s;
        }

        public float X { get; }
        public float Y { get; }
        public float Z { get; }
        public float W { get; }
        public float V { get; }
        public float U { get; }
        public float T { get; }
        public float S { get; }

        public float LengthSqr =>
            X * X +
            Y * Y +
            Z * Z +
            W * W +
            V * V +
            U * U +
            T * T +
            S * S;

        public float Length => (float)Math.Sqrt(LengthSqr);
        public Vector8 Normalized => this * (1 / Length);

        public static Vector8 operator *(Vector8 lhs, float rhs) =>
            new Vector8(
                lhs.X * rhs,
                lhs.Y * rhs,
                lhs.Z * rhs,
                lhs.W * rhs,
                lhs.V * rhs,
                lhs.U * rhs,
                lhs.T * rhs,
                lhs.S * rhs);

        public static Vector8 operator +(Vector8 lhs, Vector8 rhs) =>
            new Vector8(
                lhs.X + rhs.X,
                lhs.Y + rhs.Y,
                lhs.Z + rhs.Z,
                lhs.W + rhs.W,
                lhs.V + rhs.V,
                lhs.U + rhs.U,
                lhs.T + rhs.T,
                lhs.S + rhs.S);
    }
}
