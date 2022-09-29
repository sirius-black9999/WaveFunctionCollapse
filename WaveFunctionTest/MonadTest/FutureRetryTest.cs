
using System;
using System.Globalization;
using System.Threading;
using NUnit.Framework;
using WaveFunction.Shared.Monad;

namespace WaveFunctionTest.MonadTest
{
    public class FutureRetryTest
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
                    Thread.Sleep(10);
                    throw new TimeoutException();
                }

                return 5;
            });
            //Act
            var f = new Future<int, int>(test);
            f.Start(3);
            //Assert
            Assert.That(f.Ready, Is.False);
            Assert.Throws<InvalidOperationException>(() => f.Retrieve(1));
            var result = f.Retrieve(1000);

            Assert.That(f.Ready, Is.True);
            Assert.That(result, Is.EqualTo(5));
            Assert.That(f.Error.Count, Is.EqualTo(3));
            Assert.That(f.Error[0], Is.InstanceOf<TimeoutException>());
            Assert.That(f.Error[1], Is.InstanceOf<TimeoutException>());
            Assert.That(f.Error[2], Is.InstanceOf<TimeoutException>());
        }
        
        [Test]
        public void Retrying_Only_Retries_Latest_Step()
        {
            for (int i = 0; i < 1000; i++)
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
                var f = new Future<int, string>(test2);
                f.Start(5);
                //Assert
                Assert.That(f.Ready, Is.False);
                var result = f.Retrieve(100);
                Assert.That(f.Ready, Is.True);
                Assert.That(result, Is.EqualTo("5"));
                Assert.That(count2, Is.EqualTo(-1));
                Assert.That(count1, Is.EqualTo(1));
            }
        }
    }
}
