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
            return await db.Students.Include(m => m.Marks).ToListAsync();
            
        }
    }

    public async Task<Student?> GetStudentByIdAsync(int id)
    {
        using (var db = _dbContext)
        {
            return await db.Students.Include(g => g.Group).Include(m => m.Marks).FirstOrDefaultAsync(x => x.Id == id);
        }
    }

    public async Task CreateStudentAsync(Student student)
    {
        using (var db = _dbContext)
        {
            await db.Students.AddAsync(student);
            await db.SaveChangesAsync();
        }
    }

    public async Task<Student?> UpdateStudentAsync(int id, Student student)
    {
        Student? studentToUpdate;
        
        using (var db = _dbContext)
        {
            studentToUpdate = await db.Students.FirstOrDefaultAsync(s => s.Id == id);
            
            if (studentToUpdate == null)
            {
                return null;
            }

            studentToUpdate.FirstName = student.FirstName;
            studentToUpdate.LastName = student.LastName;
            studentToUpdate.GroupId = student.GroupId;

            await db.SaveChangesAsync();

            return studentToUpdate;
        }
    }

    public async Task<bool> DeleteStudentAsync(int id)
    {
        Student? studentToDelete;

        using (var db = _dbContext)
        {
            studentToDelete = await db.Students.FirstOrDefaultAsync(s => s.Id == id);

            if (studentToDelete == null)
            {
                return false;
            }

            db.Students.Remove(studentToDelete);
            await db.SaveChangesAsync();
            return true;
        }
    }
    
    public async Task<List<Group>> GetAllGroupsAsync()
    {

        using (var db = _dbContext)
        {
            return await db.Groups.ToListAsync();

        }
    }

    public async Task<Group?> GetGroupByIdAsync(int id)
    {
        using (var db = _dbContext)
        {
            return await db.Groups.Include(s => s.Students).FirstOrDefaultAsync(g => g.Id == id);
        }
    }

    public async Task CreateGroupAsync(Group group)
    {
        using (var db = _dbContext)
        {
            await db.Groups.AddAsync(group);
            await db.SaveChangesAsync();
        }
    }

    public async Task<Group?> UpdateGroupAsync(int id, Group group)
    {
        Group? groupToUpdate;

        using (var db = _dbContext)
        {
            groupToUpdate = await db.Groups.FirstOrDefaultAsync(g => g.Id == id);

            if (groupToUpdate == null)
            {
                return null;
            }

            groupToUpdate.Name = group.Name;

            await db.SaveChangesAsync();

            return groupToUpdate;
        }
    }

    public async Task<bool> DeleteGroupAsync(int id)
    {
        Group? groupToDelete;

        using (var db = _dbContext)
        {
            groupToDelete = await db.Groups.Include(s => s.Students).FirstOrDefaultAsync(g => g.Id == id);

            if (groupToDelete == null)
            {
                return false;
            }
            
            if (groupToDelete.Students.Any())
            {
                throw new InvalidOperationException("Cannot delete a group that has associated students.");
            }

            db.Groups.Remove(groupToDelete);
            await db.SaveChangesAsync();
            return true;
        }
    }
}