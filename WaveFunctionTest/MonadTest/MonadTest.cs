using System;
using System.Diagnostics;
using System.Globalization;
using NUnit.Framework;
using WaveFunction.Shared.Monad;

namespace WaveFunctionTest.MonadTest
{
    public class MonadTest
    {
        [Test]
        public void A_Monad_May_Execute_A_Command()
        {
            //Arrange
            IMonad<int, double> test = new Monad<int, double>(static i => i / 2f);
            //Act
            test.Run(5);
            var result = test.Retrieve();
            //Assert
            Assert.That(result, Is.EqualTo(5 / 2f).Within(0.1f));
        }

        [Test]
        public void A_Monad_May_Execute_A_Series_Of_Commands()
        {
            //Arrange
            IMonad<int, double> test = new Monad<int, double>(static i => i / 2f);
            IMonad<int, string> test2 = test.Transform(static d => d.ToString(CultureInfo.InvariantCulture));
            //Act
            test2.Run(5);
            var result = test2.Retrieve();
            //Assert
            Assert.That(result, Is.EqualTo("2.5").Within(0.1f));
        }

        [Test]
        public void A_Maybe_Monad_May_Execute_A_Series_Of_Commands()
        {
            //Arrange
            var test = new Maybe<int, double>(static i => i / 2f);
            var test2 = test.Transform(static d => d.ToString(CultureInfo.InvariantCulture)) as Maybe<int, string>;
            Debug.Assert(test2 != null, nameof(test2) + " != null");
            //Act
            test2.Run(5);
            var result = test2.Retrieve();
            //Assert
            Assert.That(result, Is.EqualTo("2.5").Within(0.1f));
            Assert.That(test2.HasValue(), Is.True);
        }

        [Test] // An exception will leave the maybe empty
        public void An_Exception_Will_Leave_The_Maybe_Empty()
        {
            //Arrange
            var test = new Maybe<int, double>(static _ => throw new Exception());
            var test2 = test.Transform(static d => d.ToString(CultureInfo.InvariantCulture)) as Maybe<int, string>;
            //Act
            Assert.That(test2, Is.Not.Null, nameof(test2) + " != null");
            // ReSharper disable once NullableWarningSuppressionIsUsed
            test2!.Run(5);
            //Assert
            Assert.That(test2.HasValue(), Is.False);
            Assert.Throws<MethodAccessException>(() => test2.Retrieve());
        }

        [Test] // An exception will leave the maybe empty
        public void When_An_Exception_Is_Thrown_A_Log_Will_Be_Kept_Of_The_Type_And_Message()
        {
            //Arrange
            var test = new Maybe<int, double>(static _ => throw new ArgumentException("i ate all the cookies"));
            var test2 = test.Transform(static d => d.ToString(CultureInfo.InvariantCulture)) as Maybe<int, string>;
            //Act
            Assert.That(test2, Is.Not.Null, nameof(test2) + " != null");
            // ReSharper disable once NullableWarningSuppressionIsUsed
            test2!.Run(5);
            //Assert
            Assert.That(test2.Error[0].Message, Is.EqualTo("i ate all the cookies"));
            Assert.That(test2.Error[0], Is.InstanceOf<ArgumentException>());

            //Because test2 tries to retrieve from test,
            //this is currently all that causes the final maybe to show up as invalid
            //potentially fix before implementing
            Assert.That(test2.Error[1].Message,
                Is.EqualTo("attempting to retrieve from monad when no data is available"));
            Assert.That(test2.Error[1], Is.InstanceOf<MethodAccessException>());
        }
    }
}
