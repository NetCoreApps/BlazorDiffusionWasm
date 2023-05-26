﻿using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;

namespace BlazorDiffusion.ServiceModel;

[Icon(Svg = Icons.Comment)]
public class ArtifactComment : AuditBase
{
    [AutoIncrement]
    public int Id { get; set; }
    public int ArtifactId { get; set; }
    public int? ReplyId { get; set; }
    public string Content { get; set; }
    [Default(0)]
    public int UpVotes { get; set; }
    [Default(0)]
    public int DownVotes { get; set; }
    [Default(0)]
    public int Votes { get; set; }
    public string? FlagReason { get; set; }
    public string? Notes { get; set; }
    public string RefId { get; set; }
    public int AppUserId { get; set; }
}

[Icon(Svg = Icons.Vote)]
[UniqueConstraint(nameof(ArtifactCommentId), nameof(AppUserId))]
public class ArtifactCommentVote
{
    [AutoIncrement]
    public long Id { get; set; }

    [References(typeof(ArtifactComment))]
    public int ArtifactCommentId { get; set; }
    [References(typeof(AppUser))]
    public int AppUserId { get; set; }
    public int Vote { get; set; } // -1 / 1
    public DateTime CreatedDate { get; set; }
}

public class ArtifactCommentReport
{
    [AutoIncrement]
    public long Id { get; set; }

    [References(typeof(ArtifactComment))]
    public int ArtifactCommentId { get; set; }
    [References(typeof(AppUser))]
    public int AppUserId { get; set; }
    public PostReport PostReport { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
}

public enum PostReport
{
    Offensive,
    Spam,
    Nudity,
    Illegal,
    Other,
}

public class CommentResult
{
    public int Id { get; set; }
    public int ArtifactId { get; set; }
    public int? ReplyId { get; set; }
    public string Content { get; set; }
    public int UpVotes { get; set; }
    public int DownVotes { get; set; }
    public int Votes { get; set; }
    public string? FlagReason { get; set; }
    public string? Notes { get; set; }
    public int AppUserId { get; set; }
    public string DisplayName { get; set; }
    public string? Handle { get; set; }
    public string? ProfileUrl { get; set; }
    public string? Avatar { get; set; } //overrides ProfileUrl
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
}


[Tag(Tag.Comments)]
[AutoApply(Behavior.AuditQuery)]
[AutoFilter(QueryTerm.Ensure, nameof(ArtifactComment.FlagReason), Template = SqlTemplate.IsNull)]
public class QueryArtifactComments : QueryDb<ArtifactComment, CommentResult>,
    IJoin<ArtifactComment,AppUser>
{
    public int ArtifactId { get; set; }
}

[Tag(Tag.Comments)]
[ValidateIsAuthenticated]
[AutoApply(Behavior.AuditCreate)]
[AutoPopulate(nameof(ArtifactComment.AppUserId), Eval = "userAuthId.toInt()")]
[AutoPopulate(nameof(ArtifactComment.RefId), Eval = "nguid")]
public class CreateArtifactComment : ICreateDb<ArtifactComment>, IReturn<ArtifactComment>
{
    public int ArtifactId { get; set; }
    public int? ReplyId { get; set; }
    [ValidateLength(1,280)]
    public string Content { get; set; }
}

[Tag(Tag.Comments)]
[ValidateIsAuthenticated]
[AutoApply(Behavior.AuditModify)]
[AutoFilter(QueryTerm.Ensure, nameof(ArtifactComment.AppUserId), Eval = "userAuthId.toInt()")]
public class UpdateArtifactComment : IPatchDb<ArtifactComment>, IReturn<ArtifactComment>
{
    public int Id { get; set; }
    public string? Content { get; set; }
}

[Tag(Tag.Comments)]
[ValidateIsAuthenticated]
[AutoApply(Behavior.AuditSoftDelete)]
[AutoFilter(QueryTerm.Ensure, nameof(ArtifactComment.AppUserId), Eval = "userAuthId.toInt()")]
public class DeleteArtifactComment : IDeleteDb<ArtifactComment>, IReturnVoid
{
    public int Id { get; set; }
}

[Tag(Tag.Comments)]
[ValidateIsAuthenticated]
[AutoFilter(QueryTerm.Ensure, nameof(ArtifactComment.AppUserId), Eval = "userAuthId.toInt()")]
public class QueryArtifactCommentVotes : QueryDb<ArtifactCommentVote>
{
    public int ArtifactId { get; set; }
}

[Tag(Tag.Comments)]
[ValidateIsAuthenticated]
[AutoApply(Behavior.AuditCreate)]
[AutoPopulate(nameof(ArtifactComment.AppUserId), Eval = "userAuthId.toInt()")]
public class CreateArtifactCommentVote : ICreateDb<ArtifactCommentVote>, IReturnVoid
{
    public int ArtifactCommentId { get; set; }
    [ValidateInclusiveBetween(-1, 1)]
    public int Vote { get; set; }
}

[Tag(Tag.Comments)]
[ValidateIsAuthenticated]
[AutoFilter(QueryTerm.Ensure, nameof(ArtifactComment.AppUserId), Eval = "userAuthId.toInt()")]
public class DeleteArtifactCommentVote : IDeleteDb<ArtifactCommentVote>, IReturnVoid
{
    public int ArtifactCommentId { get; set; }
}

[Tag(Tag.Comments)]
[ValidateIsAuthenticated]
[AutoApply(Behavior.AuditCreate)]
[AutoPopulate(nameof(ArtifactComment.AppUserId), Eval = "userAuthId.toInt()")]
public class CreateArtifactCommentReport : ICreateDb<ArtifactCommentReport>, IReturnVoid
{
    public int ArtifactCommentId { get; set; }
    public PostReport PostReport { get; set; }
    public string Description { get; set; }
}

[Tag(Tag.Comments)]
[ValidateIsAuthenticated]
[AutoFilter(QueryTerm.Ensure, nameof(ArtifactComment.AppUserId), Eval = "userAuthId.toInt()")]
public class DeleteArtifactCommentReport : IDeleteDb<ArtifactCommentReport>, IReturnVoid
{
    public int Id { get; set; }
}

