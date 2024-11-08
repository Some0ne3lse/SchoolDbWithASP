using System.ComponentModel.DataAnnotations;

namespace SchoolDbWithASP.Models;

public class Student
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string FirstName { get; set; } = null!;
    
    [MaxLength(100)]
    public string LastName { get; set; } = null!;

    public int GroupId { get; set; }

    public Group Group { get; set; } = null!;

    public List<Mark> Marks { get; set; }

    public Student()
    {
        Marks = new List<Mark>();
    }
}