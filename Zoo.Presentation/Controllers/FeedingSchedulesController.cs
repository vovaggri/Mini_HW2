using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zoo.Application.Interfaces;
using Zoo.Application.Services;
using Zoo.Presentation.DTOs.FeedingSchedule;

namespace Zoo.Presentation.Controllers;

[ApiController]
[Route("api/feeding-schedules")]
public class FeedingSchedulesController : ControllerBase
{
    private readonly IFeedingScheduleRepository _repository;
    private readonly FeedingOrganizationService _service;

    public FeedingSchedulesController(IFeedingScheduleRepository repository, FeedingOrganizationService service)
    {
        _repository = repository;
        _service = service;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FeedingScheduleResultDto>>> GetAll()
    {
        var items = await _repository.GetAllAsync();
        var dtos = items.Select(fs => new FeedingScheduleResultDto(
            fs.Id, fs.AnimalId, fs.FeedingTime, fs.FoodType, fs.IsCompleted
        ));
        return Ok(dtos);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<FeedingScheduleResultDto>> Get(Guid id)
    {
        var fs = await _repository.GetByIdAsync(id);
        if (fs == null) return NotFound();
        var dto = new FeedingScheduleResultDto(
            fs.Id, fs.AnimalId, fs.FeedingTime, fs.FoodType, fs.IsCompleted
        );
        return Ok(dto);
    }

    [HttpPost]
    [ProducesResponseType(typeof(FeedingScheduleResultDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<FeedingScheduleResultDto>> Create([FromBody] CreateFeedingScheduleDto input)
    {
        try
        {
            await _service.ScheduleAsync(input.AnimalId, input.FeedingTime, input.FoodType);
        
            // In memory repositories select last note for answer
            var all = await _repository.GetAllAsync();
            var created = all
                .Where(x => x.AnimalId == input.AnimalId && x.FeedingTime == input.FeedingTime)
                .OrderByDescending(x => x.FeedingTime)
                .First();
        
            var dto = new FeedingScheduleResultDto(created.Id, input.AnimalId, input.FeedingTime, 
                input.FoodType, created.IsCompleted);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, dto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Invalid input",
                Detail = ex.Message,
                Status = StatusCodes.Status400BadRequest,
                Instance = HttpContext.Request.Path
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ProblemDetails
            {
                Title = "Internal Server Error",
                Detail = ex.Message,
                Status = StatusCodes.Status500InternalServerError,
                Instance = HttpContext.Request.Path
            });
        }
    }
    
    [HttpPost("{id}/complete")]
    public async Task<IActionResult> Complete(Guid id)
    {
        await _service.FeedAsync(id);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}