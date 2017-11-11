using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace HR_Management.Models.HRModels
{
    public class TimeOff
    {
        [Required]
        [Key]
        public int ID { get; set; }
        public int empId { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string description { get; set; }
        public bool approve { get; set; }
        public string type { get; set; }
    }
}
