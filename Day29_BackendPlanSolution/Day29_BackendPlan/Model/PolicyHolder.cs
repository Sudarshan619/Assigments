namespace Day29_BackendPlan.Model
{
    public class PolicyHolder
    {
        public PolicyHolder() { }

        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public long Phone { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public User User { get; set; }
        public IEnumerable<ClaimReport> ClaimReport { get; set; }

        public IEnumerable<PolicyItem> Items { get; set; }


    }
}
