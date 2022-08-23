using Base.Entity.MappedEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Entity.ViewModels
{
    [NotMapped]
    public class ProductViewModel : Product
    {
        public string CategoryName { get; set; }

        public List<string> ParseListImages
        {
            get
            {
                if (String.IsNullOrEmpty(ListImages)) return null;

                return ListImages.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
        }
    }
}
