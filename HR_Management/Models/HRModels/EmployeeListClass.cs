using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management.Models.HRModels
{
    public class EmployeeListClass
    {
       public string fname { get; set; }
       public string lname { get; set; } 
       public string position { get; set; }
       public string status { get; set; }
       public string email { get; set; }
       public int rank { get; set; }
       public int managerID { get; set; }
       
    }
}
