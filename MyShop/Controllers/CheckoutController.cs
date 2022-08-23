using Base.DL.DbAccess;
using Base.Entity.MappedEntities;
using Base.Entity.ResponseModels;
using Base.Entity.Shared;
using Base.Entity.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Controllers
{
    [Authorize]
    public class CheckoutController : BaseController
    {
        readonly IUnitOfWork _uow;
        private ApplicationUserManager _userManager;

        public CheckoutController(IUnitOfWork uow, ApplicationUserManager userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        // GET: Checkout
        // Call màn đăng nhập/ đăng ký sau khi bấm đặt hàng
        public ActionResult Index()
        {
            if (Session[Const.CART_SESSION] == null)
                return RedirectToAction("Index", "Cart");

            var model = (List<CartItemViewModel>)Session[Const.CART_SESSION];
            return View(model);
        }

        [HttpPost]
        public ActionResult PlaceOrder(Order order)
        {
            var response = new MainResponse<string>();

            try
            {
                if (Session[Const.CART_SESSION] == null)
                {
                    response.Code = PlaceOrderState.CartNotFound;
                    return Json(response);
                }

                _uow.BeginTransaction();

                order.CustomerId = HttpContext.User.Identity.GetUserId();
                order.Id = Guid.NewGuid().ToString();
                order.CreatedDate = DateTime.Now;

                _uow.OrderRepository.Insert(order);

                var cartItems = (List<CartItemViewModel>)Session[Const.CART_SESSION];
                var orderItems = cartItems.Select(_ => new OrderItem()
                {
                    OrderId = order.Id,
                    Price = _.Price,
                    ProductId = _.ProductId,
                    Quantity = _.Quantity
                }).ToList();

                _uow.OrderItemRepository.InsertRange(orderItems);
                _uow.SaveChanges();
                _uow.Commit();

                response.Data = order.Id;
                response.Code = PlaceOrderState.Success;

                Session[Const.CART_SESSION] = null;
            }
            catch (Exception ex)
            {
                response.Code = PlaceOrderState.Failure;
                HandleException(response, ex);
            }

            return Json(response);
        }
    }
}