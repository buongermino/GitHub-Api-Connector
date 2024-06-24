namespace GitHubApiConnector.Domain.SeedWork;

public interface IRepository<T>
{
    public IUnitOfWork UnitOfWork { get; }
}
