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
    public class Image : BaseEntity
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Image Name")]
        public string Name { get; set; }
        public string Path { get; set; }
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
    }
}
