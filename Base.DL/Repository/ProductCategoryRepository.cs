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
    public class ProductCategoryRepository : BaseRepository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(IApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public List<MainCategoryViewModel> GetMain()
        {
            return _connection.Query<MainCategoryViewModel>("Proc_ProductCategory_Main", null, commandType: CommandType.StoredProcedure).AsList();
        }
    }
}
