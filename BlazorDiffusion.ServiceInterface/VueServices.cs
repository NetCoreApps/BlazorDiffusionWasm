﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Legacy;
using BlazorDiffusion.ServiceModel;

namespace BlazorDiffusion.ServiceInterface;

public class VueServices : Service
{
    public IAutoQueryDb AutoQuery { get; set; } = default!;
    public ICrudEvents CrudEvents { get; set; }

    //public async Task<object> Any(QueryArtifactComments query)
    //{
    //    using var db = AutoQuery.GetDb(query, base.Request);
    //    var q = AutoQuery.CreateQuery(query, base.Request, db);
    //    var response = await AutoQuery.ExecuteAsync(query, q, base.Request, db);
    //    return response;
    //}
    
    public async Task<object> Get(GetArtifactUserData request)
    {
        var session = await SessionAsAsync<CustomUserSession>();
        var userId = session.GetUserId();
        var votes = await Db.SelectAsync(Db.From<ArtifactCommentVote>().Join<ArtifactComment>()
            .Where(x => x.AppUserId == userId)
            .And<ArtifactComment>(x => x.ArtifactId == request.ArtifactId));
        var liked = await Db.ExistsAsync<ArtifactLike>(x => x.ArtifactId == request.ArtifactId && x.AppUserId == userId);

        var ret = new GetArtifactUserDataResponse
        {
            ArtifactId = request.ArtifactId,
            Liked = liked,
            UpVoted = votes.Where(x => x.Vote > 0).Map(x => x.ArtifactCommentId),
            DownVoted = votes.Where(x => x.Vote < 0).Map(x => x.ArtifactCommentId),
        };
        return ret;
    }

    public async Task<object> Get(GetAlbumUserData request)
    {
        var session = await SessionAsAsync<CustomUserSession>();
        var userId = session.GetUserId();
        var likedIds = await Db.ColumnAsync<int>(Db.From<AlbumArtifact>()
            .Join<ArtifactLike>((a,l) => a.ArtifactId == l.ArtifactId && l.AppUserId == userId)
            .Where<AlbumArtifact>(x => x.AlbumId == request.AlbumId)
            .Select(x => x.ArtifactId));
        return new GetAlbumUserDataResponse
        {
            AlbumId = request.AlbumId,
            LikedArtifacts = likedIds,
        };
    }

    async Task refreshVotes(int artifactCommentId)
    {
        var commentVotes = await Db.SelectAsync<ArtifactCommentVote>(x => x.ArtifactCommentId == artifactCommentId);
        var upVotes = commentVotes.Count(x => x.Vote > 0);
        var downVotes = commentVotes.Count(x => x.Vote < 0);
        var votes = upVotes - downVotes;
        await Db.UpdateOnlyAsync(() => new ArtifactComment
        {
            UpVotes = upVotes,
            DownVotes = downVotes,
            Votes = votes,
        }, where: x => x.Id == artifactCommentId);
    }

    public async Task Post(CreateArtifactCommentVote request)
    {
        await AutoQuery.CreateAsync(request, base.Request);
        await refreshVotes(request.ArtifactCommentId);
    }

    public async Task Delete(DeleteArtifactCommentVote request)
    {
        await AutoQuery.DeleteAsync(request, base.Request);
        await refreshVotes(request.ArtifactCommentId);
    }

}
