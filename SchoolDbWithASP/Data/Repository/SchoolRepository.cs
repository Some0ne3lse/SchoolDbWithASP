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
            if (student.GroupId.HasValue)
            {
                var groupExists = await db.Groups.AnyAsync(g => g.Id == student.GroupId.Value);
                if (!groupExists)
                {
                    throw new InvalidOperationException("The specified group does not exist");
                }
            }
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

            if (student.GroupId.HasValue)
            {
                var groupExists = await db.Groups.AnyAsync(g => g.Id == student.GroupId.Value);
                if (!groupExists)
                {
                    throw new InvalidOperationException("Associated Group not found.");
                }
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

    public async Task<List<Mark>> GetAllMarksAsync()
    {
        using (var db = _dbContext)
        {
            return await db.Marks.Include(s => s.Student).ToListAsync();
        }
    }

    public async Task<Mark?> GetMarkByIdAsync(int id)
    {
        using (var db = _dbContext)
        {
            return await db.Marks.Include(s => s.Student).Include(s => s.Subject).FirstOrDefaultAsync(x => x.Id == id);
        }
    }

    public async Task CreateMarkAsync(Mark mark)
    {
        using (var db = _dbContext)
        {
            var studentExists = await db.Students.AnyAsync(s => s.Id == mark.StudentId);
            if (!studentExists)
            {
                throw new InvalidOperationException("The student does not exist");
            }

            var subjectExists = await db.Subjects.AnyAsync(s => s.Id == mark.SubjectId);
            if (!subjectExists)
            {
                throw new InvalidOperationException("The subject does not exist");
            }
            
            await db.Marks.AddAsync(mark);
            await db.SaveChangesAsync();
        }
    }

    public async Task<Mark?> UpdateMarkAsync(int id, Mark mark)
    {
        Mark? markToUpdate;

        using (var db = _dbContext)
        {
            markToUpdate = await db.Marks.FirstOrDefaultAsync(m => m.Id == id);

            if (markToUpdate == null)
            {
                return null;
            }
            
            var studentExists = await db.Students.AnyAsync(s => s.Id == mark.StudentId);
            var subjectExists = await db.Subjects.AnyAsync(s => s.Id == mark.SubjectId);
    
            if (!studentExists || !subjectExists)
            {
                throw new InvalidOperationException("Associated Student or Subject not found.");
            }

            markToUpdate.Date = mark.Date;
            markToUpdate.MarkReceived = mark.MarkReceived;
            markToUpdate.StudentId = mark.StudentId;
            markToUpdate.SubjectId = mark.SubjectId;

            await db.SaveChangesAsync();

            return markToUpdate;
        }
    }

    public async Task<bool> DeleteMarkAsync(int id)
    {
        Mark? markToDelete;

        using (var db = _dbContext)
        {
            markToDelete = await db.Marks.FirstOrDefaultAsync(m => m.Id == id);

            if (markToDelete == null)
            {
                return false;
            }

            db.Marks.Remove(markToDelete);
            await db.SaveChangesAsync();
            return true;
        }
    }
}