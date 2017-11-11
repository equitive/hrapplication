using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace HR_Management.Models.HRModels
{
    public class PositionInfo
    {
        [Required]
        [Key]
        public int ID { get; set; }
        public bool status { get; set; } //Approved / Rejected / Promoted
        public int managerID { get; set; }
        public int empId { get; set; }
        public string jobTitle { get; set; }
        public string jobDescription { get; set; }
        public double salary { get; set; }
    }
}
