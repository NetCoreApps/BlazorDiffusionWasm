﻿using BlazorDiffusion.ServiceModel;
using BlazorDiffusion.Shared;
using BlazorDiffusion.UI;
using Ljbc1994.Blazor.IntersectionObserver;
using Microsoft.AspNetCore.Components;

namespace BlazorDiffusion.Pages;

public partial class Albums : AppAuthComponentBase
{
    [Inject] IIntersectionObserverService ObserverService { get; set; } = default!;
    [Inject] ILogger<Favorites> Log { get; set; } = default!;

    ApiResult<GetAlbumIdsResponse> api = new();
    List<AlbumResult> results = new();
    public ElementReference BottomElement { get; set; }
    IntersectionObserver? bottomObserver;

    bool hasMore;

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        api = await ApiAsync(new GetAlbumIds());
        if (api.Succeeded)
        {
            results = await UserState.GetAlbumsByIdsAsync(api.Response!.Results.Take(UserState.InitialTake));
            hasMore = results.Count >= UserState.InitialTake;
        }
    }
    async Task SaveAppPrefsAsync()
    {
        await UserState.SaveAppPrefsAsync();
        StateHasChanged();
    }

    async Task fetchResults(int count)
    {
        var nextResults = await UserState.GetAlbumsByIdsAsync(api.Response!.Results.Take(count));
        hasMore = nextResults.Count >= count;
        setResults(nextResults);
    }

    void setResults(List<AlbumResult> results)
    {
        this.results = results;
        StateHasChanged();
    }

    async Task loadMore()
    {
        log("Albums loadMore({0}) {1}...", hasMore, results.Count + UserState.NextPage);
        if (hasMore)
        {
            await fetchResults(results.Count + UserState.NextPage);
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await SetupObserver();
        }
    }

    public async Task SetupObserver()
    {
        try
        {
            bottomObserver = await ObserverService.Observe(BottomElement, async (entries) =>
            {
                var entry = entries.FirstOrDefault();
                if (entry?.IsIntersecting == true)
                {
                    await loadMore();
                }
                StateHasChanged();
            });
        }
        catch (Exception e)
        {
            // throws on initial load
            Log.LogError("Albums ObserverService.Observe(BottomElement): {0}", e.ToString());
        }
    }

}
