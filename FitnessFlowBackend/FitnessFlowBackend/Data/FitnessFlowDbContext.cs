using Microsoft.EntityFrameworkCore;
using FitnessFlowBackend.Models;

namespace FitnessFlowBackend.Data
{
    public class FitnessFlowDbContext : DbContext
    {
        public FitnessFlowDbContext(DbContextOptions<FitnessFlowDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Training> Trainings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring table names to be lowercase
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Training>().ToTable("trainings");
        }
    }
}
