using ProductHub.Application.Common.Services;

namespace ProductHub.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}