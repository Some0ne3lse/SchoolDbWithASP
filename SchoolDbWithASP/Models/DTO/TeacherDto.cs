namespace SchoolDbWithASP.Models.DTO;

public class TeacherDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public List<int>? SubjectIds { get; set; }
}