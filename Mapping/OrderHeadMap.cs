using nHibernate4.Mapping.Base;
using nHibernate4.Model;
using NHibernate.Mapping.ByCode;

namespace nHibernate4.Mapping
{
    public class OrderHeadMap : MapBase<OrderHead>
    {
        public OrderHeadMap()
        {
            Property(x => x.OrderNumber);

            Set(x => x.OrderLines,
                m => {
                    m.Cascade(Cascade.All);
                    m.Cache(c => {
                        c.Include(CacheInclude.All);
                        c.Usage(CacheUsage.ReadWrite);
                    });
                }, z => { z.OneToMany(o => { }); });

            Cache(c =>
            {
                c.Include(CacheInclude.All);
                c.Usage(CacheUsage.ReadWrite);
            });
        }
    }
}