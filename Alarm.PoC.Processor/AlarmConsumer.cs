using Alarm.PoC.Contracts;
using MassTransit;

namespace Alarm.PoC.Processor;

public class AlarmConsumer : IConsumer<AlarmEvent>
{
    public Task Consume(ConsumeContext<AlarmEvent> context)
    {
        var alarm = context.Message;

        var level = alarm.AlarmType switch
        {
            "Smoke" => "CRITICAL",
            "Panic" => "CRITICAL",
            "Motion" => "WARNING",
            _ => "INFO"
        };

        Console.ForegroundColor = level switch
        {
            "CRITICAL" => ConsoleColor.Red,
            "WARNING" => ConsoleColor.Yellow,
            _ => ConsoleColor.Green
        };

        Console.WriteLine("================================");
        Console.WriteLine("ALARM PROCESSOR");
        Console.WriteLine($"Time      : {DateTime.UtcNow:O}");
        Console.WriteLine($"Type      : {alarm.AlarmType}");
        Console.WriteLine($"Level     : {level}");
        Console.WriteLine($"Alarm Id  : {alarm.EventId}");
        Console.WriteLine("================================");

        Console.ResetColor();

        return Task.CompletedTask;
    }
}