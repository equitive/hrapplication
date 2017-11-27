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

        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(string fname, string lname, string address, string email, string jobTitle, string jobDescription, string password, string salary, string phoneNumber, string status, string department, int emptype)
        {
            var user = new ApplicationUser { UserName = email, Email = email };
            
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                var employeetype = department == "Human Resources" ? (emptype == 1 ? 4 : 2) : (emptype == 1 ? 1 : 0 );
                var mgrid = _context.Employee.Where(x => x.department == department && (x.employeeType == 1 || x.employeeType == 3 || x.employeeType == 2 || x.employeeType == 4)).First().empId;
                var newempid = _context.Employee.OrderByDescending(x => x.empId).First().empId + 1;
                var newrank = _context.Employee.OrderByDescending(x => x.rank).First().rank + 1;
                var emp = new Employee { empId = newempid, password = "password0", appuserid = user.Id, fname = fname, lname = lname, address = address, email = email, phoneNumber = phoneNumber, status = status, department = department, jobTitle = 0, vacationDays = 13, sickTime = 4, startDate = DateTime.Now.ToString(), endDate = "", managerID = mgrid, employeeType = employeetype, rank = newrank, ImageData = null, isTerminated = false, terminationReason = "", twoWeeksNotice = "" };
                _context.Employee.Add(emp);
                _context.SaveChanges();
                var pos = new PositionInfo { status = true, managerID = mgrid, empId = newempid, jobTitle = jobTitle, jobDescription = jobDescription, salary = Convert.ToDouble(salary), startDate = DateTime.Now, endDate = null };
                _context.PositionInfo.Add(pos);
                _context.SaveChanges();
                return RedirectToAction(nameof(EmployeesController.EmployeeList), "Employees");
            }
            return View();
        }

        [HttpGet]
        public IActionResult ApproveTermination(int id)
        {
            ViewData["Message"] = "Page to terminate employees that request to Quit.";
            ViewData["ID"] = id; 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ApproveTermination(int empId, string reason)
        {
            ViewData["Message"] = "Page to terminate employees that request to Quit.";
            var emp = _context.Employee.Where(x => x.empId == empId).First();
            emp.endDate = DateTime.Now.ToString();
            emp.terminationReason = reason;
            emp.status = "Terminated";
            emp.isTerminated = true;
            _context.Entry(emp).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            ViewData["EmpType"] = emp.employeeType;
            return RedirectToAction(nameof(EmployeesController.EmployeeList), "Employees");
        }

        public async Task<ActionResult>  EmployeeList()
        {

            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            var AllEmployees = _context.Employee.OrderBy(c => c.empId).ToList();
            var EmployeeInfo1 = _context.Employee.Join(_context.PositionInfo, c=>c.empId, d=>d.empId, (c,d)=>new EmployeeListClass{ empid = c.empId, fname = c.fname, lname = c.lname, position = d.jobTitle, status = c.status, managerID = c.managerID, rank=c.rank, email=c.email});
            ViewData["AllEmployees"] = new SelectList(EmployeeInfo1);
            ViewData["EmpType"] = emp.employeeType;
            return View();
        }


        public async Task<IActionResult> ViewNotice()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            ViewData["Message"] = "Page to view an employee's two week notice.";
            ViewData["EmpType"] = emp.employeeType;
            return View();
        }



        public async Task<IActionResult> ViewTerminatedEmployees()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            ViewData["Message"] = "Page to view all terminated employees.";
            var TerminatedEmployees = _context.Employee.Where(x => x.isTerminated == true || x.status == "Terminated").Join(_context.PositionInfo, c => c.empId, d => d.empId, (c, d) => new EmpPosJoined { empID = c.empId, fname = c.fname, lname = c.lname, phoneNumber = c.phoneNumber, email = c.email, jobTitle = d.jobTitle, department = c.department, managerID = c.managerID }).ToList();
            ViewData["TerminatedEmployees"] = new SelectList(TerminatedEmployees);
            ViewData["EmpType"] = emp.employeeType;
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
