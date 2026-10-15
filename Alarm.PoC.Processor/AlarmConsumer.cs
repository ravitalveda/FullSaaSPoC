
using Alarm.PoC.Contracts;
using MassTransit;

namespace Alarm.PoC.Processor;

public class AlarmConsumer : IConsumer<AlarmEvent>
{
    public static string ClassifyAlarm(string? alarmType)
    {
        return alarmType?.ToUpperInvariant() switch
        {
            "SMOKE" => "CRITICAL",
            "PANIC" => "CRITICAL",
            "MOTION" => "WARNING",
            _ => "INFO"
        };
    }


    public Task Consume(ConsumeContext<AlarmEvent> context)
    {
        var alarm = context.Message;
        var level = ClassifyAlarm(alarm.AlarmType);

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
