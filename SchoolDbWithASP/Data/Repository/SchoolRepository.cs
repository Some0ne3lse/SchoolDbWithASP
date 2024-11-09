using Microsoft.EntityFrameworkCore;
using SchoolDbWithASP.Data.Interface;
using SchoolDbWithASP.Models;
using SchoolDbWithASP.Models.DTO;

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
            studentToDelete = await db.Students.Include(student => student.Marks).FirstOrDefaultAsync(s => s.Id == id);

            if (studentToDelete == null)
            {
                return false;
            }
            
            if (studentToDelete.Marks.Any())
            {
                throw new InvalidOperationException("Cannot delete a student with associated marks. Please delete the marks first.");
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

    public async Task<List<Subject>> GetAllSubjectsAsync()
    {
        using (var db = _dbContext)
        {
            return await db.Subjects.ToListAsync();
        }
    }

    public async Task<Subject?> GetSubjectByIdAsync(int id)
    {
        using (var db = _dbContext)
        {
            return await db.Subjects.Include(t => t.Teachers).Include(s => s.Marks).FirstOrDefaultAsync(x => x.Id == id);
        }
    }

    public async Task CreateSubjectAsync(Subject subject, List<int>? teacherIds = null)
    {
        using (var db = _dbContext)
        {
            if (teacherIds != null && teacherIds.Any())
            {
                var teachers = await db.Teachers.Where(t => teacherIds.Contains(t.Id)).ToListAsync();
                
                if (teachers.Count != teacherIds.Count)
                {
                    throw new InvalidOperationException("One or more specified teachers do not exist.");
                }

                subject.Teachers.AddRange(teachers);
            }

            await db.Subjects.AddAsync(subject);
            await db.SaveChangesAsync();
        }
    }

    public async Task<Subject?> UpdateSubjectAsync(int id, SubjectDto subjectDto)
    {
        using (var db = _dbContext)
        {
            var subjectToUpdate = await db.Subjects.Include(s => s.Teachers).FirstOrDefaultAsync(s => s.Id == id);

            if (subjectToUpdate == null)
            {
                return null;
            }
            
            subjectToUpdate.Title = subjectDto.Title;
            
            if (subjectDto.TeacherIds != null)
            {
                var teachers = await db.Teachers.Where(t => subjectDto.TeacherIds.Contains(t.Id)).ToListAsync();

                if (teachers.Count != subjectDto.TeacherIds.Count)
                {
                    throw new InvalidOperationException("One or more specified teachers do not exist.");
                }

                subjectToUpdate.Teachers.Clear();
                subjectToUpdate.Teachers.AddRange(teachers);
            }

            await db.SaveChangesAsync();
            return subjectToUpdate;
        }
    }

    public async Task<bool> DeleteSubjectAsync(int id)
    {
        Subject? subjectToDelete;

        using (var db = _dbContext)
        {
            subjectToDelete = await db.Subjects
                .Include(s => s.Marks)
                .Include(s => s.Teachers)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (subjectToDelete == null)
            {
                return false;
            }
            
            // I was very unsure about this part. On one hand, deleting the subject without deleting the marks makes
            // sense, but how do you know what the mark you got was for?
            if (subjectToDelete.Marks.Any())
            {
                throw new InvalidOperationException("Cannot delete a subject that has associated marks.");
            }

            subjectToDelete.Teachers.Clear();

            db.Subjects.Remove(subjectToDelete);
            await db.SaveChangesAsync();
            return true;
        }
    }

    public async Task<List<Teacher>> GetAllTeachersAsync()
    {
        using (var db = _dbContext)
        {
            return await db.Teachers.ToListAsync();
        }
    }

    public async Task<Teacher?> GetTeacherByIdAsync(int id)
    {
        using (var db = _dbContext)
        {
            return await db.Teachers.Include(s => s.Subjects).FirstOrDefaultAsync(x => x.Id == id);
        }
    }

    public async Task CreateTeacherAsync(Teacher teacher, List<int>? subjectIds = null)
{
    using (var db = _dbContext)
    {
        if (subjectIds != null && subjectIds.Any())
        {
            var subjects = await db.Subjects.Where(s => subjectIds.Contains(s.Id)).ToListAsync();

            if (subjects.Count != subjectIds.Count)
            {
                throw new InvalidOperationException("One or more specified subjects do not exist. Subjects is a list");
            }

            teacher.Subjects.AddRange(subjects);
        }

        await db.Teachers.AddAsync(teacher);
        await db.SaveChangesAsync();
    }
}

    public async Task<Teacher?> UpdateTeacherAsync(int id, TeacherDto teacherDto)
    {
        using (var db = _dbContext)
        {
            var teacherToUpdate = await db.Teachers.Include(t => t.Subjects).FirstOrDefaultAsync(t => t.Id == id);

            if (teacherToUpdate == null)
            {
                return null;
            }
            
            teacherToUpdate.FirstName = teacherDto.FirstName;
            teacherToUpdate.LastName = teacherDto.LastName;
            
            if (teacherDto.SubjectIds != null)
            {
                var subjects = await db.Subjects.Where(s => teacherDto.SubjectIds.Contains(s.Id)).ToListAsync();

                if (subjects.Count != teacherDto.SubjectIds.Count)
                {
                    throw new InvalidOperationException("One or more specified subjects do not exist.");
                }
                
                teacherToUpdate.Subjects.Clear();
                teacherToUpdate.Subjects.AddRange(subjects);
            }

            await db.SaveChangesAsync();
            return teacherToUpdate;
        }
    }

    public async Task<bool> DeleteTeacherAsync(int id)
    {
        Teacher? teacherToDelete;

        using (var db = _dbContext)
        {
            teacherToDelete = await db.Teachers
                .Include(t => t.Subjects)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (teacherToDelete == null)
            {
                return false;
            }
            
            teacherToDelete.Subjects.Clear();
            
            db.Teachers.Remove(teacherToDelete);
            await db.SaveChangesAsync();
            return true;
        }
    }
}