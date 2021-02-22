using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supplier.Models
{
    public class CategoryProduct : BaseEntity
    {
        public int CategoryId { get; set; }
        virtual public Category Category { get; set; }
        public int ProductId { get; set; }
        virtual public Product Product { get; set; }
    }
}
