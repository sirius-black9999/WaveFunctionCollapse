using System.Data;

namespace WaveFunction.Shared.Monad
{
    public class MonadDemo
    {
        SocketBuilder Make(int i) => new SocketBuilder(i);

        public void MonadExample()
        {
            var monad = new Maybe<int, SocketBuilder>(Make)
                .Transform(static b => b.AsTcp())
                .Transform(static b => b.Bind())
                .Transform(static b => b.Connect())
                .Transform(static b => b.Result());

            var future = new Future<int, int>(monad);
            future.Start(4);
            for (int i = 0; i < 100; i++)
            {
                if (future.Ready)
                    future.Retrieve(); //guaranteed to succeed
                
                Thread.Sleep(10); //do other work
            }

            future.Stop(); // request took too long, cancel
        }
    }

    internal class SocketBuilder
    {
        public SocketBuilder(int i)
        {
            _stored = i;
        }
        private readonly int _stored;
        public int Result()
        {
            return _stored + 5;
        }

        public SocketBuilder AsTcp()
        {
            if (Random.Shared.NextDouble() < 0.1) 
                throw new DataException("socket subsystem in use");
            return this;
        }
        public SocketBuilder Bind()
        {
            Thread.Sleep((int)(Random.Shared.NextDouble()*100_000));
            return this;
        }
        public SocketBuilder Connect()
        {
            if (Random.Shared.NextDouble() < 0.9) 
                throw new TimeoutException("no response yet");
            return this;
        }
    }
}
