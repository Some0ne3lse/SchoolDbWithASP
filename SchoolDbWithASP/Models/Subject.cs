using System.ComponentModel.DataAnnotations;

namespace SchoolDbWithASP.Models;

public class Subject
{
    public Subject()
    {
        Marks = new List<Mark>();
        Teachers = new List<Teacher>();
    }
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Title { get; set; } = null!;

    public List<Mark> Marks { get; set; }

    public List<Teacher> Teachers { get; set; }
    
}