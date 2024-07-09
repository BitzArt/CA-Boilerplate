using BitzArt.CA.SampleApp.Core;

namespace BitzArt.CA.SampleApp.Infrastructure;

internal class TimeService : ITimeService
{
    public DateTime GetCurrentTime()
    {
        return DateTime.Now;
    }
}
