using MassTransit;
using Alarm.PoC.Contracts;
using Alarm.PoC.EventBus;

public class NotificationConsumer : IConsumer<AlarmEvent>
{
    public Task Consume(
        ConsumeContext<AlarmEvent> context)
    {
        Console.ForegroundColor =
            ConsoleColor.Green;

        Console.WriteLine(
            $"SMS SENT -> {context.Message.CustomerId}");

        Console.ResetColor();

        return Task.CompletedTask;
    }
}