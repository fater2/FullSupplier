using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Supplier.Models
{
    public class Product : BaseEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public DateTime ProductionDate { get; set; }
        public DateTime ExpireDate { get; set; }

        public int Amount { get; set; }

        public int Price { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public virtual List<CategoryProduct> Categories { get; set; }
        public virtual List<ProductSpecification> ProductSpecifications { get; set; }
        public virtual List<Specification> Specifications { get; set; }
      //image
       
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }


    }
}
