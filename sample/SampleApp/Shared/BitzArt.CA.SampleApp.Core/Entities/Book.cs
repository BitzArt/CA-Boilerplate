namespace BitzArt.CA.SampleApp.Core;

public class Book : IEntity<int?>
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
}
