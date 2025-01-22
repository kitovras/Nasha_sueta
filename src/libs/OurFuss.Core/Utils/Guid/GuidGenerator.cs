namespace OurFuss.Core.Utils.Guid;

/// <inheritdoc/>
public class GuidGenerator : IGuidGenerator
{
    /// <inheritdoc/>
    public System.Guid Sequential()
    {
        return RT.Comb.Provider.PostgreSql.Create();
    }

    /// <inheritdoc/>
    public System.Guid Random()
    {
        return System.Guid.NewGuid();
    }
}
