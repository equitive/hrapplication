using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace HR_Management.Models.HRModels
{
    public class Employee
    {//Properties
        [Required]
        [Key]
        public int ID { get; set; }
        public int empId { get; set; }
        public string appuserid { get; set; }
        public string password { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string status { get; set; }
        public string department { get; set; }
        public int jobTitle { get; set; }
        public int vacationDays { get; set; }
        public int sickTime { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int managerID { get; set; }
        public int employeeType { get; set; }
        public int rank { get; set; }
        public byte[] ImageData { get; set; }
        public bool isTerminated { get; set; }
        public string terminationReason { get; set; }
        public string twoWeeksNotice { get; set; }

    }
}
