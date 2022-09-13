using WaveFunction.Extensions;
using WaveFunction.WaveFunc;

namespace WaveFunction
{
    public class BagEntry<T>
    {
        public T value;
        public double Weight;
        public List<Action> OnPicked = new List<Action>();
    }

    public class QuantumBag<T>
    {
        public QuantumBag()
        {
            random = new BaseRng();
        }

        public QuantumBag(RNG rng)
        {
            random = rng;
        }

        public BagLabel Add(T entry) => Add(entry, 1);

        public BagLabel Add(T entry, double weight)
        {
            var label = new BagLabel();
            _entries.Add(label, new BagEntry<T>() { value = entry, Weight = weight });
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

        private RNG random;

        public T Get()
        {
            var val = random.next();
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

                    return _entries[wt.Key].value;
                }

                val -= wt.Value.Weight;
            }

            throw new InvalidOperationException("Bag is empty");
        }

        public void Entangle(BagLabel label, Action action)
        {
            _entries[label].OnPicked.Add(action);
        }
    }
}
