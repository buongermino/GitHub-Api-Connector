using System.Text.Json.Serialization;

namespace GitHubApiConnector.Domain.SeedWork;

public abstract class Entity
{
    private Guid _id;

    [JsonInclude]
    public virtual Guid Id
    {
        get { return _id; }
        protected set { _id = value; }
    }

    protected Entity()
    {
        _id = Guid.NewGuid();
    }
}
