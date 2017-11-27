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

namespace HR_Management.Controllers
{
    public class AccountDetailsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        public AccountDetailsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }
		public async Task<IActionResult> AccountOverview()
		{
			ViewData["Message"] = "Employee application account overview page upon login.";
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            Employee mgr = _context.Employee.Where(x => x.empId == emp.managerID).First();
            PositionInfo pst = _context.PositionInfo.Where(x => x.empId == emp.empId && x.status == true).First();
            ViewData["EmpType"] = emp.employeeType;
            ViewData["Employee"] = emp;
            ViewData["salary"] = pst.salary;
            ViewData["manager"] = mgr.fname + " " + mgr.lname;
            ViewData["jobTitle"] = pst.jobTitle;
            return View();
		}

		public IActionResult AccountOverviewEditView()
		{
			ViewData["Message"] = "Your application account overview page when you want to edit.";

			return View();
		}

        [HttpGet]
		public async Task<IActionResult> EditAccountOverviewEmployee()
		{
			ViewData["Message"] = "Edit the account information.";
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            ViewData["Employee"] = emp;
            return View();
		}

        [HttpPost]
        public async Task<IActionResult> EditAccountOverviewEmployee(string fname, string lname, string address, string email, string phoneNumber)
        {
            ViewData["Message"] = "Edit the account information.";
            ViewData["Message"] = "Employee application account overview page upon login.";
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            emp.fname = fname;
            emp.lname = lname;
            emp.address = address;
            emp.email = email;
            emp.phoneNumber = phoneNumber;
            _context.Entry(emp).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AccountDetailsController.AccountOverview), "AccountDetails");
        }

        public async Task<ActionResult> ManagerAccountOverview()
		{
			ViewData["Message"] = "Manager application account overview page upon login.";
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            Employee mgr = _context.Employee.Where(x => x.empId == emp.managerID).First();
            PositionInfo pst = _context.PositionInfo.Where(x => x.empId == emp.empId && x.status == true).First();
            var employees = _context.Employee.Join(_context.PositionInfo, c => c.empId, d => d.empId, (c, d) => new EmpPosJoined { fname = c.fname, lname = c.lname, phoneNumber = c.phoneNumber, email = c.email, jobTitle = d.jobTitle, department = c.department, empID = c.empId }).Where(v => v.department == emp.department);
            ViewData["EmpType"] = emp.employeeType;
            ViewData["Employee"] = emp;
            ViewData["salary"] = pst.salary;
            ViewData["manager"] = mgr.fname + " " + mgr.lname;
            ViewData["jobTitle"] = pst.jobTitle;
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