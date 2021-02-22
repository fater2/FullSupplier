using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supplier.Models
{
    public class CategorySpecification : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isValued { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual List<Specification> Specifications { get; set; }


    }
}
