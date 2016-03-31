using log4net;
using log4net.Config;

using nHibernate4.Benchmarks;
using System;
using System.Reflection;

namespace nHibernate4
{
    internal class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        private static void Main(string[] args)
        {
            XmlConfigurator.Configure(); //Log4Net


            var test = new RedisReadSecondLevelCache();

            test.Start();


            //var summary = BenchmarkRunner.Run<SecondLevelCache>();



        
            //var cfg = new Configuration().Configure();

            //cfg.Cache(c =>
            //{
            //    //c.Provider<SysCacheProvider>();
            //    //c.Provider<NHibernate.Caches.MemCache.MemCacheProvider>();
            //    c.Provider<RedisCacheProvider>();
            //    c.RegionsPrefix = "R_";
            //});

            //var mapper = new ModelMapper();
            //mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());
            //var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            //cfg.AddMapping(mapping);

            //var sf = cfg.BuildSessionFactory();

            //#region Mapping-By-Code (SET, One-To-Many, Many-To-One)

            //// Mapping per Code wir in den Klassen OrderHeadMap und 
            //// OrderLineMap vorgenommen.
            //using (var session = sf.OpenSession())
            //using (var tx = session.BeginTransaction())
            //{
            //    for (var i = 0; i < 100; i++)
            //    {
            //        var orderHead = new OrderHead
            //        {
            //            OrderNumber = "ORDER#" + i,
            //            OrderLines = new HashSet<OrderLine>()
            //        };

            //        var orderLine = new OrderLine
            //        {
            //            OrderHead = orderHead,
            //            Product = "PRODUCT#" + i,
            //            Quantity = i
            //        };

            //        orderHead.OrderLines.Add(orderLine);

            //        session.SaveOrUpdate(orderHead);
            //    }

            //    tx.Commit();
            //}

            //long searchId;

            //using (var session = sf.OpenSession())
            //using (var tx = session.BeginTransaction())
            //{
            //    searchId = session.QueryOver<OrderHead>()
            //                        .Select(oh => oh.Id)
            //                        .Take(1)
            //                        .SingleOrDefault<long>();

            //    tx.Commit();
            //}

            //sf.Evict(typeof(OrderHead));
            //sf.Evict(typeof(OrderLine));

            //using (var session = sf.OpenSession())
            //using (var tx = session.BeginTransaction())
            //{
            //    var head = session.Get<OrderHead>(searchId);

            //    head.OrderNumber = "dafgasdfklajsdflas";

            //    int i = head.OrderLines.Count;

            //    tx.Commit();
            //}

            //using (var session = sf.OpenSession())
            //using (var tx = session.BeginTransaction())
            //{
            //    var head = session.Get<OrderHead>(searchId);

            //    int i = head.OrderLines.Count;

            //    tx.Commit();
            //}

            //using (var session1 = sf.OpenSession())
            //using (var tx1 = session1.BeginTransaction())
            //{
            //    var head1 = session1.Get<OrderHead>(searchId);

            //    head1.OrderNumber = "Session1";


            //    using (var session2 = sf.OpenSession())
            //    using (var tx2 = session2.BeginTransaction())
            //    {
            //        var head2 = session2.Get<OrderHead>(searchId);

            //        head2.OrderNumber = "Session2";
            //        tx2.Commit();
            //    }

            //    try
            //    {
            //        tx1.Commit();
            //    }
            //    catch (Exception ex)
            //    {
            //        log.Error(ex);

            //        //StaleObject
            //    }
            //}

            //#endregion
        }

   
    }
}