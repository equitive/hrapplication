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

        public IActionResult ViewTimeOff()
		{
			ViewData["Message"] = "Page to view a specific time off.";

			return View();
		}

        [HttpGet]
        public IActionResult EditTimeOff(int ID)
		{
			ViewData["Message"] = "Page to edit a time off.";
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


        public async Task<IActionResult> TimeOffIndex()
		{
			ViewData["Message"] = "Page to view all the time offs.";
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            var timeofs = _context.TimeOff.Where(x => x.empId == emp.empId);
            ViewData["TimeOffs"] = new SelectList(timeofs);
			return View();
		}

		public IActionResult TimeOffIndexManager()
		{
			ViewData["Message"] = "Page to view all the time offs.";

			return View();
		}

		public IActionResult OutstandingTimeOff()
		{
			ViewData["Message"] = "Page to view all the time off requests.";

			return View();
		}

		public IActionResult ExceedingTimeOff()
		{
			ViewData["Message"] = "Page to view all the time off requests.";

			return View();
		}

		public IActionResult TeamMemberTimeOff()
		{
			ViewData["Message"] = "Page to edit complaint status.";

			return View();
		}

		public IActionResult AddTimeOff()
		{
			ViewData["Message"] = "Page to add a time off.";

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
