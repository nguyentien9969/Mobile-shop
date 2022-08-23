using Base.DL.DbAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Controllers
{
    public class ProductController : BaseController
    {
        readonly IUnitOfWork _uow;
        public ProductController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Product
        public ActionResult Index(int id)
        {
            return View(_uow.ProductRepository.GetDetail(id));
        }

        public ActionResult Filter(int cateId)
        {
            ViewBag.Category = _uow.ProductCategoryRepository.GetById(cateId);
            return View(_uow.ProductRepository.Filter(cateId));
        }

        public ActionResult _RelatedProduct(int id)
        {
            return View(_uow.ProductRepository.GetRelated(id, 6));
        }
    }
}