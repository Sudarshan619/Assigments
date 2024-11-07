using Day28_EventBooking.Model;
using Microsoft.EntityFrameworkCore;

namespace Day28_EventBooking.Context
{
    public class EventBookingContext:DbContext
    {
        public EventBookingContext(DbContextOptions contextOptions) : base(contextOptions)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<EventBooking> EventBookings { get; set; }

        public DbSet<Booking> Bookings { get; set; }


        public DbSet<User> Users { get; set; }


        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<EventBooking>().HasKey(ci => ci.BookId).HasName("PK_CartItem");

           
            modelBuilder.Entity<EventBooking>()
                .HasOne(o => o.Employee)
                .WithMany(c => c.EventBooking)
                .HasForeignKey(o => o.EmployeeId)
                .HasConstraintName("FK_EmployeeEventBookingId");

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Employee)
                .WithOne(b => b.Booking)
                .HasForeignKey<Booking>(b => b.EmployeeId)
                .HasConstraintName("FK_EmployeeBookingId");

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.User)
                .WithOne(u => u.Employee)
                .HasForeignKey<Employee>(e => e.UserName)
                .HasConstraintName("FK_Employee_User");

        }
    }
}
