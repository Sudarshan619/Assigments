using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6
{
    internal interface ISolver
    {
        public Issue[] GetAllIssues();
        public bool RaiseIssue(int eid, Issue issue);
        public Issue AssignIssue(int eid, int issueId);
    }
}
