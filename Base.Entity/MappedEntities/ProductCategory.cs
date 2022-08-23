namespace Base.Entity.MappedEntities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductCategory")]
    public partial class ProductCategory : MappedEntities
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(511)]
        public string Description { get; set; }

        public int Sort { get; set; }

        public bool IsHomePage { get; set; }
    }
}
