using Microsoft.AspNetCore.Mvc;
using SchoolDbWithASP.Data.Interface;
using SchoolDbWithASP.Models;

namespace SchoolDbWithASP.Data.Controllers;

[Route("api/groups")]
[Controller]
public class GroupsController : ControllerBase
{
    private readonly IRepository _repository;

    public GroupsController(IRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Group>>> GetAllGroups()
    {
        try
        {
            List<Group> groups = await _repository.GetAllGroupsAsync();
            return Ok(groups);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
    
    
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Group>> GetGroupById(int id)
    {
        try
        {
            Group? theGroup = await _repository.GetGroupByIdAsync(id);
            if (theGroup == null)
            {
                return NotFound();
            }
            
            return Ok(theGroup);
            
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }


    [HttpPost]
    public async Task<IActionResult> CreateGroup([FromBody] Group group)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input. Please ensure all required fields are correctly filled out.");
            }
            
            await _repository.CreateGroupAsync(group);
            return CreatedAtAction(nameof(GetGroupById), new { id = group.Id }, group);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<Group>> UpdateGroup(int id, [FromBody] Group group)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input. Please ensure all required fields are correctly filled out.");
            }
            
            Group? theGroup = await _repository.UpdateGroupAsync(id, group);

            if (theGroup == null)
            {
                return NotFound();
            }

            return Ok(theGroup);

        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteGroup(int id)
    {
        try
        {
            bool deleteSuccessful = await _repository.DeleteGroupAsync(id);
            if (!deleteSuccessful)
            {
                return NotFound();
            }

            return NoContent();

        }
        catch (InvalidOperationException)
        {
            return Conflict("Cannot delete this group because it has associated students.");
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
}