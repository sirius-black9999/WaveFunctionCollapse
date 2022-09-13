using NUnit.Framework;
using WaveFunction;

namespace WaveFunctionTest
{
    public class QuantumBagEntropyTest
    {
        [TestCase(0)]
        [TestCase(0, 1)]
        [TestCase(1, 1, 1)]
        
        [TestCase(0.92, 1, 2)]
        [TestCase(0.92, 0.5, 1)]
        [TestCase(0.81, 1, 3)]
        
        [TestCase(2, 3, 3, 3,3)]
        [TestCase(1.2, 0.1, 1, 1)]
        [TestCase(1.1, 0.1, 3, 3)]
        [TestCase(0.4, 0.1, 0.1, 3)]
        [TestCase(0.2, 0.1, 3)]
        
        [TestCase(1, 0, 3, 3)]
        [TestCase(0, 0, 3)]
        public void Quantum_Bag_Has_Entropy(double expected, params double[] entryWeights)
        {
            //Arrange
            QuantumBag<int> bag = new QuantumBag<int>();
            for (int i = 0; i < entryWeights.Length; i++)
            {
                bag.Add(i, entryWeights[i]);
            }

            //Act
            var ent = bag.Entropy;
            //Assert
            Assert.That(ent, Is.EqualTo(expected).Within(0.1));
        }
    }
}
