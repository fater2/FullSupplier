using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supplier.Models
{
    public class Specification : BaseEntity
    {
        public int Id { get; set; }
        public string value { get; set; }

        public int ProductId { get; set; }
        public int CategorySpecificationId { get; set; }

        public virtual Product Product { get; set; }

        public virtual CategorySpecification CategorySpecification { get; set; }

    }
}
