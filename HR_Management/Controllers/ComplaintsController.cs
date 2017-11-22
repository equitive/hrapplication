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
        public IActionResult ViewComplaint()
		{
			ViewData["Message"] = "Page to view a complaints.";

			return View();
		}

		public IActionResult EditComplaint()
		{
			ViewData["Message"] = "Page to edit a complaints.";

			return View();
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
            return View();
		}

		public async Task<ActionResult> ComplaintsIndexManager()
		{
            //Lists complaints for manager
			ViewData["Message"] = "Page to view all the complaints manager view.";
            //Lists regular employee complaints
            ViewData["Message"] = "Page to view all the complaints.";
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            var CurrentComplaintList = _context.Complaints.Where(x => x.empId == emp.empId);
            ViewData["CurrentManagerComplaints"] = new SelectList(CurrentComplaintList);
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

		public IActionResult AddComplaint()
		{
			ViewData["Message"] = "Page to add a complaints.";

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
