using Base.Entity.MappedEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Entity.ViewModels
{
    public class MainProductViewModel
    {
        public List<MainCategoryViewModel> MainCategories;

        public List<ProductViewModel> AllNewProducts { get; set; }
        public List<ProductViewModel> AllBestsellerProducts { get; set; }
        public List<ProductViewModel> AllFeaturedProducts { get; set; }
    }

    [NotMapped]
    public class MainCategoryViewModel : ProductCategory
    {
        public List<ProductViewModel> NewProducts { get; set; }
        public List<ProductViewModel> BestsellerProducts { get; set; }
        public List<ProductViewModel> FeaturedProducts { get; set; }
    }
}
