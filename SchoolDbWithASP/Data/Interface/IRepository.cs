using SchoolDbWithASP.Models;
using SchoolDbWithASP.Models.DTO;

namespace SchoolDbWithASP.Data.Interface;

public interface IRepository
{
    Task<List<Student>> GetAllStudentsAsync();

    Task<Student?> GetStudentByIdAsync(int id);

    Task CreateStudentAsync(Student student);

    Task<Student?> UpdateStudentAsync(int id, Student student);

    Task<bool> DeleteStudentAsync(int id);
    
    

    Task<List<Group>> GetAllGroupsAsync();
    Task<Group?> GetGroupByIdAsync(int id);

    Task CreateGroupAsync(Group group);

    Task<Group?> UpdateGroupAsync(int id, Group group);

    Task<bool> DeleteGroupAsync(int id);
    
    
    Task<List<Mark>> GetAllMarksAsync();
    
    Task<Mark?> GetMarkByIdAsync(int id);

    Task CreateMarkAsync(Mark mark);

    Task<Mark?> UpdateMarkAsync(int id, Mark mark);

    Task<bool> DeleteMarkAsync(int id);
    
    Task<List<Subject>> GetAllSubjectsAsync();

    Task<Subject?> GetSubjectByIdAsync(int id);

    Task CreateSubjectAsync(Subject subject, List<int>? teacherId = null);

    Task<Subject?> UpdateSubjectAsync(int id, SubjectDto subjectDto);

    Task<bool> DeleteSubjectAsync(int id);
    
    Task<List<Teacher>> GetAllTeachersAsync();

    Task<Teacher?> GetTeacherByIdAsync(int id);

    Task CreateTeacherAsync(Teacher teacher, List<int>? subjectIds = null);

    Task<Teacher?> UpdateTeacherAsync(int id, TeacherDto teacherDto);

    Task<bool> DeleteTeacherAsync(int id);
    
}