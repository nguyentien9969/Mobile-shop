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
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(IApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public List<OrderViewModel> GetViewModels()
        {
            return _connection.Query<OrderViewModel>("Proc_Order_GetViewModels", null, commandType: CommandType.StoredProcedure).AsList();
        }

        public OrderViewModel GetDetail(string id)
        {
            var orderViewModel = new OrderViewModel();

            var p = new DynamicParameters();
            p.Add("@id", id);

            using (var multi = _connection.QueryMultiple("Proc_Order_GetDetail", p, commandType: CommandType.StoredProcedure))
            {
                orderViewModel = multi.Read<OrderViewModel>().FirstOrDefault();
                orderViewModel.OrderItems = multi.Read<ProductViewModel>().AsList();
            }

            return orderViewModel;
        }
    }
}
