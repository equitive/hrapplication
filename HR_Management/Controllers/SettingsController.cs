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
	public class SettingsController : Controller
	{
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public SettingsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> SettingsOverview()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            ViewData["EmpType"] = emp.employeeType;
            ViewData["Message"] = "Your application settings overview page.";

			return View();
		}

		public async Task<IActionResult> QuitJob()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            ViewData["EmpType"] = emp.employeeType;
            ViewData["Message"] = "Quit Job.";

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