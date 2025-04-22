using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Zoo.Application.Interfaces;
using Zoo.Domain.Entities;
using Zoo.Presentation.DTOs.Enclosure;

namespace Zoo.Presentation.Controllers;

[ApiController]
[Route("api/enclosures")]
public class EnclosuresController : ControllerBase
{
    private readonly IEnclosureRepository _repository;
    public EnclosuresController(IEnclosureRepository repository) => _repository = repository;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EnclosureResultDto>>> GetAll()
    {
        var items = await _repository.GetAllAsync();
        var dtos = items.Select(e => new EnclosureResultDto(
            e.Id, e.Type, e.Size, e.CurrentAnimalCount, e.MaxCapacity
        ));
        return Ok(dtos);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<EnclosureResultDto>> Get(Guid id)
    {
        var e = await _repository.GetByIdAsync(id);
        if (e == null) return NotFound();
        var dto = new EnclosureResultDto(
            e.Id, e.Type, e.Size, e.CurrentAnimalCount, e.MaxCapacity
        );
        return Ok(dto);
    }

    [HttpPost]
    [ProducesResponseType(typeof(EnclosureResultDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<EnclosureResultDto>> Create([FromBody] CreateEnclosureDto input)
    {
        try
        {
            var e = new Enclosure(Guid.NewGuid(), input.Type, input.Size, input.MaxCapacity);
            await _repository.AddAsync(e);

            var dto = new EnclosureResultDto(e.Id, e.Type, e.Size, e.CurrentAnimalCount, e.MaxCapacity);
            return CreatedAtAction(nameof(Get), new { id = e.Id }, dto);
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
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}