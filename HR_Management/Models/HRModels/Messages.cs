using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HR_Management.Models.HRModels
{
    public class Messages
    {
        [Required]
        [Key]
        public int ID { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public int employeeFromID { get; set; }
        public int employeeToID { get; set; }
        public string date { get; set; }
        public bool isRead { get; set; }
    }
}
