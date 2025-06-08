namespace BitzArt.Pagination;

/// <summary>
/// Extension methods for <see cref="PageRequest"/>.
/// </summary>
public static class PageRequestExtensions
{
    /// <summary>
    /// Ensures that the limit of the page request does not exceed the specified maximum value.
    /// </summary>
    /// <param name="pageRequest"><see cref="PageRequest"/> to check.</param>
    /// <param name="max">Maximum limit allowed. Default value is 100.</param>
    public static void CheckLimit(this PageRequest pageRequest, int max = 100)
    {
        if (pageRequest.Limit > max) pageRequest.Limit = max;
    }
}
