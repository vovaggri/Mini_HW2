using Microsoft.AspNetCore.Mvc;
using Zoo.Application.Services;

namespace Zoo.Presentation.Controllers;

[ApiController]
[Route("api/statistics")]
public class StatisticsController : ControllerBase
{
    private readonly ZooStatisticsService _stats;
    public StatisticsController(ZooStatisticsService stats) => _stats = stats;

    [HttpGet]
    public async Task<ActionResult<ZooStatistics>> Get() => Ok(await _stats.GetAsync());
}