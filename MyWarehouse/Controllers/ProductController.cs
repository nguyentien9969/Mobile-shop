
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
    public class ProductController : BasicAuthorizationController
    {
        readonly IUnitOfWork _uow;
        public ProductController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: ProductCategory
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Get(int id)
        {
            var response = new MainResponse<Product>();

            try
            {
                response.Data = _uow.ProductRepository.GetById(id);
            }
            catch (Exception ex)
            {
                HandleException(response, ex);
            }

            return Json(response);
        }

        public ActionResult GetAll()
        {
            var response = new MainResponse<List<ProductViewModel>>();

            try
            {
                response.Data = _uow.ProductRepository.GetViewModels();
            }
            catch (Exception ex)
            {
                HandleException(response, ex);
            }

            return Json(response);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            var response = new MainResponse<int>();

            try
            {

                _uow.ProductRepository.Insert(product);
                response.Data = _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                HandleException(response, ex);
            }

            return Json(response);
        }

        public ActionResult Update(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public ActionResult Update(Product product)
        {
            var response = new MainResponse<int>();

            try
            {

                _uow.ProductRepository.Update(product);
                response.Data = _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                HandleException(response, ex);
            }

            return Json(response);
        }

        //[HttpPost]
        //public ActionResult Delete(int id)
        //{
        //    var response = new MainResponse<int>();

        //    try
        //    {

        //        _uow.ProductCategoryRepository.Delete(id);
        //        response.Data = _uow.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        HandleException(response, ex);
        //    }

        //    return Json(response);
        //}
    }
}