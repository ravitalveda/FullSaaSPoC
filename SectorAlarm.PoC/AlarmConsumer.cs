using MassTransit;
using SectorAlarm.PoC.Contracts;

public class AlarmConsumer
    : IConsumer<AlarmEvent>
{
    public Task Consume(ConsumeContext<AlarmEvent> context)
    {
        var severity = context.Message.AlarmType switch
                        {
                            "Smoke" => "CRITICAL",
                            "Panic" => "CRITICAL",
                            "Motion" => "WARNING",
                            _ => "INFO"
                        };

        Console.WriteLine($"[{severity}] {context.Message.AlarmType}");

        return Task.CompletedTask;
    }
}