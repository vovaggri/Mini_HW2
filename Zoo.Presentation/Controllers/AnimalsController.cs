using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zoo.Application.Interfaces;
using Zoo.Application.Services;
using Zoo.Domain.Entities;
using Zoo.Presentation.DTOs.Animals;
using Zoo.Presentation.DTOs.FeedingSchedule;

namespace Zoo.Presentation.Controllers;

[ApiController]
[Route("api/animals")]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalRepository _repository;
    private readonly AnimalTransferService _transfer;
    private readonly FeedingOrganizationService _feeding;
    
    public AnimalsController(
        IAnimalRepository repository,
        AnimalTransferService transfer,
        FeedingOrganizationService feeding)
    {
        _repository = repository;
        _transfer = transfer;
        _feeding = feeding;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AnimalResultDto>>> GetAll()
    {
        var animals = await _repository.GetAllAsync();
        var dtos = animals.Select(a => new AnimalResultDto(
            a.Id, a.Species, a.Name, a.BirthDate, a.Gender,
            a.FavoriteFood, a.Status, a.EnclosureId
        ));
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AnimalResultDto>> Get(Guid id)
    {
        var a = await _repository.GetByIdAsync(id);
        if (a == null) return NotFound();
        var dto = new AnimalResultDto(
            a.Id, a.Species, a.Name, a.BirthDate, a.Gender,
            a.FavoriteFood, a.Status, a.EnclosureId
        );
        return Ok(dto);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(AnimalResultDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AnimalResultDto>> Create([FromBody] CreateAnimalDto input)
    {
        try
        {
            var animal = new Animal(
                Guid.NewGuid(),
                input.Species,
                input.Gender,
                input.Name,
                input.BirthDate,
                input.FavoriteFood
            );
            await _repository.AddAsync(animal);

            var dto = new AnimalResultDto(
                animal.Id, animal.Species, animal.Name, animal.BirthDate,
                animal.Gender, animal.FavoriteFood, animal.Status, animal.EnclosureId
            );
            return CreatedAtAction(nameof(Get), new { id = animal.Id }, dto);
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
    
    [HttpPost("{id}/transfer/{enclosureId}")]
    public async Task<IActionResult> Transfer(Guid id, Guid enclosureId)
    {
        await _transfer.TransferAsync(id, enclosureId);
        return NoContent();
    }
    
    [HttpPost("{id}/feed")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Feed(
        Guid id,
        [FromBody] FeedAnimalDto dto)
    {
        var animal = await _repository.GetByIdAsync(id);
        if (animal is null)
            return NotFound(new ProblemDetails {
                Title  = "Animal not found",
                Detail = $"Animal with id = {id} dous not exist",
                Status = StatusCodes.Status404NotFound,
                Instance = HttpContext.Request.Path
            });
        
        try
        {
            await _feeding.ScheduleAsync(
                animal.Id,
                dto.FeedingTime,
                dto.FoodType
            );
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ProblemDetails {
                Title  = "Invalid input",
                Detail = ex.Message,
                Status = StatusCodes.Status400BadRequest,
                Instance = HttpContext.Request.Path
            });
        }
    }
}