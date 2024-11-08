using System.ComponentModel.DataAnnotations;

namespace SchoolDbWithASP.Models;

public class Group
{
    public Group()
    {
        Students = new List<Student>();
    }
    public int Id { get; set; }

    [Required]
    [MaxLength(20)]
    public string Name { get; set; } = null!;

    public List<Student> Students { get; set; }
}