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
            
            return Ok(stud);
            
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }


    [HttpPost]
    public async Task<IActionResult> CreateStudent([FromBody] Student student)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input. Please ensure all required fields are correctly filled out.");
            }

            await _repository.CreateStudentAsync(student);
            return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
        }
        catch (InvalidOperationException)
        {
            return BadRequest("The group does not exist");
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<Student>> UpdateStudent(int id, [FromBody] Student student)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input. Please ensure all required fields are correctly filled out.");
            }
            
            Student? stud = await _repository.UpdateStudentAsync(id, student);

            if (stud == null)
            {
                return NotFound();
            }

            return Ok(stud);

        }
        catch (InvalidOperationException)
        {
            return BadRequest("The group does not exist");
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        try
        {
            bool deleteSuccessful = await _repository.DeleteStudentAsync(id);
            if (!deleteSuccessful)
            {
                return NotFound();
            }
            
            return NoContent();
            
        }
        catch (InvalidOperationException)
        {
            return Conflict("Cannot delete a student that has associated marks.");
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
    
}