using AutoMapper;
using GitHubApiConnector.UseCases;

namespace GitHubApiConnector.UnitTests;

internal static class MapEntitiesHelper
{
    public static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MapEntities>();
        });
        return config.CreateMapper();
    }
}
