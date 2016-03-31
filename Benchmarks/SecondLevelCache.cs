
using NHibernate.Caches.Redis;

using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using System.Reflection;
using NHibernate.Caches.EnyimMemcached;
using NHibernate.Caches.SysCache2;
using NHibernate;
using System.Diagnostics;
using System.IO;
using NHibernate.Dialect;
using System.Xml;
using StackExchange.Redis;
using System;

namespace nHibernate4.Benchmarks
{
    public class SessionFactory
    {
        const string CONNECTION_STRING = @"Server=(local)\SQLExpress;Database=nHibernate4;Trusted_Connection=true;";

        public void Setup()
        {
            SetupRedis();
            SetupMemCached();
            SetupSysCache2();

            SessionFactoryWithRedis();
            SessionFactoryWithMemCache();
            SessionFactoryWithSysCache2();
            SessionFactoryWithNoChaching();
        }

        private void SetupSysCache2()
        {
            
        }

        private void SetupMemCached()
        {
            Process process = new Process();

            process.StartInfo.FileName = "memcached.exe";
            process.StartInfo.WorkingDirectory = @"..\..\deps\memcached\";
            process.StartInfo.Arguments = "-l 127.0.0.1 -v";

            process.Start();
        }

        private void SetupRedis()
        {
            Process process = new Process();

            process.StartInfo.FileName = "redis-server.exe";
            process.StartInfo.WorkingDirectory = @"..\..\deps\redis\";
            process.StartInfo.Arguments = "--bind 127.0.0.1";

            process.Start();
        }

        #region SessionFactory

        public static ISessionFactory SessionFactoryWithNoChaching()
        {
            var cfg = new Configuration().DataBaseIntegration(db =>
            {
                db.ConnectionString = CONNECTION_STRING;
                db.Dialect<MsSql2012Dialect>();
            });

            cfg.Cache(c =>
            {
                c.Provider<NHibernate.Caches.RtMemoryCache.RtMemoryCacheProvider>();
            });

            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());
            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            cfg.AddMapping(mapping);

            var sf = cfg.BuildSessionFactory();

            return sf;
        }

        public static ISessionFactory SessionFactoryWithSysCache2()
        {
            var cfg = new Configuration().DataBaseIntegration(db =>
            {
                db.ConnectionString = CONNECTION_STRING;
                db.Dialect<MsSql2012Dialect>();
            });

            cfg.Cache(c =>
            {
                c.Provider<SysCacheProvider>();
            });

            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());
            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            cfg.AddMapping(mapping);

            var sf = cfg.BuildSessionFactory();

            return sf;
        }

        public static ISessionFactory SessionFactoryWithMemCache()
        {
            var cfg = new Configuration().DataBaseIntegration(db =>
            {
                db.ConnectionString = CONNECTION_STRING;
                db.Dialect<MsSql2012Dialect>();
            });

            cfg.Cache(c =>
            {
                c.Provider<NHibernate.Caches.EnyimMemcached.MemCacheProvider>();
            });

            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());
            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            cfg.AddMapping(mapping);

            var sf = cfg.BuildSessionFactory();

            return sf;
        }

        public static ISessionFactory SessionFactoryWithRedis()
        {
            var connectionMultiplexer = ConnectionMultiplexer.Connect("localhost:6379,allowAdmin=true");
            connectionMultiplexer.GetServer("localhost", 6379).FlushAllDatabases();

            RedisCacheProvider.SetConnectionMultiplexer(connectionMultiplexer);
            RedisCacheProvider.SetOptions(new RedisCacheProviderOptions()
            {
                Serializer = new NetDataContractCacheSerializer(),
                CacheConfigurations = new[]
                {
                    new RedisCacheConfiguration("NHibernate.Cache.StandardQueryCache")
                    {
                        Expiration = TimeSpan.FromSeconds(9)
                    }
                }
            });

            var cfg = new Configuration().DataBaseIntegration(db =>
            {
                db.ConnectionString = CONNECTION_STRING;
                db.Dialect<MsSql2012Dialect>();
            });

            cfg.Cache(c =>
            {
                c.Provider<RedisCacheProvider>();
            });

            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());
            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            cfg.AddMapping(mapping);

            var sf = cfg.BuildSessionFactory();

            return sf;
        }

        #endregion sf

        


    }
}
