using System;
using System.Globalization;
using NUnit.Framework;
using WaveFunction.Shared.Monad;

namespace WaveFunctionTest.MonadTest
{
    public class RetryingTest
    {
        [Test]
        public void Specific_Exceptions_May_Be_Set_To_Retry()
        {
            //Arrange
            int count = 3;
            var test = new Maybe<int, int>(_ =>
            {
                if (count-- > 0)
                {
                    throw new TimeoutException();
                }

                return 5;
            });
            //Act
            test.Run(5);
            //Assert
            Assert.That(count, Is.EqualTo(2));
            Assert.That(test.HasValue(), Is.False);
            Assert.That(count, Is.EqualTo(1));
            Assert.That(test.HasValue(), Is.False);
            Assert.That(count, Is.EqualTo(0));
            Assert.That(test.HasValue(), Is.True);
            Assert.That(test.Retrieve(), Is.EqualTo(5));
            Assert.That(test.Error.Count, Is.EqualTo(3));
            Assert.That(test.Error[0], Is.InstanceOf<TimeoutException>());
            Assert.That(test.Error[1], Is.InstanceOf<TimeoutException>());
            Assert.That(test.Error[2], Is.InstanceOf<TimeoutException>());
        }

        [Test]
        public void Retrying_Only_Retries_Latest_Step()
        {
            //Arrange
            int count1 = 0;
            int count2 = 3;
            var test = new Maybe<int, int>(_ =>
            {
                count1++;
                return 5;
            });
            var test2 = test.Transform(d =>
            {
                if (count2-- > 0) throw new TimeoutException();

                return d.ToString(CultureInfo.InvariantCulture);
            }) as Maybe<int, string>;
            //Act
            Assert.That(test2, Is.Not.Null, nameof(test2) + " != null");
            // ReSharper disable once NullableWarningSuppressionIsUsed
            test2!.Run(5);
            //Assert
            Assert.That(count2, Is.EqualTo(2));
            Assert.That(test2.HasValue(), Is.False);
            Assert.That(count2, Is.EqualTo(1));
            Assert.That(test2.HasValue(), Is.False);
            Assert.That(count2, Is.EqualTo(0));
            Assert.That(test2.HasValue(), Is.True);
            Assert.That(count2, Is.EqualTo(-1));
            Assert.That(test2.HasValue(), Is.True);
            Assert.That(count2, Is.EqualTo(-1));
            Assert.That(test2.Retrieve(), Is.EqualTo("5"));
            Assert.That(count1, Is.EqualTo(1));
        }
    }
}
