using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management.Controllers
{
	public class SettingsController : Controller
	{
		public IActionResult SettingsOverview()
		{
			ViewData["Message"] = "Your application settings overview page.";

			return View();
		}

		public IActionResult QuitJob()
		{
			ViewData["Message"] = "Quit Job.";

			return View();
		}



		public IActionResult Error()
		{
			return View();
		}
	}
}