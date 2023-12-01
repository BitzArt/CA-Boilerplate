namespace MMAS.AS;

internal static class SqlServerConnectionStringUtility
{
    internal static string SetDatabaseName(this string connectionString, string name)
    {
        var parameterStrings = connectionString.Split(';');
        var parameters = new Dictionary<string, string>(parameterStrings
            .Select(x =>
            {
                var parts = x.Split('=');
                return new KeyValuePair<string, string>(parts[0], parts[1]);
            }))
        {
            ["Database"] = name
        };
        return string.Join(';', parameters.Select(x => $"{x.Key}={x.Value}"));
    }
}
