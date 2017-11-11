using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult AddEmployee()
        {
            ViewData["Message"] = "Page to add an employee";

            return View();
        }

        public IActionResult TerminateEmployee()
        {
            ViewData["Message"] = "Page to terminate employees that request to Quit.";

            return View();
        }

        public IActionResult EmployeeList()
        {
            ViewData["Message"] = "Page to list all employees.";

            return View();
        }


        public IActionResult ViewNotice()
        {
            ViewData["Message"] = "Page to view an employee's two week notice.";

            return View();
        }

        public IActionResult ApproveTermination()
        {
            ViewData["Message"] = "Page to approve the termination of an employee.";

            return View();
        }

        public IActionResult ViewTerminatedEmployees()
        {
            ViewData["Message"] = "Page to view all terminated employees.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
