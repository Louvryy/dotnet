using Microsoft.AspNetCore.Mvc;

namespace Louvryy.Core.Tests.MinimalApi.Controllers;

/// <summary>
/// IndexController
/// </summary>
[ApiController]
[Route("/")]
public class IndexController : ControllerBase
{
    private readonly ILogger<IndexController> _logger;

    /// <summary>
    /// IndexController
    /// </summary>
    /// <param name="logger"></param>
    public IndexController(ILogger<IndexController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Get
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public string Get()
    {
        return "Hello world";
    }
}
