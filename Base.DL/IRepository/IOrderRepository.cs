using Base.Entity.MappedEntities;
using Base.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DL.IRepository
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        List<OrderViewModel> GetViewModels();
        OrderViewModel GetDetail(string id);
    }
}
