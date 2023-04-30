using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using TaskManagmentApi.Data.Models;

namespace TaskManagmentApi.Data.DBContext
{
    public class TaskDBContext : IdentityDbContext<User>
    {
        public TaskDBContext(DbContextOptions options) : base(options) { }

        public DbSet<Status> Status { get; set; }
        public DbSet<TaskTable> Tasks { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Comment> Comments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Manager>()
            .HasKey(m => m.Id)
           .HasName("ManagerId");

            modelBuilder.Entity<Developer>()
            .HasKey(d => d.Id)
            .HasName("DeveloperId");

            modelBuilder.Entity<Status>()
            .HasKey(s => s.Id)
            .HasName("StatusId");

            modelBuilder.Entity<TaskTable>()
            .HasKey(t => t.Id)
            .HasName("TaskId");


            modelBuilder.Entity<Manager>()
               .HasMany(e => e.Tasks)
               .WithOne(e => e.Manager)
               .HasForeignKey(e => e.ManagerId)
               .IsRequired(false);

            modelBuilder.Entity<Developer>()
               .HasMany(e => e.Tasks)
               .WithOne(e => e.Developer)
               .HasForeignKey(e => e.DeveloperId)
               .IsRequired(false);

            modelBuilder.Entity<Status>()
               .HasMany(e => e.Tasks)
               .WithOne(e => e.Status)
               .HasForeignKey(e => e.StatusId)
               .IsRequired(false);

            modelBuilder.Entity<TaskTable>()
               .HasMany(e => e.Comments)
               .WithOne(e => e.Task)
               .HasForeignKey(e => e.TaskId)
               .IsRequired(false);

            modelBuilder.Entity<User>()
               .HasOne<Manager>(s => s.Manager)
               .WithOne(c => c.User)
               .HasForeignKey<Manager>(ad => ad.Id);

        }
    }
}
