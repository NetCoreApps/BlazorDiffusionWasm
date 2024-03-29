﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BlazorDiffusion.UI;
using BlazorDiffusion.ServiceModel;
using ServiceStack.Logging;

namespace BlazorDiffusion.Pages.ssg;

public class LatestModel : PageModel, IGetPageModel
{
    [FromQuery] public new int? Page { get; set; }

    public int UsePage;
    public int Total;
    public int Pages;
    public const int GridColumns = 4;
    public GalleryResults GalleryResults = new();

    public async Task OnGetAsync()
    {
        var log = LogManager.GetLogger(GetType());
        UsePage = Math.Max(Page ?? 1, 1);
        var Gateway = HostContext.AppHost.GetServiceGateway();

        List<Artifact> results = new();
        HashSet<int> resultIds = new();
        bool hasMore;

        GalleryResults = new() { GridColumns = GridColumns };

        var request = new SearchArtifacts
        {
            Skip = (UsePage - 1) * UserState.StaticPagedTake,
            Take = UserState.StaticTake,
            Include = "total",
            Show = "latest"
        };

        var api = await Gateway.ApiAsync(request);
        clearResults();

        if (api.Succeeded)
        {
            if (api.Response?.Results != null)
            {
                addResults(api.Response.Results);
            }
        }

        void setResults(IEnumerable<Artifact> newResults)
        {
            results = newResults.ToList();
            GalleryResults = X.Apply(GalleryResults.Clone(), x => x.Artifacts = results.ShuffleGridArtifacts(GridColumns).ToList());
        }

        void clearResults()
        {
            results.Clear();
            resultIds.Clear();
        }

        void addResults(List<ArtifactResult> artifacts, bool reset = false)
        {
            if (reset)
                clearResults();

            var missingPrompts = new List<int>();

            hasMore = artifacts.Count >= request.Take;
            foreach (var artifact in artifacts)
            {
                if (resultIds.Contains(artifact.Id))
                    continue;

                if (string.IsNullOrEmpty(artifact.Prompt) && string.IsNullOrEmpty(artifact.UserPrompt))
                {
                    missingPrompts.Add(artifact.Id);
                    continue;
                }

                resultIds.Add(artifact.Id);
                results.Add(artifact);
            }

            if (missingPrompts.Count > 0) 
            { 
                log.ErrorFormat("Artifacts with missing prompts: {0}", string.Join(", ", missingPrompts));
            }

            setResults(results);
        }

        Total = api.Response?.Total ?? 0;
        Pages = (int)Math.Ceiling(Total / (double)UserState.StaticPagedTake);
    }
}
