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
            ViewData["ShowAdd"] = emp.employeeType == 0 ? false : true;
            ViewData["Positions"] = new SelectList(positions);
            return View();
		}

		public IActionResult PositionsIndexManager()
		{
			ViewData["Message"] = "Page to view all positions manager view.";

			return View();
		}

        [HttpGet]
		public IActionResult AddPosition()
		{
			ViewData["Message"] = "Page to view add positions.";

			return View();
		}

        [HttpPost]
        public async Task<IActionResult> AddPosition(string title, string description, int managerID, int empId, double salary)
        {
            ViewData["Message"] = "Page to view add positions.";
            Employee emp = _context.Employee.Where(x => x.empId == empId).First();
            PositionInfo pos = _context.PositionInfo.Where(x => x.empId == emp.empId).Where(y => y.status == true).First();
            PositionInfo newpos = new PositionInfo { salary = salary, jobDescription = description, managerID = managerID, empId = emp.empId, jobTitle = title, startDate = DateTime.Now, endDate = null, status = true };
            pos.status = false;
            pos.endDate = DateTime.Now;
            _context.Entry(pos).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _context.PositionInfo.Add(newpos);
            await _context.SaveChangesAsync();
            emp.jobTitle = newpos.ID;
            emp.managerID = managerID;
            _context.Entry(emp).State = EntityState.Modified;
            return RedirectToAction(nameof(PositionsController.PositionsIndex), "Positions");
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

		public IActionResult TeamMemberPosition(int id)
		{
			ViewData["Message"] = "Page to edit complaint status.";
            Employee empA = _context.Employee.Where(z => z.empId == id).First();
            var positions = _context.PositionInfo.Where(z => z.empId == id).Join(_context.Employee, c => c.managerID, d => d.empId, (c, d) =>
                new PosMgrJoined { status = c.status, startDate = c.startDate, endDate = c.endDate, salary = c.salary, jobTitle = c.jobTitle, mgrfname = d.fname, mgrlname = d.lname }).OrderByDescending(y => y.startDate);
            ViewData["Positions"] = new SelectList(positions);
            ViewData["CurrentEmployee"] = empA;
            return View();

		}

		public async Task<IActionResult> TeamPositions()
		{
			ViewData["Message"] = "Page to view edit positions.";
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            var employees = _context.Employee.Where(x => x.department == emp.department).Join(_context.PositionInfo, c => c.empId, d => d.empId, (c, d) => new EmpPosJoined { posstartdate = d.startDate.ToString(), salary = d.salary, empID = c.empId, fname = c.fname, lname = c.lname, phoneNumber = c.phoneNumber, status = d.status, email = c.email, jobTitle = d.jobTitle, department = c.department, managerID = c.managerID }).Where(v => v.status == true).ToList();
            ViewData["ShowAdd"] = emp.employeeType == 0 ? false : true;
            ViewData["Employees"] = new SelectList(employees);
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
