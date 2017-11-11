using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management.Controllers
{
	public class TeamsController : Controller
	{
		public IActionResult ViewTeam()
		{
			ViewData["Message"] = "Page to view all team members.";

			return View();
		}

		public IActionResult EditTeamMember()
		{
			ViewData["Message"] = "Page to edit a team member.";

			return View();
		}

		public IActionResult ViewTeamMember()
		{
			ViewData["Message"] = "Page to view one team member.";

			return View();
		}

		public IActionResult ViewTeamManager()
		{
			ViewData["Message"] = "Page to view team manager/hr view.";

			return View();
		}


        public IActionResult AddTeamMember()
        {
            ViewData["Message"] = "Page to add a team members.";

            return View();
        }

		public IActionResult Error()
		{
			return View();
		}
	}
}
