namespace OurFuss.Core.Utils.Guid;

/// <summary>
/// Guid generator
/// </summary>
public interface IGuidGenerator
{
    /// <summary>
    /// Consistent
    /// </summary>
    /// <returns>Guid</returns>
    public System.Guid Sequential();

    /// <summary>
    /// Random
    /// </summary>
    /// <returns>Guid</returns>
    public System.Guid Random();
}
