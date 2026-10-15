using Alarm.PoC.Contracts;
using MassTransit;

var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
{
    cfg.Host("localhost");
});

await bus.StartAsync();

Console.WriteLine("Alarm Simulator Started");
Console.WriteLine("Testing yaml file");

var alarms = new[]
{
    "Smoke",
    "Motion",
    "DoorOpen",
    "Panic"
};

while (true)
{
    Console.WriteLine();
    Console.WriteLine("1 - Smoke");
    Console.WriteLine("2 - Motion");
    Console.WriteLine("3 - DoorOpen");
    Console.WriteLine("4 - Panic");
    Console.WriteLine("Q - Quit");

    var key = Console.ReadLine();

    var alarmType = key switch
    {
        "1" => "Smoke",
        "2" => "Motion",
        "3" => "DoorOpen",
        "4" => "Panic",
        _ => null
    };

    if (string.IsNullOrEmpty(alarmType))
        break;

    await bus.Publish(new AlarmEvent
    {
        EventId = Guid.NewGuid(),
        DeviceId = $"D-{Random.Shared.Next(100)}",
        CustomerId = $"C-{Random.Shared.Next(20)}",
        AlarmType = alarmType,
        CreatedUtc = DateTime.UtcNow
    });

    Console.WriteLine($"Published: {alarmType}");
}

Console.WriteLine();
Console.WriteLine("All test alarms published.");
Console.WriteLine("Press any key to exit.");

Console.ReadKey();

await bus.StopAsync();