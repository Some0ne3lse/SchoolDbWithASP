using System.ComponentModel.DataAnnotations;

namespace SchoolDbWithASP.Models;

public class Teacher
{
    public Teacher()
    {
        Subjects = new List<Subject>();
    }
    public int Id { get; set; }

    [MaxLength(100)]
    public string FirstName { get; set; } = null!;

    [MaxLength(100)]
    public string LastName { get; set; } = null!;
    
    public List<Subject> Subjects { get; set; }
}