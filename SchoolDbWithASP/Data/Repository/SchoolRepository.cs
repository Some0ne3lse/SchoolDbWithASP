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

        using (var db = _dbContext)
        {
            return await db.Students.Include(g => g.Group).Include(m => m.Marks).ToListAsync();
            
        }
    }

    public async Task<Student?> GetStudentByIdAsync(int id)
    {
        using (var db = _dbContext)
        {
            return await db.Students.Include(g => g.Group).Include(m => m.Marks).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}