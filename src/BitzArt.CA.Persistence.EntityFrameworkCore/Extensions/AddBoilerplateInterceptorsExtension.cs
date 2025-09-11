using Microsoft.EntityFrameworkCore;

namespace BitzArt.CA.Persistence;

/// <summary>
/// CA-Boilerplate <see cref="DbContextOptionsBuilder"/> extensions.
/// </summary>
public static class AddBoilerplateInterceptorsExtension
{
    /// <summary>
    /// Applies necessary CA-Boilerplate interceptors to the <see cref="DbContextOptionsBuilder"/>.
    /// </summary>
    /// <param name="optionsBuilder"></param>
    /// <returns></returns>
    public static DbContextOptionsBuilder AddBoilerplateInterceptors(this DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.AddInterceptors(
            [
                new DeletablesInterceptor(),
                new AuditablesInterceptor()
            ]);
}
