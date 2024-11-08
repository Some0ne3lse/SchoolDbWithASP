namespace SchoolDbWithASP.Models.DTO;

public class SubjectDto
{
    public string Title { get; set; } = null!;
    public List<int>? TeacherIds { get; set; }
}