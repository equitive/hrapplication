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
    public class PosMgrJoined
    {
        public bool status { get; set; } //Approved / Rejected / Promoted
        public string jobTitle { get; set; }
        public double salary { get; set; }
        public string mgrfname { get; set; }
        public string mgrlname { get; set; }
        public DateTime startDate { get; set; }
        public DateTime? endDate { get; set; }
    }
    public class MessageFromClass
    {
        public int messageID;
        public string title { get; set; }
        public string content { get; set; }
        public int employeeFromID { get; set; }
        public string date { get; set; }
        public bool isRead { get; set; }
        public string fnameFrom { get; set; }
        public string lnameFrom { get; set; }
    }
}
