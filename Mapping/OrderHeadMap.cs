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


            Property(x => x.OrderNumber10);
            Property(x => x.OrderNumber11);
            Property(x => x.OrderNumber12);
            Property(x => x.OrderNumber13);
            Property(x => x.OrderNumber14);
            Property(x => x.OrderNumber15);
            Property(x => x.OrderNumber16);
            Property(x => x.OrderNumber17);
            Property(x => x.OrderNumber18);
            Property(x => x.OrderNumber19);

            Property(x => x.OrderNumber20);
            Property(x => x.OrderNumber21);
            Property(x => x.OrderNumber22);
            Property(x => x.OrderNumber23);
            Property(x => x.OrderNumber24);
            Property(x => x.OrderNumber25);
            Property(x => x.OrderNumber26);
            Property(x => x.OrderNumber27);
            Property(x => x.OrderNumber28);
            Property(x => x.OrderNumber29);

            //Set(x => x.OrderLines,
            //    m => {
            //        m.Cascade(Cascade.All);
            //        m.Cache(c => {
            //            c.Include(CacheInclude.All);
            //            c.Usage(CacheUsage.ReadWrite);
            //        });
            //    }, z => { z.OneToMany(o => { }); });



            Cache(c =>
            {
                c.Include(CacheInclude.All);
                c.Usage(CacheUsage.ReadWrite);
                c.Region("reg1");
            });
        }
    }
}