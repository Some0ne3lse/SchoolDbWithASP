using Microsoft.AspNetCore.Mvc;
using SchoolDbWithASP.Data.Interface;
using SchoolDbWithASP.Models;
using SchoolDbWithASP.Models.DTO;

namespace SchoolDbWithASP.Data.Controllers;

[Route("api/subjects")]
[Controller]
public class SubjectsController : ControllerBase
{
    private readonly IRepository _repository;

    public SubjectsController(IRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Subject>>> GetAllSubjects()
    {
        try
        {
            List<Subject> subjects = await _repository.GetAllSubjectsAsync();
            return Ok(subjects);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Subject>> GetSubjectById(int id)
    {
        try
        {
            Subject? subj = await _repository.GetSubjectByIdAsync(id);
            if (subj == null)
            {
                return NotFound();
            }

            return Ok(subj);
            
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubject([FromBody] SubjectDto subjectDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input. Please ensure all required fields are correctly filled out. TeacherIds is a list");
            }

            var subject = new Subject
            {
                Title = subjectDto.Title
            };

            await _repository.CreateSubjectAsync(subject, subjectDto.TeacherIds);

            return CreatedAtAction(nameof(GetSubjectById), new { id = subject.Id }, subject);
        }
        catch (InvalidOperationException )
        {
            return BadRequest("One or more teachers don't exist");
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSubject(int id, [FromBody] SubjectDto subjectDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input. Please ensure all required fields are correctly filled out. TeacherIds is a list");
            }

            Subject? updatedSubject = await _repository.UpdateSubjectAsync(id, subjectDto);

            if (updatedSubject == null)
            {
                return NotFound();
            }

            return Ok(updatedSubject);
        }
        catch (InvalidOperationException)
        {
            return BadRequest("One or more specified teachers do not exist.");
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubject(int id)
    {
        try
        {
            bool deleteSuccessful = await _repository.DeleteSubjectAsync(id);
        
            if (!deleteSuccessful)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return Conflict("Cannot delete a subject that has associated marks.");
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
}