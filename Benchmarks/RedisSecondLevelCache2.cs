using NHibernate;
using nHibernate4.Model;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace nHibernate4.Benchmarks
{
    public class RedisSecondLevelCache : Measurement
    {
        ISessionFactory sf = SessionFactory.SessionFactoryWithNoChaching();

        Task task1;
        Task task2;
        Task task3;
        Task task4;

        Timer timerMeasure;
        Timer timerTotal;
        Timer timerCleanup;

        long ops = 0;
        long optotal = 0;
        long timeCleanup = 0;

        DateTime start;

        object l = new object();
        ReaderWriterLockSlim rwlock = new ReaderWriterLockSlim();

        public override void Setup()
        {

        }

        public override void Start()
        {
            var ts = new CancellationTokenSource();
            CancellationToken ct = ts.Token;

            timerMeasure = new Timer(state => Measure(), null, 5000, Timeout.Infinite);
            timerTotal = new Timer(state => Total(), null, 30000, Timeout.Infinite);
            timerCleanup = new Timer(state => CleanUp(), null, 10000, Timeout.Infinite);

            start = DateTime.Now;

            task1 = Task.Factory.StartNew(() => DoIt(ct), ct); // Pass same token to StartNew.   
            task2 = Task.Factory.StartNew(() => DoIt(ct), ct); // Pass same token to StartNew.   
            task3 = Task.Factory.StartNew(() => DoIt(ct), ct); // Pass same token to StartNew.   
            task4 = Task.Factory.StartNew(() => DoIt(ct), ct); // Pass same token to StartNew. 

            try
            {
                task1.Wait();
            }
            catch (AggregateException e)
            {
                foreach (var v in e.InnerExceptions)
                    Console.WriteLine(e.Message + " " + v.Message);
            }
            finally
            {
                ts.Dispose();
            }
        }

        private void DoIt(CancellationToken cto)
        {
            while (true)
            {
                cto.ThrowIfCancellationRequested();

                rwlock.EnterReadLock();

                using (var session = sf.OpenSession())
                using (var tx = session.BeginTransaction())
                {
                    for (int i2 = 0; i2 < 1000; i2++)
                    {
                        var head = new OrderHead();
                        head.OrderNumber = "#" + i2;

                        var line = new OrderLine();
                        line.Product = "Product #" + i2;
                        line.Quantity = 5;

                        head.OrderLines.Add(line);

                        session.Save(head);
                    }

                    tx.Commit();

                    Interlocked.Add(ref ops, 1000);
                    Interlocked.Add(ref optotal, 1000);

                    if (cto.IsCancellationRequested)
                    {
                        cto.ThrowIfCancellationRequested();
                    }
                }

                rwlock.ExitReadLock();
            }
        }

        private void Measure()
        {
            long tempOps = ops;

            Console.Out.WriteLine("OP/S : " + tempOps / 5);

            Interlocked.Add(ref ops, tempOps * -1);

            timerMeasure.Change(5000, 5000);
        }

        private void Total()
        {
            long tempOps = optotal;

            long durationMs = (long)(DateTime.Now - start).Milliseconds - timeCleanup;
            double seconds = (double)durationMs / (double)1000;

            Console.Out.WriteLine("OP/S Total : " + tempOps / seconds);

            Interlocked.Exchange(ref optotal, 0);

            timerTotal.Change(30000, 30000);
        }

        private void CleanUp()
        {
           
            try
            {
                Console.Out.WriteLine(rwlock.IsReadLockHeld);

                rwlock.TryEnterWriteLock(1000);

                DateTime start = DateTime.Now;

                using (var session = sf.OpenSession())
                using (var tx = session.BeginTransaction())
                {

                    foreach (var classMetadata in sf.GetAllClassMetadata())
                    {
                        sf.EvictEntity(classMetadata.Key);
                    }

                    session.CreateSQLQuery("Delete from OrderLine").ExecuteUpdate();
                    session.CreateSQLQuery("Delete from OrderHead").ExecuteUpdate();

                    tx.Commit();
                }

                DateTime end = DateTime.Now;

                Interlocked.Add(ref timeCleanup, (end - start).Milliseconds);

                Console.Out.WriteLine(end - start);

                timerCleanup.Change(10000, 10000);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex);          
            }
            finally
            {
                timerCleanup.Change(10000, 10000);

                if (rwlock.IsWriteLockHeld)
                {
                    rwlock.ExitWriteLock();
                }
            }
        }

        public override void Stop()
        {
           
        }
    }
}
