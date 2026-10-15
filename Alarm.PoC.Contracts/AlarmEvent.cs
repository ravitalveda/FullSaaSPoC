namespace Alarm.PoC.Contracts;

public record AlarmEvent
{
    public Guid EventId { get; init; }

    public string DeviceId { get; init; }

    public string CustomerId { get; init; }

    public string AlarmType { get; init; }

    public DateTime CreatedUtc { get; init; }
}
