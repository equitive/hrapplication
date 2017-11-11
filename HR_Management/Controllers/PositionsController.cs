using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management.Controllers
{
	public class PositionsController : Controller
	{

		public IActionResult PositionsIndex()
		{
			ViewData["Message"] = "Page to view all positions.";

			return View();
		}

		public IActionResult PositionsIndexManager()
		{
			ViewData["Message"] = "Page to view all positions manager view.";

			return View();
		}

		public IActionResult AddPosition()
		{
			ViewData["Message"] = "Page to view add positions.";

			return View();
		}

		public IActionResult EditPosition()
		{
			ViewData["Message"] = "Page to view edit positions.";

			return View();
		}

		public IActionResult TeamPositionRequests()
		{
			ViewData["Message"] = "Page to view edit positions.";

			return View();
		}

		public IActionResult TeamMemberPosition()
		{
			ViewData["Message"] = "Page to edit complaint status.";

			return View();
		}

		public IActionResult TeamPositions()
		{
			ViewData["Message"] = "Page to view edit positions.";

			return View();
		}


		public IActionResult Error()
		{
			return View();
		}
	}
}
