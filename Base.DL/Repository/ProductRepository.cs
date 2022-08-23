using Base.DL.DbAccess;
using Base.DL.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Entity.MappedEntities;
using Base.Entity.ViewModels;
using Dapper;
using System.Data;

namespace Base.DL.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public List<ProductViewModel> GetBestSeller(int top)
        {
            var p = new DynamicParameters();
            p.Add("@top", top);
            return _connection.Query<ProductViewModel>("Proc_Product_GetBestSeller", p, commandType: CommandType.StoredProcedure).AsList();
        }

        public List<ProductViewModel> GetNew(int top)
        {
            var p = new DynamicParameters();
            p.Add("@top", top);
            return _connection.Query<ProductViewModel>("Proc_Product_GetNew", p, commandType: CommandType.StoredProcedure).AsList();
        }

        public List<ProductViewModel> GetFeatured(int top)
        {
            var p = new DynamicParameters();
            p.Add("@top", top);
            return _connection.Query<ProductViewModel>("Proc_Product_GetFeature", p, commandType: CommandType.StoredProcedure).AsList();
        }

        public ProductViewModel GetDetail(int id)
        {
            var p = new DynamicParameters();
            p.Add("@id", id);
            return _connection.QueryFirst<ProductViewModel>("Proc_Product_GetDetail", p, commandType: CommandType.StoredProcedure);
        }
        
        public List<ProductViewModel> GetRelated(int id, int top)
        {
            var p = new DynamicParameters();
            p.Add("@id", id);
            p.Add("@top", top);
            return _connection.Query<ProductViewModel>("Proc_Product_GetRelated", p, commandType: CommandType.StoredProcedure).AsList();
        }

        public MainProductViewModel GetMain(int top)
        {
            var mainProductViewModel = new MainProductViewModel();

            var p = new DynamicParameters();
            p.Add("@top", top);

            using (var multi = _connection.QueryMultiple("Proc_Product_GetMain", p, commandType: CommandType.StoredProcedure))
            {
                mainProductViewModel.MainCategories = multi.Read<MainCategoryViewModel>().AsList();
                mainProductViewModel.AllNewProducts = multi.Read<ProductViewModel>().AsList();
                mainProductViewModel.AllFeaturedProducts = multi.Read<ProductViewModel>().AsList();
                mainProductViewModel.AllBestsellerProducts = multi.Read<ProductViewModel>().AsList();
            }

            return mainProductViewModel;
        }
        
        public List<ProductViewModel> GetViewModels()
        {
            return _connection.Query<ProductViewModel>("Proc_Product_GetViewModels", null, commandType: CommandType.StoredProcedure).AsList();
        }
        
        public List<ProductViewModel> Filter(int cateId)
        {
            var p = new DynamicParameters();
            p.Add("@cateId", cateId);
            return _connection.Query<ProductViewModel>("Proc_Product_Filter", p, commandType: CommandType.StoredProcedure).AsList();
        }
    }
}
