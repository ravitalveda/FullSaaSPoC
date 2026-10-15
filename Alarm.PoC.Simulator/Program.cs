using MassTransit;
using Alarm.PoC.Contracts;

var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
{
    cfg.Host("localhost");
});

await bus.StartAsync();

Console.WriteLine("Alarm Simulator Started");

var alarms = new[]
{
    "Smoke",
    "Motion",
    "DoorOpen",
    "Panic"
};

while (true)
{
    var evt = new AlarmEvent
    {
        EventId = Guid.NewGuid(),
        DeviceId = $"D-{Random.Shared.Next(100)}",
        CustomerId = $"C-{Random.Shared.Next(20)}",
        AlarmType = alarms[Random.Shared.Next(alarms.Length)],
        CreatedUtc = DateTime.UtcNow
    };

    await bus.Publish(evt);

    Console.WriteLine(
        $"Published : {evt.AlarmType}");

    await Task.Delay(3000);
}