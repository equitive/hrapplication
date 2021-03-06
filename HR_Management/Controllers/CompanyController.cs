﻿using System;
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
    public class CompanyController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public CompanyController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> EmployeeList()
        {
			ViewData["Message"] = "All company employees page.";
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            var AllEmployees = _context.Employee.OrderBy(c => c.empId).ToList();
            var EmployeeInfo1 = _context.Employee.Join(_context.PositionInfo, c => c.empId, d => d.empId, (c, d) => new EmployeeListClass { posStatus = d.status, empid = c.empId, fname = c.fname, lname = c.lname, position = d.jobTitle, status = c.status, managerID = c.managerID, rank = c.rank, email = c.email }).Where(k => k.posStatus == true).OrderBy(v => v.rank);
            int total = _context.Employee.Join(_context.PositionInfo, c => c.empId, d => d.empId, (c, d) => new EmployeeListClass { posStatus = d.status, empid = c.empId, fname = c.fname, lname = c.lname, position = d.jobTitle, status = c.status, managerID = c.managerID, rank = c.rank, email = c.email }).Where(k => k.posStatus == true).OrderBy(v => v.rank).Count();
            ViewData["TotalEmployees"] = total;
            ViewData["AllEmployees"] = new SelectList(EmployeeInfo1);

            return View();
        }

        public async Task<IActionResult> ComplaintsList()
        {
            ViewData["Message"] = "All company complaints.";
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            var AllComplaints = _context.Complaints.OrderBy(x => x.status).ToList();
            int totalComplaints = _context.Complaints.OrderBy(x => x.status).Count();
            ViewData["AllComplaints"] = new SelectList(AllComplaints);
            ViewData["TotalComplaints"] = totalComplaints;
            return View();
        }

        public async Task<IActionResult> TimeOffList()
        {
            ViewData["Message"] = "All company timeoff requests";
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            var AllTimeoffs = _context.TimeOff.Join(_context.Employee, c => c.empId, d => d.empId, (c, d) =>
            new TimeoffEmployeeJoined { ID = c.ID, approved = c.approve, startDate = c.startDate, endDate = c.endDate, type = c.type, description = c.description, mgrfname = d.fname, mgrlname = d.lname }).OrderByDescending(b => b.ID);
            int total = _context.TimeOff.Join(_context.Employee, c => c.empId, d => d.empId, (c, d) =>
            new TimeoffEmployeeJoined { ID = c.ID, approved = c.approve, startDate = c.startDate, endDate = c.endDate, type = c.type, description = c.description, mgrfname = d.fname, mgrlname = d.lname }).OrderByDescending(b => b.ID).Count();
            ViewData["AllTimeoffs"] = new SelectList(AllTimeoffs);
            ViewData["total"] = total;
            return View();
        }

        public async Task<IActionResult> ReviewsList()
        {
            ViewData["Message"] = "All company reviews list.";
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            var AllReviews = _context.Reviews.OrderBy(v => v.manager).Join(_context.Employee, c => c.manager, d => d.empId, (c, d) =>
            new RevMgrJoined { ID = c.ID, date = c.date, title = c.title, manager = c.manager, score = c.score, description = c.description, mgrfname = d.fname, mgrlname = d.lname }).OrderBy(y => y.date);
            int total = _context.Reviews.OrderBy(v => v.manager).Join(_context.Employee, c => c.manager, d => d.empId, (c, d) =>
            new RevMgrJoined { ID = c.ID, date = c.date, title = c.title, manager = c.manager, score = c.score, description = c.description, mgrfname = d.fname, mgrlname = d.lname }).OrderBy(y => y.date).Count();
            ViewData["total"] = total;
            ViewData["AllReviews"] = new SelectList(AllReviews);
            return View();
        }

		public IActionResult PositionsList()
		{
			ViewData["Message"] = "All company reviews list.";
            var allPositions = _context.Employee.Join(_context.PositionInfo, c => c.empId, d => d.empId, (c, d) => new EmpPosJoined { posstartdate = d.startDate.ToString(), salary = d.salary, empID = c.empId, fname = c.fname, lname = c.lname, phoneNumber = c.phoneNumber, status = d.status, email = c.email, jobTitle = d.jobTitle, department = c.department, managerID = c.managerID }).ToList();
            int total = _context.Employee.Join(_context.PositionInfo, c => c.empId, d => d.empId, (c, d) => new EmpPosJoined { posstartdate = d.startDate.ToString(), salary = d.salary, empID = c.empId, fname = c.fname, lname = c.lname, phoneNumber = c.phoneNumber, status = d.status, email = c.email, jobTitle = d.jobTitle, department = c.department, managerID = c.managerID }).ToList().Count();
            ViewData["AllPositions"] = new SelectList(allPositions);
            ViewData["total"] = total;
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
