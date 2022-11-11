using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack;

[assembly: HostingStartup(typeof(BlazorDiffusion.ConfigureUi))]

namespace BlazorDiffusion;

public class ConfigureUi : IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices(services => {
            services.AddSingleton<IComponentRenderer,ComponentRenderer>();
        });
}

public interface IComponentRenderer
{
    Task<string> RenderComponentAsync<T>(HttpContext httpContext);
}

public class ComponentRenderer : IComponentRenderer
{
    public async Task<string> RenderComponentAsync<T>(HttpContext httpContext)
    {
        //var httpContext = ((NetCoreRequest)request).HttpContext;
        var componentTagHelper = new ComponentTagHelper
        {
            ComponentType = typeof(T),
            RenderMode = RenderMode.Static,
            Parameters = new Dictionary<string, object>(), //TODO: Overload and pass in parameters
            ViewContext = new ViewContext { HttpContext = httpContext },
        };

        var tagHelperContext = new TagHelperContext(
            new TagHelperAttributeList(),
            new Dictionary<object, object>(),
            "uniqueid");

        var tagHelperOutput = new TagHelperOutput(
            "tagName",
            new TagHelperAttributeList(),
            (useCachedResult, encoder) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));

        await componentTagHelper.ProcessAsync(tagHelperContext, tagHelperOutput);

        using var stringWriter = new StringWriter();

        tagHelperOutput.Content.WriteTo(stringWriter, HtmlEncoder.Default);

        return stringWriter.ToString();
    }
}
