using System.ComponentModel.DataAnnotations;

namespace SchoolDbWithASP.Models;

public class Student
{
    public Student()
    {
        Marks = new List<Mark>();
    }
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = null!;
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = null!;

    public int? GroupId { get; set; }

    public Group? Group { get; set; }

    public List<Mark> Marks { get; set; }
    
}