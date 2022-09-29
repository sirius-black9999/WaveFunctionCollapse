using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using NUnit.Framework;
using WaveFunction.Shared.Monad;
using ThreadState = System.Threading.ThreadState;

namespace WaveFunctionTest.MonadTest
{
    public class FutureTest
    {
        [Test]
        public void A_Future_Holds_And_Executes_A_Maybe_On_A_Separate_Thread()
        {
            //Arrange
            var test = new Maybe<int, double>(static i => i / 2f);
            var test2 = test.Transform(static d => d.ToString(CultureInfo.InvariantCulture)) as Maybe<int, string>;
            Assert.That(test2, Is.Not.Null, nameof(test2) + " != null");
            if (test2 == null) return;

            var f = new Future<int, string>(test2).Start(5);
            //Act

            var result = f.Retrieve(10);
            //Assert
            Assert.That(result, Is.EqualTo("2.5").Within(0.1f), "future returned expected result");
            Assert.That(test2.HasValue(), Is.True, "Test2 has executed");
        }

        bool finish;

        [Test]
        public void A_Future_Will_Confirm_When_The_Held_Operation_Is_Finished()
        {
            //Arrange
            finish = false;
            var test = new Maybe<int, int>(i =>
            {
                while (!finish)
                {
                    Thread.Sleep(1);
                }

                return -1;
            });
            var f = new Future<int, int>(test);
            //Act
            f.Start(3);
            //Assert
            Assert.That(f.Ready, Is.False);
            Assert.Throws<InvalidOperationException>(() => f.Retrieve(100));
            finish = true;
            var result = f.Retrieve(10);

            Assert.That(f.Ready, Is.True);
            Assert.That(result, Is.EqualTo(-1));
        }

        [Test]
        public void A_Future_With_Long_Runtime_Can_Be_Cancelled()
        {
            //Arrange
            var test = new Maybe<int, int>(i =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                }

                return 0;
            });
            var f = new Future<int, int>(test);
            //Act
            f.Start(3);
            //Assert
            Thread.Sleep(10);
            Assert.That(f.Time, Is.GreaterThan(TimeSpan.FromMilliseconds(9)));

            Assert.That(f.State == ThreadState.WaitSleepJoin || f.State == ThreadState.Running);
            f.Stop();
            Assert.That(f.State, Is.EqualTo(ThreadState.Stopped));
            Assert.That(f.Error.Count, Is.EqualTo(1));
            Assert.That(f.Error[0], Is.InstanceOf<ThreadInterruptedException>());
            
        }

        [Test]
        public void Busy_Waiting_Operations_Can_Not_Be_Interrupted()
        {
            //Arrange
            var test = new Maybe<int, int>(static i =>
            {
                while (true)
                {
                    double sum = 0;
                    for (int j = 0; j < 100_000_000; j++)
                    {
                        sum += j * i;
                    }

                    Thread.Sleep(1);
                }

                return 0;
            });
            var f = new Future<int, int>(test);
            //Act
            f.Start(3);
            //Assert
            Assert.That(f.State == ThreadState.WaitSleepJoin || f.State == ThreadState.Running);
            f.Stop();
            Assert.That(f.State, Is.EqualTo(ThreadState.Stopped));
        }

        [Test]
        public void Future_Will_Expose_Maybe_Exception_Handling()
        {
            //Arrange
            var test = new Maybe<int, int>(static _ => throw new ArgumentException("i ate all the cookies"));
            var f = new Future<int, int>(test);
            //Act
            f.Start(3);
            //Assert
            Thread.Sleep(10);
            Assert.That(f.State, Is.EqualTo(ThreadState.Stopped));
            Assert.That(f.Ready, Is.False);
            Assert.That(f.Error[0].Message, Is.EqualTo("i ate all the cookies"));
            Assert.That(f.Error[0], Is.InstanceOf<ArgumentException>());
            Assert.Throws<MethodAccessException>(() => f.Retrieve(1000000)); //returns instantly
        }
    }
}
