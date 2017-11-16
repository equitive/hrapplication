using System;
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using HR_Management.Models;
using HR_Management.Models.HRModels;
using HR_Management.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HR_Management.Controllers
{
	public class RankingsController : Controller
	{
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public RankingsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }
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

		public async Task<ActionResult> CompanyRankingIndex()
		{
			ViewData["Message"] = "Page to view all the rankings in the company.";
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            var RankedEmployees =_context.Employee.Join(_context.PositionInfo, c => c.empId, d => d.empId, (c, d) => new EmployeeListClass { fname = c.fname, lname = c.lname, position = d.jobTitle, status = c.status, managerID = c.managerID, rank = c.rank, email = c.email }).OrderBy(c=>c.rank);
            ViewData["FullRankedEmployees"] = new SelectList(RankedEmployees);
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

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

    }
}
