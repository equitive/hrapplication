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
    public class ComplaintsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        public ComplaintsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> ViewComplaint(int ID)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            ViewData["EmpLoggedInName"] = emp.fname + " " + emp.lname;
            ViewData["Message"] = "Page to view a specific time off.";
            Complaints complaint = _context.Complaints.Where(x => x.ID == ID).First();
            Employee cmpfrom = _context.Employee.Where(x => x.empId == complaint.empId).First();
            ViewData["Complaint"] = complaint;
            ViewData["FromName"] = cmpfrom.fname + " " + cmpfrom.lname;
            ViewData["EmpType"] = emp.employeeType;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditComplaint(int ID)
        {
            ViewData["Message"] = "Page to edit a time off.";
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            ViewData["EmpLoggedInName"] = emp.fname + " " + emp.lname;
            Complaints compl = _context.Complaints.Where(x => x.ID == ID).First();
            Employee empCompl = _context.Employee.Where(x => x.empId == compl.empId).First();
            ViewData["ShowStatusChange"] = compl.empId == emp.empId ? false : emp.employeeType == 0 ? false : true;

            Messages msg = new Messages { title = "Complaint edited", content = "Complaint filed by employee " + empCompl.fname + " " + empCompl.lname + " has been changed.", date = DateTime.Now.ToString(), employeeToID = empCompl.empId, isRead = false, employeeFromID = -1 };
            _context.Messages.Add(msg);
            await _context.SaveChangesAsync();
            ViewData["EmpType"] = emp.employeeType;
            return View(compl);
        }

        [HttpPost]
        public async Task<IActionResult> EditComplaint(Complaints compledited)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(compledited).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ComplaintsController.ComplaintsIndex), "Complaints");
        }

        public async Task<IActionResult> ComplaintsIndex()
        {
            //Lists regular employee complaints
            ViewData["Message"] = "Page to view all the complaints.";
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            ViewData["EmpLoggedInName"] = emp.fname + " " + emp.lname;
            var CurrentComplaintList = _context.Complaints.Where(x => x.empId == emp.empId);
            ViewData["CurrentEmployeeComplaints"] = new SelectList(CurrentComplaintList);
            ViewData["ShowAdd"] = emp.employeeType == 0 ? false : true;
            ViewData["EmpType"] = emp.employeeType;
            return View();
        }

        public async Task<IActionResult> OutstandingComplaints()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            ViewData["EmpLoggedInName"] = emp.fname + " " + emp.lname;
            //If logged in user is HR, lists all company complaints
            ViewData["Message"] = "Page to view all the team complaints.";
            var AllCompanyComplaints = _context.Complaints;
            ViewData["AllComplaints"] = new SelectList(AllCompanyComplaints);
            ViewData["EmpType"] = emp.employeeType;
            return View();
        }

        public async Task<IActionResult> EditComplaintStatus()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            ViewData["EmpLoggedInName"] = emp.fname + " " + emp.lname;
            ViewData["Message"] = "Page to edit complaint status.";
            ViewData["EmpType"] = emp.employeeType;

            return View();
        }

        public async Task<IActionResult> TeamMemberComplaints(int ID = -1)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            var allComplaints = _context.Complaints.Where(z => z.empId == ID);
            ViewData["Complaints"] = new SelectList(allComplaints);
            ViewData["EmpLoggedInName"] = emp.fname + " " + emp.lname;
            ViewData["Message"] = "Page to edit complaint status.";
            ViewData["EmpType"] = emp.employeeType;
            return View();
		}

        [HttpGet]
        public async Task<IActionResult> AddComplaint()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            ViewData["EmpLoggedInName"] = emp.fname + " " + emp.lname;
            ViewData["Message"] = "Page to add a complaints.";
            ViewData["EmpType"] = emp.employeeType;

            return View();
		}


        [HttpPost]
        public async Task<IActionResult> AddComplaint(string title, string description)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            ViewData["EmpLoggedInName"] = emp.fname + " " + emp.lname;
            ViewData["Message"] = "Page to add a time off.";
            Complaints complaint = new Complaints();
            complaint.date = DateTime.Now.ToString();
            complaint.title = title;
            complaint.status = "Pending";
            complaint.description = description;
            complaint.empId = emp.empId;
            _context.Complaints.Add(complaint);
            await _context.SaveChangesAsync();
            Messages msg = new Messages { title = "Complaint filed", content = "Complaint filed by employee " + emp.fname + " " + emp.lname + ".", date = DateTime.Now.ToString(), employeeToID = emp.empId, isRead = false, employeeFromID = -1 };
            _context.Messages.Add(msg);
            await _context.SaveChangesAsync();
            ViewData["EmpType"] = emp.employeeType;
            return RedirectToAction(nameof(ComplaintsController.ComplaintsIndex), "Complaints");
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
