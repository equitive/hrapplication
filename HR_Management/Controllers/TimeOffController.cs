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
	public class TimeOffController : Controller
	{
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public TimeOffController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> ViewTimeOff(int ID)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            ViewData["EmpLoggedInName"] = emp.fname + " " + emp.lname;
            ViewData["Message"] = "Page to view a specific time off.";
            TimeOff timeoff = _context.TimeOff.Where(x => x.ID == ID).First();
            ViewData["Timeoff"] = timeoff;
            ViewData["EmpType"] = emp.employeeType;
            return View();
		}

        [HttpGet]
        public async Task<IActionResult> DeleteTimeOff(int ID)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            ViewData["EmpLoggedInName"] = emp.fname + " " + emp.lname;
            ViewData["EmpType"] = emp.employeeType;
            TimeOff timeoff = _context.TimeOff.Where(x => x.ID == ID).First();
            _context.TimeOff.Remove(timeoff);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(TimeOffController.TimeOffIndex), "TimeOff");
        }

        [HttpGet]
        public async Task<IActionResult> EditTimeOff(int ID)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            ViewData["EmpLoggedInName"] = emp.fname + " " + emp.lname;
            ViewData["Message"] = "Page to edit a time off.";
            ViewData["EmpType"] = emp.employeeType;
            TimeOff timeoff = _context.TimeOff.Where(x => x.ID == ID).First();
            return View(timeoff);
		}

        [HttpPost]
        public async Task <IActionResult> EditTimeOff(TimeOff timeoffedited)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(timeoffedited).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(TimeOffController.TimeOffIndex), "TimeOff");
        }

        public async Task<IActionResult> ApproveTimeOff(int ID, int approve)
        {
            ViewData["Message"] = "Page to edit a time off.";
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            ViewData["EmpLoggedInName"] = emp.fname + " " + emp.lname;
            TimeOff timeoff = _context.TimeOff.Where(x => x.ID == ID).First();
            Employee empTimeoff = _context.Employee.Where(x => x.empId == timeoff.empId).First();
            if (approve == 1)
            {
                Messages msg = new Messages { title = "Time-off approved", content = "Your request for time-off was approved", date = DateTime.Now.ToString(), employeeToID = empTimeoff.empId, isRead = false, employeeFromID = emp.empId };
                _context.Messages.Add(msg);
                await _context.SaveChangesAsync();
                timeoff.approve = 3;
            } else
            {
                Messages msg = new Messages { title = "Time-off rejected", content = "Your request for time-off was rejected", date = DateTime.Now.ToString(), employeeToID = empTimeoff.empId, isRead = false, employeeFromID = emp.empId };
                _context.Messages.Add(msg);
                await _context.SaveChangesAsync();
                timeoff.approve = 2;
            }
            _context.Entry(timeoff).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            ViewData["EmpType"] = emp.employeeType;


            return RedirectToAction(nameof(TimeOffController.OutstandingTimeOff), "TimeOff");
        }

        public async Task<IActionResult> TimeOffIndex()
		{
			ViewData["Message"] = "Page to view all the time offs.";
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            ViewData["EmpLoggedInName"] = emp.fname + " " + emp.lname;
            var timeofs = _context.TimeOff.Where(x => x.empId == emp.empId);
            int timeoffsTotal= _context.TimeOff.Where(x => x.empId == emp.empId).Count();
            int VacationDays = _context.TimeOff.Where(x => x.empId == emp.empId && x.type =="Vacation").Count();
            int SickDays = _context.TimeOff.Where(x => x.empId == emp.empId && x.type == "Sick Day").Count();
            ViewData["TimeOffs"] = new SelectList(timeofs);
            ViewData["TotalSickDays"] = SickDays;
            ViewData["TotalVacationDays"] = VacationDays;
            ViewData["ShowAdd"] = emp.employeeType == 0 ? false : true;
            ViewData["EmpType"] = emp.employeeType;
            ViewData["CurrentEmployee"] = emp;
            ViewData["TotalRequests"] = timeoffsTotal ;
            return View();
		}

		//public IActionResult TimeOffIndex()
		//{
		//	ViewData["Message"] = "Page to view all the time offs.";

		//	return View();
		//}

		public async Task<IActionResult> OutstandingTimeOff()
		{
			ViewData["Message"] = "Page to view all the time off requests.";
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            ViewData["EmpLoggedInName"] = emp.fname + " " + emp.lname;
            var CurrentTeam = _context.Employee.Where(x => x.department == emp.department);
            var AllTimeoffs = _context.TimeOff.Where(v => v.approve == 1).Join(CurrentTeam, c => c.empId, d => d.empId, (c, d) =>
            new TimeoffEmployeeJoined { ID = c.ID, approved = c.approve, startDate = c.startDate, endDate = c.endDate, type = c.type, description = c.description, mgrfname = d.fname, mgrlname = d.lname }).OrderByDescending(b => b.ID);
            int totalRows =_context.TimeOff.Where(v => v.approve == 1).Join(CurrentTeam, c => c.empId, d => d.empId, (c, d) =>
            new TimeoffEmployeeJoined { ID = c.ID, approved = c.approve, startDate = c.startDate, endDate = c.endDate, type = c.type, description = c.description, mgrfname = d.fname, mgrlname = d.lname }).OrderByDescending(b => b.ID).Count();
            ViewData["TotalRows"] = totalRows;
            ViewData["CurrentEmployee"] = emp;
            ViewData["AllTimeoffs"] = new SelectList(AllTimeoffs);
            ViewData["EmpType"] = emp.employeeType;

            return View();
		}

		public async Task<IActionResult> ExceedingTimeOff()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            ViewData["EmpLoggedInName"] = emp.fname + " " + emp.lname;
            ViewData["Message"] = "Page to view all the time off requests.";
            ViewData["EmpType"] = emp.employeeType;
            return View();
		}

		public IActionResult TeamMemberTimeOff(int id)
		{
			ViewData["Message"] = "Page to edit complaint status.";
            Employee emp = _context.Employee.Where(z => z.empId == id).First();
            var allTimeOff = _context.TimeOff.Where(z => z.empId == id);
            ViewData["CurrentEmployee"] = emp;
            ViewData["AllTimeOff"] = new SelectList(allTimeOff);
            ViewData["EmpType"] = emp.employeeType;
            return View();
		}

        [HttpGet]
		public IActionResult AddTimeOff()
		{
			ViewData["Message"] = "Page to add a time off.";

			return View();
		}

        [HttpPost]
        public async Task<IActionResult> AddTimeOff(string type, string description, DateTime startDate, DateTime endDate)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            ViewData["EmpLoggedInName"] = emp.fname + " " + emp.lname;
            Employee mgr = _context.Employee.Where(x => x.empId == emp.managerID).First();
            if(startDate > endDate)
            {
                ViewData["Error"] = "Start date cannot be after end date";
                return View();
            }
            ViewData["Message"] = "Page to add a time off.";
            TimeOff newtoff = new TimeOff();
            newtoff.approve = 1; newtoff.description = description; newtoff.empId = emp.empId; newtoff.endDate = endDate.ToString();
            newtoff.startDate = startDate.ToString(); newtoff.type = type;
            _context.TimeOff.Add(newtoff);
            await _context.SaveChangesAsync();
            Messages msg = new Messages { title = "Time-off request submitted", content = "Time-off request submitted by employee " + emp.fname + " " + emp.lname + ".", date = DateTime.Now.ToString(), employeeToID = emp.empId, isRead = false, employeeFromID = mgr.empId};
            Messages msg2 = new Messages { title = "Time-off request submitted by your team member", content = "Time-off request submitted by employee " + emp.fname + " " + emp.lname + ".", date = DateTime.Now.ToString(), employeeToID = mgr.empId, isRead = false, employeeFromID = emp.empId};
            _context.Messages.AddRange(msg, msg2);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(TimeOffController.TimeOffIndex), "TimeOff");
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
