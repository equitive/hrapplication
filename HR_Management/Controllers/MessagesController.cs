using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management.Controllers
{
	public class MessagesController : Controller
	{
		public IActionResult ViewMessage()
		{
			ViewData["Message"] = "Page to view all team members.";

			return View();
		}

		public IActionResult MessagesIndex()
		{
			ViewData["Message"] = "Page to view all messages.";

			return View();
		}

		public IActionResult MessagesIndexManager()
		{
			ViewData["Message"] = "Page to view all messages manager view.";

			return View();
		}

		public IActionResult AddMessage()
		{
			ViewData["Message"] = "Page to add a message.";

			return View();
		}

		public IActionResult Error()
		{
			return View();
		}
	}
}
