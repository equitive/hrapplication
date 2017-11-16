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
    public class EmployeesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public EmployeesController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }
        public IActionResult AddEmployee()
        {
            ViewData["Message"] = "Page to add an employee";

            return View();
        }

        public IActionResult TerminateEmployee()
        {
            ViewData["Message"] = "Page to terminate employees that request to Quit.";

            return View();
        }

        public async Task<ActionResult>  EmployeeList()
        {
            ViewData["Message"] = "Page to list all employees.";
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            var AllEmployees = _context.Employee.OrderBy(c => c.empId).ToList();
            var EmployeeInfo1 = _context.Employee.Join(_context.PositionInfo, c=>c.empId, d=>d.empId, (c,d)=>new EmployeeListClass{ fname = c.fname, lname = c.lname, position = d.jobTitle, status = c.status, managerID = c.managerID, rank=c.rank, email=c.email});
            ViewData["AllEmployees"] = new SelectList(EmployeeInfo1);
            return View();
        }


        public IActionResult ViewNotice()
        {
            ViewData["Message"] = "Page to view an employee's two week notice.";

            return View();
        }

        public IActionResult ApproveTermination()
        {
            ViewData["Message"] = "Page to approve the termination of an employee.";

            return View();
        }

        public IActionResult ViewTerminatedEmployees()
        {
            ViewData["Message"] = "Page to view all terminated employees.";

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
