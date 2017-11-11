using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management.Controllers
{
    public class AccountDetailsController : Controller
    {
		public IActionResult AccountOverview()
		{
			ViewData["Message"] = "Employee application account overview page upon login.";

			return View();
		}

		public IActionResult AccountOverviewEditView()
		{
			ViewData["Message"] = "Your application account overview page when you want to edit.";

			return View();
		}

		public IActionResult EditAccountOverviewEmployee()
		{
			ViewData["Message"] = "Edit the account information.";

			return View();
		}

		public IActionResult ManagerAccountOverview()
		{
			ViewData["Message"] = "Manager application account overview page upon login.";

			return View();
		}

		public IActionResult Error()
		{
			return View();
		}
    }
}