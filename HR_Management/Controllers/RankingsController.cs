using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management.Controllers
{
	public class RankingsController : Controller
	{

		public IActionResult EditRankings()
		{
			ViewData["Message"] = "Page to edit rankings.";

			return View();
		}

		public IActionResult DepartmentRankingIndex()
		{
			ViewData["Message"] = "Page to view all the rankings in a department.";

			return View();
		}

		public IActionResult CompanyRankingIndex()
		{
			ViewData["Message"] = "Page to view all the rankings in the company.";

			return View();
		}

		public IActionResult ManagerRankingIndex()
		{
			ViewData["Message"] = "Page to view all the rankings in the company.";

			return View();
		}

		public IActionResult EmployeeRankingIndex()
		{
			ViewData["Message"] = "Page to view all the rankings in the company.";

			return View();
		}

		public IActionResult DepartmentRankingIndexHR()
		{
			ViewData["Message"] = "Page to view all the rankings in the company.";

			return View();
		}

		public IActionResult Error()
		{
			return View();
		}
	}
}
