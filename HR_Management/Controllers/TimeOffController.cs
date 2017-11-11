using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management.Controllers
{
	public class TimeOffController : Controller
	{
		public IActionResult ViewTimeOff()
		{
			ViewData["Message"] = "Page to view a specific time off.";

			return View();
		}

        public IActionResult EditTimeOff()
		{
			ViewData["Message"] = "Page to edit a time off.";

			return View();
		}

        public IActionResult TimeOffIndex()
		{
			ViewData["Message"] = "Page to view all the time offs.";

			return View();
		}

		public IActionResult TimeOffIndexManager()
		{
			ViewData["Message"] = "Page to view all the time offs.";

			return View();
		}

		public IActionResult OutstandingTimeOff()
		{
			ViewData["Message"] = "Page to view all the time off requests.";

			return View();
		}

		public IActionResult ExceedingTimeOff()
		{
			ViewData["Message"] = "Page to view all the time off requests.";

			return View();
		}

		public IActionResult TeamMemberTimeOff()
		{
			ViewData["Message"] = "Page to edit complaint status.";

			return View();
		}

		public IActionResult AddTimeOff()
		{
			ViewData["Message"] = "Page to add a time off.";

			return View();
		}

		public IActionResult Error()
		{
			return View();
		}
	}
}
