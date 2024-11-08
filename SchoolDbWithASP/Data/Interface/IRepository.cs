using SchoolDbWithASP.Models;

namespace SchoolDbWithASP.Data.Interface;

public interface IRepository
{
    Task<List<Student>> GetAllStudentsAsync();
}