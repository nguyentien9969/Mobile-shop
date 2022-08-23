using Base.Entity.MappedEntities;
using Base.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DL.IRepository
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        List<CustomerViewModel> GetViewModels();
    }
}
