using Base.DL.DbAccess;
using Base.Entity.ResponseModels;
using Base.Entity.Shared;
using Base.Entity.ViewModels;
using MyShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Controllers
{
    public class CartController : BaseController
    {
        readonly IUnitOfWork _uow;
        public CartController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Cart
        public ActionResult Index()
        {
            // Lấy thông tin danh sách sản phẩm trong giỏ hàng
            var model = (List<CartItemViewModel>)Session[Const.CART_SESSION];
            return View(model);
        }

        [HttpPost]
        public JsonResult Add(int productId, int quantity = 1)
        {
            // Thêm sản phẩm vào giỏ hàng
            // Add list sản phẩm đó vào session 
            var response = new MainResponse<int>();

            try
            {
                List<CartItemViewModel> ssCart = new List<CartItemViewModel>();
                var product = _uow.ProductRepository.GetById(productId);
                if (Session[Const.CART_SESSION] == null)
                {
                    var cartItem = new CartItemViewModel();
                    cartItem.ProductId = productId;
                    cartItem.Quantity = quantity;
                    cartItem.ProductName = product.Name;
                    cartItem.Price = product.Price;
                    cartItem.ProductMainImage = product.ImageUrl + product.MainImage;

                    ssCart.Add(cartItem);
                }
                else
                {
                    ssCart = (List<CartItemViewModel>)Session[Const.CART_SESSION];
                    if (ssCart.Exists(x => x.ProductId == productId))
                    {
                        foreach (var item in ssCart)
                        {
                            if (item.ProductId == productId)
                                item.Quantity += quantity;
                        }
                    }
                    else
                    {
                        var cartItem = new CartItemViewModel();
                        cartItem.ProductId = productId;
                        cartItem.Quantity = quantity;
                        cartItem.ProductName = product.Name;
                        cartItem.Price = product.Price;
                        cartItem.ProductMainImage = product.ImageUrl + product.MainImage;

                        ssCart.Add(cartItem);
                    }
                }

                Session[Const.CART_SESSION] = ssCart;
                response.Data = ssCart.Count;
            }
            catch (Exception ex)
            {
                HandleException(response, ex);
            }

            return Json(response);
        }
        // Đếm số lượng sản phẩm đang có trong giỏ hàng
        public ActionResult Count()
        {
            var response = new MainResponse<int>();

            try
            {
                if (Session[Const.CART_SESSION] != null)
                {
                    var ssCart = (List<CartItemViewModel>)Session[Const.CART_SESSION];
                    response.Data = ssCart.Count;
                }
            }
            catch (Exception ex)
            {
                HandleException(response, ex);
            }

            return Json(response);
        }

        // Cập nhật lại danh sách sản phẩm đang có trong giỏ hàng
        [HttpPost]
        public ActionResult UpdateQuantity(int productId, int quantity)
        {
            var response = new MainResponse<string>();

            try
            {
                if (Session[Const.CART_SESSION] == null)
                {
                    response.Code = CartItemState.CartNotFound;
                    return Json(response);
                }

                var product = _uow.ProductRepository.GetById(productId);
                if (quantity > product.Quantity)
                {
                    response.Code = CartItemState.ExceedQuantity;
                    return Json(response);
                }

                List<CartItemViewModel> ssCart = (List<CartItemViewModel>)Session[Const.CART_SESSION];
                var curProduct = ssCart.Where(x => x.ProductId == productId).FirstOrDefault();
                if(curProduct == null)
                {
                    response.Code = CartItemState.ProductNotFound;
                    return Json(response);
                }

                curProduct.Quantity = quantity;
                Session[Const.CART_SESSION] = ssCart;

                var html = RenderHelper.PartialView(this, "_CartBody", ssCart);
                response.Code = CartItemState.Success;
                response.Data = html;
            }
            catch (Exception ex)
            {
                HandleException(response, ex);
            }

            return Json(response);
        }

        [HttpPost]
        public JsonResult Delete(int productId)
        {
            var response = new MainResponse<string>();

            try
            {
                if (Session[Const.CART_SESSION] == null)
                {
                    response.Code = CartItemState.CartNotFound;
                    return Json(response);
                }

                List<CartItemViewModel> ssCart = (List<CartItemViewModel>)Session[Const.CART_SESSION];
                var curProduct = ssCart.Where(x => x.ProductId == productId).FirstOrDefault();
                if (curProduct == null)
                {
                    response.Code = CartItemState.ProductNotFound;
                    return Json(response);
                }

                ssCart.Remove(curProduct);
                Session[Const.CART_SESSION] = ssCart;

                var html = RenderHelper.PartialView(this, "_CartBody", ssCart);
                response.Code = CartItemState.Success;
                response.Data = html;
            }
            catch (Exception ex)
            {
                HandleException(response, ex);
            }

            return Json(response);
        }
    }
}