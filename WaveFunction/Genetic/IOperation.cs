using System.Diagnostics;

namespace WaveFunction.Genetic
{
    public interface IOperation<T>
    {
        int Cost();
        T Transform(T before);
        string Described();
    }

    public class SwapColumn : IOperation<int[,]>
    {
        public SwapColumn(int from, int to, int row)
        {
            _from = from;
            _to = to;
            _row = row;
        }

        public int Cost() => 35;

        public int[,] Transform(int[,] before)
        {
            var ret = before.Clone() as int[,];
            Debug.Assert(ret != null, nameof(ret) + " != null");
            (ret[_from, _row], ret[_to, _row]) =
                (ret[_to, _row], ret[_from, _row]);
            return ret;
        }

        public string Described() => $"Swap [{_row},{_from}] with [{_row},{_to}] (row)";

        private readonly int _from;
        private readonly int _to;
        private readonly int _row;
    }

    public class SwapRow : IOperation<int[,]>
    {
        public SwapRow(int from, int to, int col)
        {
            _from = from;
            _to = to;
            _col = col;
        }

        public int Cost() => 1;

        public int[,] Transform(int[,] before)
        {
            var ret = before.Clone() as int[,];
            Debug.Assert(ret != null, nameof(ret) + " != null");
            (ret[_col, _from], ret[_col, _to]) =
                (ret[_col, _to], ret[_col, _from]);
            return ret;
        }

        private readonly int _from;
        private readonly int _to;
        private readonly int _col;

        public string Described() => $"Swap [{_from},{_col}] with [{_to},{_col}] (Col)";
    }

    public class AlterVal : IOperation<int[,]>
    {
        public AlterVal(int row, int col, bool up)
        {
            _row = row;
            _col = col;
            _up = up;
        }

        public int Cost() => 20;

        public int[,] Transform(int[,] before)
        {
            var ret = before.Clone() as int[,];
            Debug.Assert(ret != null, nameof(ret) + " != null");
            ret[_col, _row] += _up ? 1 : -1;
            return ret;
        }

        public string Described() => (_up ? "Increment" : "Decrement") + $"[{_row},{_col}]";

        private readonly bool _up;
        private readonly int _row;
        private readonly int _col;
    }
}
