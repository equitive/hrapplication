using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management.Models.HRModels
{
    public class EmpPosJoined
    {
        public string fname { get; set; }
        public string lname { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string jobTitle { get; set; }
        public string department { get; set; }
    }
    public class RevMgrJoined
    {
        public string date { get; set; }
        public string title { get; set; }
        public int manager { get; set; }
        public double score { get; set; }
        public string description { get; set; }
        public string mgrfname { get; set; }
        public string mgrlname { get; set; }
        public int ID { get; set; }
    }
}
