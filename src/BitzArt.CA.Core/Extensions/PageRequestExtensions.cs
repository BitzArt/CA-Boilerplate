namespace BitzArt.Pagination;

public static class PageRequestExtensions
{
    public static void CheckLimit(this PageRequest pageRequest, int max = 100)
    {
        if (pageRequest.Limit > max) pageRequest.Limit = max;
    }
}
