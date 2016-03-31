using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nHibernate4.Benchmarks
{
    public abstract class Measurement
    {
        public abstract void Setup();

        public abstract void Start();

        public abstract void Stop();
    }
}
