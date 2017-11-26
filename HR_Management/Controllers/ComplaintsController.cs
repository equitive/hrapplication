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

        public IActionResult ViewComplaint(int ID)
        {
            ViewData["Message"] = "Page to view a specific time off.";
            Complaints complaint = _context.Complaints.Where(x => x.ID == ID).First();
            Employee cmpfrom = _context.Employee.Where(x => x.empId == complaint.empId).First();
            ViewData["Complaint"] = complaint;
            ViewData["FromName"] = cmpfrom.fname + " " + cmpfrom.lname;
            return View();
        }

        [HttpGet]
        public IActionResult EditComplaint(int ID)
        {
            ViewData["Message"] = "Page to edit a time off.";
            Complaints compl = _context.Complaints.Where(x => x.ID == ID).First();
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
            var CurrentComplaintList = _context.Complaints.Where(x => x.empId == emp.empId);
            ViewData["CurrentEmployeeComplaints"] = new SelectList(CurrentComplaintList);
            ViewData["ShowAdd"] = emp.employeeType == 0 ? false : true;
            return View();
		}

		public IActionResult OutstandingComplaints()
		{
            //If logged in user is HR, lists all company complaints
			ViewData["Message"] = "Page to view all the team complaints.";
            var AllCompanyComplaints = _context.Complaints;
            ViewData["AllComplaints"] = new SelectList(AllCompanyComplaints);
            return View();
		}

		public IActionResult EditComplaintStatus()
		{
			ViewData["Message"] = "Page to edit complaint status.";

			return View();
		}

		public IActionResult TeamMemberComplaints()
		{
			ViewData["Message"] = "Page to edit complaint status.";

			return View();
		}

        [HttpGet]
        public IActionResult AddComplaint()
		{
			ViewData["Message"] = "Page to add a complaints.";

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
            ViewData["Message"] = "Page to add a time off.";
            Complaints complaint = new Complaints();
            complaint.date = DateTime.Now.ToString();
            complaint.title = title;
            complaint.status = "Pending";
            complaint.description = description;
            complaint.empId = emp.empId;
            _context.Complaints.Add(complaint);
            await _context.SaveChangesAsync();
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
