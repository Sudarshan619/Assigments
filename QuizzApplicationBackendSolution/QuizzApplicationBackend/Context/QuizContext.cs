using Microsoft.EntityFrameworkCore;
using QuizzApplicationBackend.Models;
using System;

namespace QuizzApplicationBackend.Context
{
    public class QuizContext : DbContext
    {
        public QuizContext(DbContextOptions contextOptions) : base(contextOptions)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Quiz> Quizes { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Option> Options { get; set; }

        public DbSet<ScoreCard> ScoreCards { get; set; }

        public DbSet<LeaderBoard> LeaderBoards { get; set; }

        public DbSet<Query> Queries { get; set; }


        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Query and User Relationship
            modelBuilder.Entity<Query>()
                .HasOne(e => e.User)
                .WithMany(a => a.Queries)
                .HasForeignKey(e => e.ReportedBy)
                .HasConstraintName("FK_Query_Report")
                .OnDelete(DeleteBehavior.Restrict);

            // Option and Question Relationship
            modelBuilder.Entity<Option>()
                .HasOne(e => e.Question)
                .WithMany(a => a.Options)
                .HasForeignKey(e => e.QuestionId)
                .HasConstraintName("FK_Question_Option")
                .OnDelete(DeleteBehavior.Restrict);

            // Quiz and User Relationship
            modelBuilder.Entity<Quiz>()
                .HasOne(e => e.User)
                .WithMany(a => a.Quiz)
                .HasForeignKey(e => e.CreatorId)
                .HasConstraintName("FK_Quiz_creator")
                .OnDelete(DeleteBehavior.Restrict);

            // LeaderBoard and Quiz Relationship
            modelBuilder.Entity<LeaderBoard>()
                .HasOne(e => e.Quiz)
                .WithOne(e => e.LeaderBoard)
                .HasForeignKey<LeaderBoard>(e => e.QuizId)
                .HasConstraintName("FK_Quiz_leaderboard")
                .OnDelete(DeleteBehavior.Cascade);


            base.OnModelCreating(modelBuilder);
        }

    }

}
