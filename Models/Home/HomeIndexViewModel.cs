using OnlineShoppingStore.DAL;
using OnlineShoppingStore.Repository;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OnlineShoppingStore.Models.Home
{
	public class HomeIndexViewModel
	{
		public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
		dbMyOnlineShoppingEntities context = new dbMyOnlineShoppingEntities();

		public IPagedList<Tbl_Product> ListOfPorducts { get; set; }
		public HomeIndexViewModel CreateModel(string search, int? page, int pageSize)
		{
			//검색 프로시저
			SqlParameter[] param = new SqlParameter[]
			{
				new SqlParameter("@search", search??(object)DBNull.Value)
			};

			//ToPagedList 페이지
			IPagedList<Tbl_Product> data = context.Database.SqlQuery<Tbl_Product>("GetBySearch @search", param).ToList().ToPagedList(page ?? 1, pageSize);

			return new HomeIndexViewModel
			{
				ListOfPorducts = data
			};
		}
	}
}