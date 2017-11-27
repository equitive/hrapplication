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
	public class ReviewsController : Controller
	{
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public ReviewsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }

        public IActionResult ViewReview(int ID)
        {
            ViewData["Message"] = "Page to edit a time off.";
            Reviews rev = _context.Reviews.Where(x => x.ID == ID).First();
            Employee mgr = _context.Employee.Where(x => x.empId == rev.manager).First();
            Employee emp = _context.Employee.Where(x => x.empId == rev.empId).First();
            ViewData["Review"] = rev;
            ViewData["Employee"] = emp;
            ViewData["Manager"] = mgr;
            ViewData["EmpType"] = emp.employeeType;
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> EditReview(int ID)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            ViewData["Message"] = "Page to edit a review.";
            Reviews review = _context.Reviews.Where(x => x.ID == ID).First();
            ViewData["EmpType"] = emp.employeeType;
            return View(review);
        }

        [HttpPost]
        public async Task<IActionResult> EditReview(Reviews reviewedited)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(reviewedited).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ReviewsController.ReviewsIndex), "Reviews");
        }

        public async Task<IActionResult> ReviewsIndex()
		{
			ViewData["Message"] = "Page to view all the reviews.";
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            var reviews = _context.Reviews.Join(_context.Employee, c => c.manager, d => d.empId, (c, d) =>
            new RevMgrJoined { ID = c.ID, date = c.date, title = c.title, manager = c.manager, score = c.score, description = c.description, mgrfname = d.fname, mgrlname = d.lname }).OrderBy(y => y.date) ;
            int totalReviews = _context.Reviews.Join(_context.Employee, c => c.manager, d => d.empId, (c, d) =>
            new RevMgrJoined { ID = c.ID, date = c.date, title = c.title, manager = c.manager, score = c.score, description = c.description, mgrfname = d.fname, mgrlname = d.lname }).OrderBy(y => y.date).Count();
            ViewData["Reviews"] = new SelectList(reviews);
            ViewData["CurrentEmployee"] = emp;
            ViewData["ShowAdd"] = emp.employeeType == 0 ? false : true;
            ViewData["TotalReviews"] = totalReviews;
            ViewData["EmpType"] = emp.employeeType;
            return View();
		}


		public async Task<IActionResult> TeamReviews()
		{
			ViewData["Message"] = "Page to view all team reviews.";
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            var reviews = _context.Reviews.Join(_context.Employee, c => c.empId, d => d.empId, (c, d) =>
            new RevMgrJoined { ID = c.ID, empdepartment = d.department, date = c.date, title = c.title, manager = c.manager, score = c.score, description = c.description, mgrfname = d.fname, mgrlname = d.lname }).OrderBy(y => y.date).Where(b => b.empdepartment == emp.department);
            ViewData["Reviews"] = new SelectList(reviews);
            ViewData["ShowAdd"] = emp.employeeType == 0 ? false : true;
            ViewData["EmpType"] = emp.employeeType;
            return View();
		}

		public IActionResult TeamMemberReviews(int id)
		{
			ViewData["Message"] = "Page to edit complaint status.";
            Employee emp = _context.Employee.Where(z => z.empId == id).First();
            Employee mgr = _context.Employee.Where(z => z.empId == emp.managerID).First();
            var allReviews = _context.Reviews.Where(z => z.empId == id);
            ViewData["AllReviews"] = new SelectList(allReviews);
            ViewData["CurrentEmployee"] = emp;
            ViewData["CurrentManager"] = mgr;
            ViewData["EmpType"] = emp.employeeType;
            return View();
		}

        [HttpGet]
        public async Task<IActionResult> AddReview(int ID = -1)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            ViewData["Message"] = "Page to add a review.";
            ViewData["EmpType"] = emp.employeeType;
            ViewData["empidtopopulate"] = ID == -1 ? "" : ID + "";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(int empId, string title, string description, int score)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            Employee empreviewed = _context.Employee.Where(x => x.empId == empId).First();
            ViewData["Message"] = "Page to add a time off.";
            Reviews review = new Reviews();
            review.date = DateTime.Now.ToString();
            review.title = title;
            review.score = score;
            review.manager = emp.empId;
            review.description = description;
            review.empId = empreviewed.empId;
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            Messages msg = new Messages { title = "Review submitted", content = "Review submitted by manager " + emp.fname + " " + emp.lname + " for employee " + empreviewed.fname + " " + empreviewed.lname + " .", date = DateTime.Now.ToString(), employeeToID = empreviewed.empId, isRead = false, employeeFromID = emp.empId };
            Messages msg2 = new Messages { title = "Review submitted", content = "Review submitted by manager " + emp.fname + " " + emp.lname + " for employee " + empreviewed.fname + " " + empreviewed.lname + " .", date = DateTime.Now.ToString(), employeeToID = emp.empId, isRead = false, employeeFromID = empreviewed.empId };
            _context.Messages.AddRange(msg, msg2);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ReviewsController.ReviewsIndex), "Reviews");
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
