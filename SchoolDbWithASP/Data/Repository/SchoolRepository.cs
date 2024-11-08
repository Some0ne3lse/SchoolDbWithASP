using Microsoft.EntityFrameworkCore;
using SchoolDbWithASP.Data.Interface;
using SchoolDbWithASP.Models;

namespace SchoolDbWithASP.Data.Repository;

public class SchoolRepository : IRepository
{
    private readonly SchoolDbContext _dbContext;

    public SchoolRepository()
    {
        _dbContext = new SchoolDbContext();
    }

    public async Task<List<Student>> GetAllStudentsAsync()
    {
        List<Student> students;

        using (var db = _dbContext)
        {
            // Check if you need to include lists here!!!
            students = await db.Students.Include(g => g.Group).Include(m => m.Marks).ToListAsync();

            return students;
        }
    }
}