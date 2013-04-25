using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaoHien.Model
{
    public class OrderDetailEntity
    {
        public int AttributeId { get; set; }
        public int ProductId { get; set; }
        public int UnitId { get; set; }
        public int OrderId { get; set; }
        public double NumberUnit { get; set; }
    }
}
