﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using HR_Management.Models;
namespace HR_Management.Controllers
{
    public class AccountDetailsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountDetailsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
		public async Task<IActionResult> AccountOverview()
		{
			ViewData["Message"] = "Employee application account overview page upon login.";
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            return View();
		}

		public IActionResult AccountOverviewEditView()
		{
			ViewData["Message"] = "Your application account overview page when you want to edit.";

			return View();
		}

		public IActionResult EditAccountOverviewEmployee()
		{
			ViewData["Message"] = "Edit the account information.";

			return View();
		}

		public IActionResult ManagerAccountOverview()
		{
			ViewData["Message"] = "Manager application account overview page upon login.";

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