using Newtonsoft.Json;
using OnlineShoppingStore.DAL;
using OnlineShoppingStore.Models;
using OnlineShoppingStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();


        public List<SelectListItem> GetCategory()
		{
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecords();
			foreach (var item in cat)
			{
                list.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.CategoryName });
			}
            return list;
        }

        /// <summary>
        /// 대쉬보드 메인
        /// </summary>
        /// <returns></returns>
        public ActionResult Dashboard()
        {
            return View();
        }

        /// <summary>
        /// 카테고리 목록
        /// </summary>
        /// <returns></returns>
        public ActionResult Categories()
		{
            List<Tbl_Category> allcategories = _unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList();
            return View(allcategories);
		}

        /// <summary>
        /// 카테고리 상세페이지
        /// </summary>
        /// <returns></returns>
        public ActionResult AddCategory()
		{
            return UpdateCategory(0);
		}

        public ActionResult UpdateCategory(int categoryId)
		{
            CategoryDetail cd;
			if (categoryId != null)
			{
                cd = JsonConvert.DeserializeObject<CategoryDetail>(JsonConvert.SerializeObject(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstorDefault(categoryId)));
			}
            else
			{
                cd = new CategoryDetail();
			}
            return View("UpdateCategory", cd);
		}


        public ActionResult CategoryEdit(int catId)
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstorDefault(catId));
        }

        [HttpPost]
        public ActionResult CategoryEdit(Tbl_Category tbl)
        {
            _unitOfWork.GetRepositoryInstance<Tbl_Category>().Update(tbl);
            return RedirectToAction("Categories");
        }

        /// <summary>
        /// 제품리스트
        /// </summary>
        /// <returns></returns>
        public ActionResult Product()
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Product>().GetProduct());
        }
		#region Product 수정

		public ActionResult ProductEdit(int productId)
		{
            ViewBag.CategoryList = GetCategory();
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Product>().GetFirstorDefault(productId));
		}

		[HttpPost]
		public ActionResult ProductEdit(Tbl_Product tbl, HttpPostedFileBase file)
		{
            //File
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImg/"), pic);
                file.SaveAs(path);
            }
            tbl.ProductImage = file != null ? pic : tbl.ProductImage;

            tbl.ModifiedDate = DateTime.Now;
			_unitOfWork.GetRepositoryInstance<Tbl_Product>().Update(tbl);
			return RedirectToAction("Product");
		}

		#endregion

		public ActionResult ProductAdd()
        {
            ViewBag.CategoryList = GetCategory();
            return View();
        }

        [HttpPost]
        public ActionResult ProductAdd(Tbl_Product tbl, HttpPostedFileBase file)
        {
            //File
            string pic = null;
			if (file != null)
			{
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImg/"), pic);
                file.SaveAs(path);
			}

            tbl.ProductImage = pic;
            tbl.CreatedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Tbl_Product>().Add(tbl);
            return RedirectToAction("Product");
        }
    }
}