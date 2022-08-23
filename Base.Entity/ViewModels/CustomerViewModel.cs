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
    public class CustomerViewModel : Customer
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
