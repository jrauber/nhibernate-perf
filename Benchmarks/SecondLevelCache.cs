using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nHibernate4.Benchmarks
{
    public class SecondLevelCache
    {
        [Benchmark]
        public void Create1MillionObjects()
        {
           
        }

        [Benchmark]
        public void Create1MillionObjectsWithRedis()
        {
           
        }

        [Benchmark]
        public void Create1MillionObjectsWithSysCache2()
        {

        }

        [Benchmark]
        public void Create1MillionObjectsWithMemCache()
        {

        }
    }
}
