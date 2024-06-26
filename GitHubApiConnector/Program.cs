using GitHubApiConnector.Infrastructure;
using GitHubApiConnector.Infrastructure.Data;
using GitHubApiConnector.Infrastructure.Interfaces;
using GitHubApiConnector.UseCases;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "GitHub API Connector",
        Description = "An ASP.NET Core Web API for fetch, save and list github repositories",
        Contact = new OpenApiContact
        {
            Name = "Marcelo B. Buongermino",
            Url = new Uri("https://www.linkedin.com/in/marcelo-buongermino/"),
            Email = "buon.marcelo@outlook.com",
        },
    });

    options.EnableAnnotations();
});

builder.Services.AddAutoMapper(typeof(MapEntities));

builder.Services.AddInfrastructureModule(builder.Configuration);

builder.Services.AddHttpClient<IGitHubApiClient, GitHubApiClient>();
builder.Services.AddScoped<IFetchAndSaveRepositoriesUseCase, FetchAndSaveRepositoriesUseCase>();
builder.Services.AddScoped<IGetAllGitHubRepositoriesUseCase, GetAllGitHubRepositoriesUseCase>();
builder.Services.AddScoped<IGetGitHubRepositoryByIdUseCase, GetGitHubRepositoryByIdUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Execute migrations when application starts
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<GitHubRepoContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while applying the migrations.");
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
