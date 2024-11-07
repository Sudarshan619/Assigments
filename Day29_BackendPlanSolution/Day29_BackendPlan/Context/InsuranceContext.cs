
using Day29_BackendPlan.Model;
using Microsoft.EntityFrameworkCore;

namespace Day29_BackendPlan.Context
{
    public class InsuranceContext:DbContext
    {
        public InsuranceContext(DbContextOptions contextOptions) : base(contextOptions)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Policy> Policies {get; set;}

        public DbSet<PolicyHolder> PolicyHolders {get; set;}
        public DbSet<PolicyItem> PolicyItems {get; set;}

        public DbSet<ClaimReport> ClaimReports {get; set;}

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            //cartitem has name sno so it wont create primary key automatically
            //modelBuilder.Entity<CartItem>().HasKey(ci => ci.SNo).HasName("PK_CartItem");

            //order contains  


            modelBuilder.Entity<ClaimReport>()
                .HasOne(od => od.PolicyHolder)
                .WithMany(p => p.ClaimReport)
                .HasForeignKey(od => od.PolicyHolderId)
                .HasConstraintName("FK_ClaimReport_PolicyHolder")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PolicyHolder>()
                .HasOne(ph => ph.User)
                .WithOne(u => u.PolicyHolder)
                .HasForeignKey<PolicyHolder>(ph => ph.Username)
                .HasConstraintName("Fk_PolicyHolder_user");
        }
    }
}
