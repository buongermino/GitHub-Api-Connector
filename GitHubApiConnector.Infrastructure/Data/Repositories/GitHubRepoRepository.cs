﻿using GitHubApiConnector.Domain.Entities;
using GitHubApiConnector.Domain.IRepository;
using GitHubApiConnector.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace GitHubApiConnector.Infrastructure.Data.Repositories;

public class GitHubRepoRepository(GitHubRepoContext context) : IGitHubRepoRepository
{
    private readonly GitHubRepoContext _context = context;

    public async Task SaveAsync(GitHubRepo gitHubRepo)
    {
        await _context.AddAsync(gitHubRepo);
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<List<GitHubRepo>> GetAllAsync()
    {
        return await _context.GitHubRepos
            .Include(p => p.Owner)
            .ToListAsync();
    }

    public async Task<GitHubRepo> GetByIdAsync(Guid id)
    {
        return await _context.GitHubRepos
            .Include(repo => repo.Owner)
            .FirstOrDefaultAsync(repo => repo.Id == id)!;
    }  
}