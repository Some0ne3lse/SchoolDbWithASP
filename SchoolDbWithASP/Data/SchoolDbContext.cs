using Microsoft.EntityFrameworkCore;
using SchoolDbWithASP.Models;

namespace SchoolDbWithASP.Data;

public class SchoolDbContext : DbContext
{
    public DbSet<Group> Groups { get; set; }

    public DbSet<Mark> Marks { get; set; }
    
    public DbSet<Student> Students { get; set; }

    public DbSet<Subject> Subjects { get; set; }

    public DbSet<Teacher> Teachers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Group>().HasData(
            new Group { Id = 1, Name = "Group A" },
            new Group { Id = 2, Name = "Group B" }
        );
        
        modelBuilder.Entity<Student>().HasData(
            new Student { Id = 1, FirstName = "John", LastName = "Doe", GroupId = 1 },
            new Student { Id = 2, FirstName = "Jane", LastName = "Smith", GroupId = 2 }
        );
        
        modelBuilder.Entity<Teacher>().HasData(
            new Teacher { Id = 1, FirstName = "Michael", LastName = "Johnson" },
            new Teacher { Id = 2, FirstName = "Sara", LastName = "Connor" }
        );
        
        modelBuilder.Entity<Subject>().HasData(
            new Subject { Id = 1, Title = "Math" },
            new Subject { Id = 2, Title = "History" }
        );

        modelBuilder.Entity<Mark>().HasData(
            new Mark { Id = 1, Date = DateTime.Now, MarkReceived = 85, StudentId = 1, SubjectId = 1 },
            new Mark { Id = 2, Date = DateTime.Now, MarkReceived = 90, StudentId = 2, SubjectId = 2 }
        );
        
        modelBuilder.Entity<Teacher>()
            .HasMany(t => t.Subjects)
            .WithMany(s => s.Teachers)
            .UsingEntity(j => j.HasData(
                new { SubjectsId = 1, TeachersId = 1 },
                new { SubjectsId = 2, TeachersId = 2 },
                new { SubjectsId = 1, TeachersId = 2 }
            ));
    }
    
    public string DbPath { get; }

    public SchoolDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Combine(path, "schoolWithASP.db");
        Console.WriteLine($"Database path: {DbPath}");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}