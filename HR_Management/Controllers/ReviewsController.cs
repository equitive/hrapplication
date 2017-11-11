using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management.Controllers
{
	public class ReviewsController : Controller
	{
		public IActionResult ViewReview()
		{
			ViewData["Message"] = "Page to view a review.";

			return View();
		}

		public IActionResult EditReview()
		{
			ViewData["Message"] = "Page to edit a review.";

			return View();
		}

		public IActionResult ReviewsIndex()
		{
			ViewData["Message"] = "Page to view all the reviews.";

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
	}
}
