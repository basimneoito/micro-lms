using Microsoft.AspNetCore.Mvc;

namespace Tenant.Controllers;

[ApiController]
[Route("[controller]")]
public class TenantControllerController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<TenantControllerController> _logger;

    public TenantControllerController(ILogger<TenantControllerController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetTenantController")]
    public IEnumerable<TenantController> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new TenantController
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
