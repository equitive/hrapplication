using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using HR_Management.Models;
using HR_Management.Models.HRModels;
using HR_Management.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HR_Management.Controllers
{
	public class MessagesController : Controller
	{
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public MessagesController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> ViewMessage(int ID)
		{
            Messages message = _context.Messages.Where(x => x.ID == ID).First();
            
            
            Employee empfrom = _context.Employee.Where(x => x.empId == message.employeeFromID).First();
            Employee empto = _context.Employee.Where(x => x.empId == message.employeeToID).First();
            ViewData["Message"] = message;
            ViewData["EmpFrom"] = empfrom.fname + " " + empfrom.lname;
            ViewData["EmpTo"] = empto.fname + " " + empto.lname;
            message.isRead = true;
            _context.Entry(message).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return View();
        }

        public async Task <IActionResult> MessagesIndex()
		{
            ViewData["Message"] = "Page to view all the time offs.";
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            var messages = _context.Messages.Where(x => x.employeeFromID == emp.empId).Join(_context.Employee, c => c.employeeFromID, d => d.empId, (c, d) => new MessageFromClass { messageID = c.ID, fnameFrom = d.fname, lnameFrom = d.lname, date = c.date, title = c.content, content = c.content, isRead = c.isRead}).OrderBy(c => c.date);
            var countRead = messages.Where(x => x.isRead == true).Count();
            var countUnread = messages.Where(x => x.isRead == false).Count();
            var countTotal = countUnread + countRead;
            ViewData["Messages"] = new SelectList(messages);
            ViewData["MessageCount"] = countTotal;
            ViewData["MessageCountRead"] = countRead;
            ViewData["MessageCountUnread"] = countUnread;
            ViewData["Message"] = "Page to view all messages.";
			return View();
		}

		public IActionResult MessagesIndexManager()
		{
			ViewData["Message"] = "Page to view all messages manager view.";

			return View();
		}

        [HttpGet]
		public IActionResult AddMessage()
		{
			ViewData["Message"] = "Page to add a message.";
            ViewData["Error"] = "";
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> AddMessage(string title, string description, string recipientName)
        {
            ViewData["Message"] = "Page to add a message.";
            ViewData["Error"] = "";
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            Employee emp = _context.Employee.Where(x => x.appuserid == user.Id).First();
            Messages msg = new Messages();
            msg.content = description;
            msg.title = title;
            msg.employeeFromID = emp.empId;
            msg.date = DateTime.Now.ToString();
            msg.isRead = false;
            try
            {
                msg.employeeToID = _context.Employee.Where(x => x.fname + " " + x.lname == recipientName).First().empId;
                _context.Entry(msg).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MessagesController.MessagesIndex), "Messages");

            }
            catch (Exception ex)
            {
                ViewData["Error"] = "Could not sent the message. Verify recipient first and last name are correct";
                return View();
            }
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
