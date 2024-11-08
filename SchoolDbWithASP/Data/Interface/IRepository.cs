using SchoolDbWithASP.Models;

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
}