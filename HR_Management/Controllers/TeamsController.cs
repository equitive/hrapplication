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
	public class TeamsController : Controller
	{
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public TeamsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> ViewTeam()
		{
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            ViewData["EmpLoggedInName"] = emp.fname + " " + emp.lname;
            var employees = _context.Employee.Join(_context.PositionInfo, c => c.empId, d => d.empId, (c, d) => new EmpPosJoined{ fname = c.fname, lname = c.lname, phoneNumber = c.phoneNumber, email = c.email, jobTitle = d.jobTitle, status = d.status, empID = c.empId,department = c.department}).Where(v => v.department == emp.department && v.status == true);
            Employee mgr = _context.Employee.Where(y => y.department == emp.department && (y.employeeType == 1 || y.employeeType == 4)).First();
            int total= _context.Employee.Join(_context.PositionInfo, c => c.empId, d => d.empId, (c, d) => new EmpPosJoined { fname = c.fname, lname = c.lname, phoneNumber = c.phoneNumber, email = c.email, jobTitle = d.jobTitle, empID = c.empId, department = c.department }).Where(v => v.department == emp.department).Count();

            ViewData["Message"] = "Page to view all team members.";
            ViewData["CurrentEmployee"] = emp;
            ViewData["Employees"] = new SelectList(employees);
            ViewData["Manager"] = mgr;
            ViewData["totalTeam"] = total;
            ViewData["EmpType"] = emp.employeeType;
            return View();
		}
		public IActionResult EditTeamMember()
		{
			ViewData["Message"] = "Page to edit a team member.";

			return View();
		}

        public async Task<IActionResult> ViewTeamMember(int ID)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            ViewData["EmpLoggedInName"] = emp.fname + " " + emp.lname;
            //Show the info the selected Team Member
            ViewData["Message"] = "Page to view one team member.";
            var ChosenEmployee = _context.Employee.Join(_context.PositionInfo, c => c.empId, d => d.empId, (c, d) => new EmpPosJoined { fname = c.fname, lname = c.lname, phoneNumber = c.phoneNumber, email = c.email, jobTitle = d.jobTitle, department = c.department, empID = c.empId }).Where(x => x.empID == ID).First();
            Employee ChosenEmployee2 = _context.Employee.Where(x => x.empId == ID).First();
            Employee mgr = _context.Employee.Where(x => x.empId == ChosenEmployee2.managerID).First();
            ViewData["ChosenEmployee"] = ChosenEmployee;
            ViewData["ChosenEmployee2"] = ChosenEmployee2;
            ViewData["ChosenManager"] = mgr;
            ViewData["EmpType"] = emp.employeeType;

            return View();
		}

		public async Task<IActionResult> ViewTeamManager()
		{
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            ViewData["EmpLoggedInName"] = emp.fname + " " + emp.lname;
            var employees = _context.Employee.Join(_context.PositionInfo, c => c.empId, d => d.empId, (c, d) => new EmpPosJoined { fname = c.fname, lname = c.lname, phoneNumber = c.phoneNumber, email = c.email, jobTitle = d.jobTitle, department = c.department, empID=c.empId }).Where(v => v.department == emp.department);
            Employee mgr = _context.Employee.Where(y => y.department == emp.department && y.employeeType == 1).First();
            ViewData["Message"] = "Page to view team manager/hr view.";
            ViewData["Employees"] = new SelectList(employees);
            ViewData["Manager"] = mgr;
            ViewData["EmpType"] = emp.employeeType;
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
        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
