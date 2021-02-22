using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Supplier.Models
{
    public class Category : BaseEntity
    {
        public int Id { get; set;  }
        public string Description { get; set; }
        public string Name { get; set; }
        public virtual List<CategoryProduct> Products { get; set; }
        public virtual List<CategorySpecification> CategorySpecifications { get; set; }
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
        public virtual List<Image> images { get; set; }



    }
}
