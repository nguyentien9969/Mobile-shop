using Base.DL.DbAccess;
using Base.Entity.ResponseModels;
using Base.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWarehouse.Controllers
{
    public class CustomerController : BasicAuthorizationController
    {
        readonly IUnitOfWork _uow;
        public CustomerController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: ProductCategory
        public ActionResult Index()
        {
            return View();
        }
        // Lấy full danh sách khách hàng
        public ActionResult GetAll()
        {
            var response = new MainResponse<List<CustomerViewModel>>();

            try
            {
                response.Data = _uow.CustomerRepository.GetViewModels();
            }
            catch (Exception ex)
            {
                HandleException(response, ex);
            }

            return Json(response);
        }
    }
}