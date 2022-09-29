using WaveFunction.Shared;
using WaveFunction.WaveFunc.Extensions;

namespace WaveFunction.WaveFunc
{
    public class BagEntry<T>
    {
        public T Value { get; set; }
        public double Weight { get; set; }
        public List<Action> OnPicked { get; } = new List<Action>();
    }

    public class QuantumBag<T>
    {
        public QuantumBag()
        {
            _random = new BaseRng();
        }

        public QuantumBag(IRng rng)
        {
            _random = rng;
        }

        public BagLabel Add(T entry) => Add(entry, 1);

        public BagLabel Add(T entry, double weight)
        {
            var label = new BagLabel();
            _entries.Add(label, new BagEntry<T>() { Value = entry, Weight = weight });
            return label;
        }

        private readonly Dictionary<BagLabel, BagEntry<T>> _entries =
            new Dictionary<BagLabel, BagEntry<T>>();

        public int Count => _entries.Count;
        public double Entropy => _entries.Values.Entropy();

        public void Resize(BagLabel label, double i)
        {
            _entries[label].Weight = i;
        }

        public double Weight(BagLabel label) => _entries[label].Weight;

        private readonly IRng _random;


        public T Get()
        {
            var val = _random.Next();
            var weights = _entries.ToArray();

            var sum = _entries.Values.TotalWeight();
            val *= sum;
            foreach (var wt in weights)
            {
                if (val <= wt.Value.Weight)
                {
                    foreach (var evt in wt.Value.OnPicked)
                    {
                        evt.Invoke();
                    }

                    var ret = _entries[wt.Key].Value;
                    if (Erasing)
                        _entries.Remove(wt.Key);
                    return ret;
                }

                val -= wt.Value.Weight;
            }

            throw new InvalidOperationException("Bag is empty");
        }

        public bool Erasing { get; set; }

        public void Entangle(BagLabel label, Action action)
        {
            _entries[label].OnPicked.Add(action);
        }
    }
}
