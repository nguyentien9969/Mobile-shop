using Base.DL.DbAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Base.Entity.ViewModels;

namespace MyShop.Controllers
{
    public class HomeController : BaseController
    {
        readonly IUnitOfWork _uow;
        public HomeController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult _TopMenu()
        {
            // Lấy danh sách loại sản phẩm All bảng
            var category = _uow.ProductCategoryRepository.GetAll();
            return PartialView("_TopMenu", category); // Fill data lên màn  _TopMenu (patial view)
        }

        public ActionResult _Footer()
        {
            return View();
        }
        
        public ActionResult _BestSellerProduct()
        {
            var products = _uow.ProductRepository.GetBestSeller(10);
            return PartialView("_BestSellerProduct", products);
        }

        public ActionResult _NewProduct()
        {
            var products = _uow.ProductRepository.GetNew(10);
            return PartialView("_NewProduct", products);
        }

        public ActionResult _FeaturedProduct()
        {
            var products = _uow.ProductRepository.GetFeatured(10);
            return PartialView("_FeaturedProduct", products);
        }

        public ActionResult _MainProduct()
        {
            var mainProductViewModel = _uow.ProductRepository.GetMain(6);
            return PartialView("_MainProduct", mainProductViewModel);
        }
    }
}