using Base.DL.DbAccess;
using Base.DL.IRepository;
using Base.Entity.MappedEntities;
using Base.Entity.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWarehouse.Controllers
{
    public class ProductCategoryController : BasicAuthorizationController
    {
        readonly IUnitOfWork _uow;
        public ProductCategoryController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: ProductCategory
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll()
        {
            var response = new MainResponse<List<ProductCategory>>();

            try
            {
                response.Data = _uow.ProductCategoryRepository.GetAll().OrderByDescending(_ => _.Id).ToList();
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
        public ActionResult Create(ProductCategory productCategory)
        {
            var response = new MainResponse<int>();

            try
            {

                _uow.ProductCategoryRepository.Insert(productCategory);
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
            var category = _uow.ProductCategoryRepository.GetById(id);
            return View(category);
        }

        [HttpPost]
        public ActionResult Update(ProductCategory productCategory)
        {
            var response = new MainResponse<int>();

            try
            {

                _uow.ProductCategoryRepository.Update(productCategory);
                response.Data = _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                HandleException(response, ex);
            }

            return Json(response);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var response = new MainResponse<int>();

            try
            {

                _uow.ProductCategoryRepository.Delete(id);
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