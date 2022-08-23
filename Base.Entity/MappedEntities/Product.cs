namespace Base.Entity.MappedEntities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Product")]
    public partial class Product : MappedEntities
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

        [Column(TypeName = "ntext")]
        public string ListImages { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string MainImage { get; set; }

        public bool IsActive { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        [StringLength(511)]
        public string ShortDescription { get; set; }

        public bool IsFeatured { get; set; }

        public int Quantity { get; set; }


        [NotMapped]
        public string ImageUrl { get { return "https://localhost:44399/"; } }
    }
}
