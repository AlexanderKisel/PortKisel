using Xunit;

namespace PortKisel.Api.Tests.Infrastructure
{
    [CollectionDefinition(nameof(PortApiTestCollection))]
    public class PortApiTestCollection
        : ICollectionFixture<PortApiFixture>
    {
    }
}
