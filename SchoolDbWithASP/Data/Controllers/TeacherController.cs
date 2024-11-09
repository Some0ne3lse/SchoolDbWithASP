using Microsoft.AspNetCore.Mvc;
using SchoolDbWithASP.Data.Interface;
using SchoolDbWithASP.Models;
using SchoolDbWithASP.Models.DTO;

namespace SchoolDbWithASP.Data.Controllers;

[Route("api/teachers")]
[Controller]
public class TeacherController : ControllerBase
{
    private readonly IRepository _repository;

    public TeacherController(IRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Teacher>>> GetAllTeachers()
    {
        try
        {
            List<Teacher> teachers = await _repository.GetAllTeachersAsync();
            return Ok(teachers);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Teacher>> GetTeacherById(int id)
    {
        try
        {
            Teacher? teach = await _repository.GetTeacherByIdAsync(id);
            if (teach == null)
            {
                return NotFound();
            }

            return Ok(teach);

        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTeacher([FromBody] TeacherDto teacherDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input. Please ensure all required fields are correctly filled out. SubjectIds is a list.");
            }

            var teacher = new Teacher
            {
                FirstName = teacherDto.FirstName,
                LastName = teacherDto.LastName
            };

            await _repository.CreateTeacherAsync(teacher, teacherDto.SubjectIds);

            return CreatedAtAction(nameof(GetTeacherById), new { id = teacher.Id }, teacher);
        }
        catch (InvalidOperationException)
        {
            return BadRequest("One or more subjects don't exist.");
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTeacher(int id, [FromBody] TeacherDto teacherDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input. Please ensure all required fields are correctly filled out. SubjectIds is a list.");
            }

            Teacher? updatedTeacher = await _repository.UpdateTeacherAsync(id, teacherDto);

            if (updatedTeacher == null)
            {
                return NotFound();
            }

            return Ok(updatedTeacher);
        }
        catch (InvalidOperationException)
        {
            return BadRequest("One or more specified subjects do not exist.");
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeacher(int id)
    {
        try
        {
            bool deleteSuccessful = await _repository.DeleteTeacherAsync(id);

            if (!deleteSuccessful)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return Conflict("Cannot delete a teacher who has associated subjects.");
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
}
