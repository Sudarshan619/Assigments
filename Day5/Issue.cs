using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day5
{
    internal class Issue
    {
        public Issue(int id, string name, string description, string reportedBY,int assignedTo)
        {
            Id = id;
            Name = name;
            Description = description;
            ReportedBY = reportedBY;
            AssignedTo = assignedTo;
            CreatedDate = DateTime.Now;
            Status = "Just Now";
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string ReportedBY { get; set; }
        public int AssignedTo { get; set; }
       
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }

        public DateTime ClosedDate { get; set; }

        public bool ChangeStatus(string newStatus)
        {
            if (Status == "Closed")
            {
                Console.WriteLine("Cannot change staus as the issue is closed. Please raise a new Issue");
                return false;
            }
            Status = newStatus;
            if (newStatus == "Closed")
            {
                ClosedDate = DateTime.Now;
                Console.WriteLine($"Issue with {Id} is maked as Closed");
            }
            return true;
        }
        public bool AssignIssue(int assignedTo)
        {
            if (Status == "Closed")
            {
                Console.WriteLine("Cannot assign issue as the issue is closed. Please raise a new Issue");
                return false;
            }
            AssignedTo = assignedTo;
            Console.WriteLine($"Issue with {Id} successfully Assigned to {AssignedTo}");
            return true;
        }

        public override string ToString()
        {
            var details = $"ID:{Id}\n Name:{Name}\n Created At:{CreatedDate}\nDescription:{Description}\nReported by:{ReportedBY}\n Status :{Status}\n Closed at:";
            details += ClosedDate == null ? "-" : ClosedDate;
            return details;
       
        }

    }
}

