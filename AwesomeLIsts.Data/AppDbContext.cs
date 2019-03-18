using AwesomeLists.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AwesomeLIsts.Data
{
    public sealed class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(user =>
            {
                user.Property(u => u.FirstName).IsRequired();

                user.Property(u => u.Id)
                    .IsRequired()
                    .ValueGeneratedNever();

                user.Property(u => u.LastName).IsRequired();
            });

            modelBuilder.Entity<TaskList>(taskList =>
            {
                taskList.HasOne(tl => tl.User)
                    .WithMany(u => u.TaskLists)
                    .HasForeignKey(tl => tl.UserId);

                taskList.Property(tl => tl.UserId)
                    .IsRequired();

                taskList.Property(tl => tl.Name)
                    .IsRequired();
            });

            modelBuilder.Entity<AppTask>(task =>
            {
                task.Property(t => t.Name)
                    .IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<AppTask> Tasks { get; set; }

        public DbSet<TaskList> TaskLists { get; set; }
    }
}
