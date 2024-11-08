using Microsoft.AspNetCore.Mvc;
using SchoolDbWithASP.Data.Interface;
using SchoolDbWithASP.Models;

namespace SchoolDbWithASP.Data.Controllers;

[Route("api/students")]
[Controller]
public class StudentsController : ControllerBase
{
    private readonly IRepository _repository;

    public StudentsController(IRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Student>>> GetAllStudents()
    {
        try
        {
            List<Student> students = await _repository.GetAllStudentsAsync();
            return Ok(students);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Student>> GetStudentById(int id)
    {
        try
        {
            Student? stud = await _repository.GetStudentByIdAsync(id);
            if (stud == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(stud);
            }
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
    
}