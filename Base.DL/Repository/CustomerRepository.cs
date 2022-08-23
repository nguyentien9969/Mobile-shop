using Base.DL.DbAccess;
using Base.DL.IRepository;
using Base.Entity.MappedEntities;
using Base.Entity.ViewModels;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DL.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(IApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public List<CustomerViewModel> GetViewModels()
        {
            return _connection.Query<CustomerViewModel>("Proc_Customer_GetViewModels", null, commandType: CommandType.StoredProcedure).AsList();
        }
    }
}
