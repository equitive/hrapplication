using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management.Controllers
{
    public class CompanyController : Controller
    {
        public IActionResult EmployeeList()
        {
			ViewData["Message"] = "All company employees page.";

			return View();
        }

        public IActionResult ComplaintsList()
        {
            ViewData["Message"] = "All company complaints.";

            return View();
        }

        public IActionResult TimeOffList()
        {
            ViewData["Message"] = "All company timeoff requests";

            return View();
        }

        public IActionResult ReviewsList()
        {
            ViewData["Message"] = "All company reviews list.";

            return View();
        }

		public IActionResult PositionsList()
		{
			ViewData["Message"] = "All company reviews list.";

			return View();
		}

        public IActionResult Error()
        {
            return View();
        }
    }
}
