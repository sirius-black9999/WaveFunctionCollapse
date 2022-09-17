
using System;
using NUnit.Framework;
using WaveFunction;

namespace WaveFunctionTest
{
    public class QuantumBagTest
    {
        [Test]
        public void Quantum_Bag_Starts_Empty()
        {
            //Arrange
            var bag = new QuantumBag<int>();
            //Act
            var size = bag.Count;
            //Assert
            Assert.That(size, Is.EqualTo(0));
        }
        [Test]
        public void Adding_Value_Changes_Size()
        {
            //Arrange
            var bag = new QuantumBag<int>();
            //Act
            bag.Add(1);
            //Assert
            var size = bag.Count;
            Assert.That(size, Is.EqualTo(1));
        }
        [Test]
        public void Label_May_Be_Used_To_Retrieve_Weight()
        {
            //Arrange
            var bag = new QuantumBag<int>();
            var label = bag.Add(1);
            //Act
            var weight = bag.Weight(label);
            //Assert
            Assert.That(weight, Is.EqualTo(1));
        }
        [Test]
        public void Add_Can_Take_Custom_Weight()
        {
            //Arrange
            var bag = new QuantumBag<int>();
            var label = bag.Add(1, 6);
            //Act
            var weight = bag.Weight(label);
            //Assert
            Assert.That(weight, Is.EqualTo(6));
        }
        [TestCase(1, 2, 2)]
        //[TestCase(2, 2, 4)]
        public void Weights_May_Be_Resized(double initial, double mutation, double expected)
        {
            //Arrange
            var bag = new QuantumBag<int>();
            var label = bag.Add(1, initial);
            //Act
            bag.Resize(label, mutation);
            //Assert
            var size = bag.Weight(label);
            Assert.That(size, Is.EqualTo(expected));
        }
        
        [TestCase(1, 0.0)]
        [TestCase(1, 0.49)]
        [TestCase(2, 0.51)]
        [TestCase(2, 1.0)]
        public void Retrieving_Item_Occurs_Via_RNG(int expected, double value)
        {
            //Arrange
            IRng used = new PRng(value);
            var bag = new QuantumBag<int>(used);
            bag.Add(1);
            bag.Add(2);
            //Act
            var ret = bag.Get();
            //Assert
            Assert.That(ret, Is.EqualTo(expected));
        }
        
        [TestCase(1, 0.0, 2)]
        [TestCase(1, 0.32, 2)]
        [TestCase(2, 0.34, 2)]
        [TestCase(2, 1.0, 2)]
        
        [TestCase(1, 0.0, 3)]
        [TestCase(1, 0.24, 3)]
        [TestCase(2, 0.26, 3)]
        [TestCase(2, 1.0, 3)]
        public void Weight_Affects_Breakpoints(int expected, double value, double weight)
        {
            //Arrange
            IRng used = new PRng(value);
            var bag = new QuantumBag<int>(used);
            bag.Add(1);
            bag.Add(2, weight);
            //Act
            var ret = bag.Get();
            //Assert
            Assert.That(ret, Is.EqualTo(expected));
        }
        [Test]
        public void Retrieving_From_Empty_Bag_Throws()
        {
            //Arrange
            var bag = new QuantumBag<int>();
            //Act/Assert
            Assert.Throws<InvalidOperationException>(() => bag.Get(), "Bag is empty");
        }
        
        [Test]
        public void Once_Picked_Bag_Will_Fire_Event_Associated_With_Entry()
        {
            //Arrange
            IRng used = new PRng(0);
            var bag = new QuantumBag<int>(used);
            var label1 = bag.Add(1);
            var label2 = bag.Add(2);
            //Act
            bool fired1 = false;
            bool fired2 = false;
            bag.Entangle(label1, () => { fired1 = true; });
            bag.Entangle(label2, () => { fired2 = true; });
            //Assert
            bag.Get();
            Assert.That(fired1, Is.True);
            Assert.That(fired2, Is.False);
        }
    }
}
