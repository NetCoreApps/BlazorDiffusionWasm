using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceStack.Host.NetCore;
using ServiceStack.Web;

namespace BlazorDiffusion.Pages;

public class RenderModel : PageModel
{
    private readonly ILogger<RenderModel> _logger;

    public string HtmlBody { get; set; } = "<blank>";

    public RenderModel(ILogger<RenderModel> logger)
    {
        _logger = logger;
    }

    public async Task OnGet()
    {
        var renderer = new ComponentRenderer();
        HtmlBody = await renderer.RenderComponentAsync<Pages.Index>(HttpContext);
    }
}
