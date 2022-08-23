using Base.DL.DbAccess;
using Base.DL.IRepository;
using Base.Entity.MappedEntities;
using Base.Entity.ResponseModels;
using Base.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWarehouse.Controllers
{
    public class OrderController : BasicAuthorizationController
    {
        readonly IUnitOfWork _uow;
        public OrderController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: ProductCategory
        public ActionResult Index()
        {
            return View();
        }

        // Lấy danh sách tất cả đơn hàng
        public ActionResult GetAll()
        {
            var response = new MainResponse<List<OrderViewModel>>();

            try
            {
                response.Data = _uow.OrderRepository.GetViewModels();
            }
            catch (Exception ex)
            {
                HandleException(response, ex);
            }

            return Json(response);
        }

        // Xem chi tiết đơn hàng
        public ActionResult Detail(string id)
        {
            ViewBag.Id = id;

            return View();
        }

        // get data chi tiết đơn hàng 
        public ActionResult GetDetail(string id)
        {
            var response = new MainResponse<OrderViewModel>();

            try
            {
                response.Data = _uow.OrderRepository.GetDetail(id);
            }
            catch (Exception ex)
            {
                HandleException(response, ex);
            }

            return Json(response);
        }

     

        [HttpPost]
        // Edit đơn hàng
        public ActionResult Update(Order order)
        {
            var response = new MainResponse<int>();

            try
            {
                var fullOrder = _uow.OrderRepository.GetById(order.Id);
                fullOrder.State = order.State;
                _uow.OrderRepository.Update(fullOrder);
                response.Data = _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                HandleException(response, ex);
            }

            return Json(response);
        }
    }
}