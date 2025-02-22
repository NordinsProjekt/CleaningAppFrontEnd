using CleaningApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CleaningApp.Infrastructure.UnitOfWork;

public class CleaningDBContext : DbContext
{
    public CleaningDBContext(DbContextOptions<CleaningDBContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<TaskType> TaskTypes { get; set; }
    public DbSet<CleaningTask> Tasks { get; set; }
    public DbSet<TaskTemplate> TaskTemplates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed Users
        var user1 = new User { Id = Guid.NewGuid(), Name = "Markus" };
        var user2 = new User { Id = Guid.NewGuid(), Name = "Cecilia" };
        var user3 = new User { Id = Guid.NewGuid(), Name = "Planerad" };
        modelBuilder.Entity<User>().HasData(user1, user2, user3);

        // Seed Rooms
        var rooms = new List<Room>
        {
            new Room { Id = Guid.NewGuid(), Name = "Vardagsrum" },
            new Room { Id = Guid.NewGuid(), Name = "Kök" },
            new Room { Id = Guid.NewGuid(), Name = "Badrum uppe" },
            new Room { Id = Guid.NewGuid(), Name = "Badrum nere" },
            new Room { Id = Guid.NewGuid(), Name = "Sovrum" }
        };
        modelBuilder.Entity<Room>().HasData(rooms);

        // Seed TaskTypes
        var taskTypes = new List<TaskType>
        {
            new TaskType { Id = Guid.NewGuid(), Name = "Dammsugit golv" },
            new TaskType { Id = Guid.NewGuid(), Name = "Tvättat golv" },
            new TaskType { Id = Guid.NewGuid(), Name = "Torkat av alla ytor" },
            new TaskType { Id = Guid.NewGuid(), Name = "Rengjort badrum" },
            new TaskType { Id = Guid.NewGuid(), Name = "Bytt sängkläder" },
            new TaskType { Id = Guid.NewGuid(), Name = "Tömt sopor" },
            new TaskType { Id = Guid.NewGuid(), Name = "Tömt tvättmaskin" },
            new TaskType { Id = Guid.NewGuid(), Name = "Startat tvättmaskin" },
            new TaskType { Id = Guid.NewGuid(), Name = "Tömt diskmaskin" },
            new TaskType { Id = Guid.NewGuid(), Name = "Startat diskmaskin" }
        };
        modelBuilder.Entity<TaskType>().HasData(taskTypes);
    }
}