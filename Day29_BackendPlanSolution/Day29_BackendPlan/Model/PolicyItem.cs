using System.ComponentModel.DataAnnotations;

namespace Day29_BackendPlan.Model
{
    public class PolicyItem
    {
        [Key]
        public int SNo { get; set; }

        public int PolicyId { get; set; }

        public int PolicyHolderId { get; set; }


        public Policy Policy { get; set; }

        public PolicyHolder PolicyHolder { get; set; }

       //dont create object or else it will consider it a new item and will add it to the table and can cause problem
    }
}
