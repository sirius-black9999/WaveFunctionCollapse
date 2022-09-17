using System;
using NUnit.Framework;
using WaveFunction.MagicSystemSketch;

namespace WaveFunctionTest.MagicSystem
{
    public class SignatureTest
    {
        [TestCase(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)]
        [TestCase(0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0)]
        [TestCase(1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0)]
        [TestCase(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0)]
        [TestCase(1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0)]
        [TestCase(1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0)]
        [TestCase(1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0)]
        [TestCase(0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0)]
        [TestCase(1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1)]
        [TestCase(0, 0, 1, -1, 0, 0, 0, 0, 0, 0, 0)]
        [TestCase(0, 0, 1, 0, -1, 0, 0, 0, 0, 0, 0)]
        [TestCase(0, 0, 1, 0, 0, -1, 0, 0, 0, 0, 0)]
        [TestCase(0, 0, 1, 0, 0, 0, -1, 0, 0, 0, 0)]
        [TestCase(0, 1, 0, 0, 0, 0, 0, -1, 0, 0, 0)]
        [TestCase(0, 0, 1, 0, 0, 0, 0, 0, -1, 0, 0)]
        [TestCase(1, 0, 0, 0, 0, 0, 0, 0, 0, -1, 0)]
        [TestCase(0, 1, 0, 0, 0, 0, 0, 0, 0, 0, -1)]
        //rgrrrrgr
        [TestCase(5, 2, 0, 1, 1, 1, 1, 1, 1, 1, 1)]
        //bbbbgbrg
        [TestCase(1, 2, 6, -1, -1, -1, -1, -1, -1, -1, -1)]

        //maxed red
        [TestCase(7, 1, 0, 1, 1, 1, 1, 1, 1, -1, 1)]
        //maxed green
        [TestCase(0, 4, 4, 1, -1, -1, -1, -1, -1, 1, -1)]
        //maxed blue
        [TestCase(0, 3, 5, -1, -1, -1, -1, -1, -1, 1, -1)]
        public void Signature_May_Convert_To_Color(float r, float g, float b, params float[] elements)
        {
            //Arrange
            var sig = new Signature(elements);
            //Act
            var col = sig.Color;
            //Assert
            Assert.That(col.X, Is.EqualTo(Math.Tanh(r / 7f)).Within(0.1f), "X");
            Assert.That(col.Y, Is.EqualTo(Math.Tanh(g / 4f)).Within(0.1f), "Y");
            Assert.That(col.Z, Is.EqualTo(Math.Tanh(b / 5f)).Within(0.1f), "Z");
        }
    }
}
