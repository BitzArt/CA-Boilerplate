namespace BitzArt.CA;

public interface IRepository
{
    Task<int> SaveChangesAsync();
}