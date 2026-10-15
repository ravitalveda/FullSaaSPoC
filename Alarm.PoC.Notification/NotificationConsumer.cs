using Alarm.PoC.Contracts;
using MassTransit;

namespace Alarm.PoC.Notification;

public class NotificationConsumer : IConsumer<AlarmEvent>
{
    public Task Consume(ConsumeContext<AlarmEvent> context)
    {
        var alarm = context.Message;

        Console.ForegroundColor = ConsoleColor.Yellow;

        var notification = alarm.AlarmType switch
        {
            "Smoke" =>
                "🚨 SMS + Email sent to customer",

            "Panic" =>
                "🆘 Emergency escalation sent",

            "Motion" =>
                "📱 Mobile push notification sent",

            "DoorOpen" =>
                "🚪 Door activity notification sent",

            _ =>
                "ℹ️ General notification sent"
        };

        Console.WriteLine();
        Console.WriteLine("===== NOTIFICATION =====");
        Console.WriteLine($"Customer  : {alarm.CustomerId}");
        Console.WriteLine($"Device    : {alarm.DeviceId}");
        Console.WriteLine($"AlarmType : {alarm.AlarmType}");
        Console.WriteLine(notification);
        Console.WriteLine("========================");
        Console.WriteLine();

        Console.ResetColor();

        return Task.CompletedTask;
    }
}