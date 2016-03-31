using System.Collections.Generic;
using nHibernate4.Model.Base;

namespace nHibernate4.Model
{
    public class OrderHead : ModelBase
    {
        public OrderHead()
        {
            OrderLines = new HashSet<OrderLine>();
        }

        public virtual string OrderNumber { get; set; }

        public virtual string OrderNumber10 { get; set; }
        public virtual string OrderNumber11 { get; set; }
        public virtual string OrderNumber12 { get; set; }
        public virtual string OrderNumber13 { get; set; }
        public virtual string OrderNumber14 { get; set; }
        public virtual string OrderNumber15 { get; set; }
        public virtual string OrderNumber16 { get; set; }
        public virtual string OrderNumber17 { get; set; }
        public virtual string OrderNumber18 { get; set; }
        public virtual string OrderNumber19 { get; set; }

        public virtual string OrderNumber20 { get; set; }
        public virtual string OrderNumber21 { get; set; }
        public virtual string OrderNumber22 { get; set; }
        public virtual string OrderNumber23 { get; set; }
        public virtual string OrderNumber24 { get; set; }
        public virtual string OrderNumber25 { get; set; }
        public virtual string OrderNumber26 { get; set; }
        public virtual string OrderNumber27 { get; set; }
        public virtual string OrderNumber28 { get; set; }
        public virtual string OrderNumber29 { get; set; }



        public virtual ISet<OrderLine> OrderLines { get; set; }
    }
}