using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaoHien.Model
{
    public class ProductAttributeModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int AttributeId { get; set; }

        public string ProductAttribute { get; set; }
    }
}
