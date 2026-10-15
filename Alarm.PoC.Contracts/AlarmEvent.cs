namespace Alarm.PoC.Contracts;

public record AlarmEvent
{
    public Guid EventId { get; init; }

    public required string DeviceId { get; init; }

    public required string CustomerId { get; init; }

    public required string AlarmType { get; init; }

    public DateTime CreatedUtc { get; init; }
}
