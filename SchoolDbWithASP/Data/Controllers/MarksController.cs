using Microsoft.AspNetCore.Mvc;
using SchoolDbWithASP.Data.Interface;
using SchoolDbWithASP.Models;

namespace SchoolDbWithASP.Data.Controllers;

[Route("api/marks")]
[Controller]
public class MarksController : ControllerBase
{
    private readonly IRepository _repository;

    public MarksController(IRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Mark>>> GetAllMarks()
    {
        try
        {
            List<Mark> marks = await _repository.GetAllMarksAsync();
            return Ok(marks);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Mark>> GetMarkById(int id)
    {
        try
        {
            Mark? theMark = await _repository.GetMarkByIdAsync(id);
            if (theMark == null)
            {
                return NotFound();
            }

            return Ok(theMark);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateMark([FromBody] Mark mark)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.CreateMarkAsync(mark);
            return CreatedAtAction(nameof(GetMarkById), new { id = mark.Id }, mark);
        }
        catch(InvalidOperationException)
        {
            return BadRequest("The student or subject doesn't exist");
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
    
    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<Mark>> UpdateMark(int id, [FromBody] Mark mark)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            Mark? theMark = await _repository.UpdateMarkAsync(id, mark);

            if (theMark == null)
            {
                return NotFound();
            }

            return Ok(theMark);

        }
        catch (InvalidOperationException)
        {
            return BadRequest("The student or subject does not exist");
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
    
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteMark(int id)
    {
        try
        {
            bool deleteSuccessful = await _repository.DeleteMarkAsync(id);
            if (!deleteSuccessful)
            {
                return NotFound();
            }
            
            return NoContent();
            
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
    
}