using System.ComponentModel.DataAnnotations;

namespace Day29_BackendPlan.Model
{
    public class Policy
    {
        [Key]
        public int PolicyId { get; set; }

        public string PolicyName { get; set; } = string.Empty;

        public string PolicyType {  get; set; } = string.Empty;

        public double PolicyValue { get; set; }

        public IEnumerable<PolicyItem> Items { get; set; }

    }
}
