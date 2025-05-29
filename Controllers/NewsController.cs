using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class NewsController : ControllerBase
{
    private readonly NewsApiService _newsApiService;

    public NewsController(NewsApiService newsApiService)
    {
        _newsApiService = newsApiService;
    }

    [HttpGet("headlines")]
    public async Task<ActionResult<List<NewsArticle>>> GetHeadlines()
    {
        var headlines = await _newsApiService.GetHeadlinesAsync();
        return Ok(headlines);
    }
}