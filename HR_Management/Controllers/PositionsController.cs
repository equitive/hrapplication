using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using HR_Management.Models;
using HR_Management.Models.HRModels;
using HR_Management.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HR_Management.Controllers
{
	public class PositionsController : Controller
	{
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public PositionsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }

        public async Task<ActionResult> PositionsIndex()
		{

            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            var positions = _context.PositionInfo.Where(z => z.empId == emp.empId).Join(_context.Employee, c => c.managerID, d => d.empId, (c, d) =>
                new PosMgrJoined { status = c.status, startDate = c.startDate, endDate = c.endDate, salary = c.salary, jobTitle = c.jobTitle, mgrfname = d.fname, mgrlname = d.lname }).OrderByDescending(y => y.startDate);

            ViewData["Positions"] = new SelectList(positions);
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
        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
