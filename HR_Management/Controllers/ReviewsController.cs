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
            return View();

        }

        public IActionResult EditReview()
		{
			ViewData["Message"] = "Page to edit a review.";

			return View();
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
            ViewData["Reviews"] = new SelectList(reviews);
            return View();
		}

		public IActionResult ReviewsIndexManager()
		{
			ViewData["Message"] = "Page to view all the reviews.";

			return View();
		}

		public IActionResult TeamReviews()
		{
			ViewData["Message"] = "Page to view all team reviews.";

			return View();
		}

		public IActionResult TeamMemberReviews()
		{
			ViewData["Message"] = "Page to edit complaint status.";

			return View();
		}

		public IActionResult AddReview()
		{
			ViewData["Message"] = "Page to add a review.";

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
