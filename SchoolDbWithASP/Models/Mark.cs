using System.ComponentModel.DataAnnotations;

namespace SchoolDbWithASP.Models;

public class Mark
{
    public int Id { get; set; }
    
    public DateTime Date { get; set; }
    
    [Required]
    public int MarkReceived { get; set; }
    
    [Required]
    public int StudentId { get; set; }
    public Student? Student { get; set; }
    [Required]
    public int SubjectId { get; set; }
    public Subject? Subject { get; set; }
}