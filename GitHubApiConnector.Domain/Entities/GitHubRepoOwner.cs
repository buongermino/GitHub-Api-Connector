﻿using GitHubApiConnector.Domain.SeedWork;
using System.Text.Json.Serialization;

namespace GitHubApiConnector.Domain.Entities;

public class GitHubRepoOwner : Entity
{
    public GitHubRepoOwner() {}

    public string Login { get; set; }
    public string GitHubUserId { get; set; }
    public string Url { get; set; }
    public string AvatarUrl { get; set; }
    public string HtmlUrl { get; set; }
    public string ReposUrl { get; set; }
    public string OrganizationsUrl { get; set; }
    public string Type { get; set; }

    [JsonIgnore]
    public List<GitHubRepo> Repos { get; set; }
}
