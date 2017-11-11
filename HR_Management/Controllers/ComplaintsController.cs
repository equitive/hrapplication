using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management.Controllers
{
	public class ComplaintsController : Controller
	{
		public IActionResult ViewComplaint()
		{
			ViewData["Message"] = "Page to view a complaints.";

			return View();
		}

		public IActionResult EditComplaint()
		{
			ViewData["Message"] = "Page to edit a complaints.";

			return View();
		}

		public IActionResult ComplaintsIndex()
		{
			ViewData["Message"] = "Page to view all the complaints.";

			return View();
		}

		public IActionResult ComplaintsIndexManager()
		{
			ViewData["Message"] = "Page to view all the complaints manager view.";

			return View();
		}

		public IActionResult OutstandingComplaints()
		{
			ViewData["Message"] = "Page to view all the team complaints.";

			return View();
		}

		public IActionResult EditComplaintStatus()
		{
			ViewData["Message"] = "Page to edit complaint status.";

			return View();
		}

		public IActionResult TeamMemberComplaints()
		{
			ViewData["Message"] = "Page to edit complaint status.";

			return View();
		}

		public IActionResult AddComplaint()
		{
			ViewData["Message"] = "Page to add a complaints.";

			return View();
		}

		public IActionResult Error()
		{
			return View();
		}
	}
}
