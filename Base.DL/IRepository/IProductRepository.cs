using Base.Entity.MappedEntities;
using Base.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DL.IRepository
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        List<ProductViewModel> GetBestSeller(int top);
        List<ProductViewModel> GetNew(int top);
        List<ProductViewModel> GetFeatured(int top);
        ProductViewModel GetDetail(int id);
        List<ProductViewModel> GetRelated(int id, int top);
        MainProductViewModel GetMain(int top);
        List<ProductViewModel> GetViewModels();
        List<ProductViewModel> Filter(int cateId);
    }
}
