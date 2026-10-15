using MassTransit;
using Alarm.PoC.Contracts;
using Alarm.PoC.EventBus;

Console.WriteLine("Notification Started");

EventHub.AlarmRaised += alarm =>
{
    Console.ForegroundColor = ConsoleColor.Green;

    Console.WriteLine(
        $"SMS => Customer {alarm.CustomerId} : {alarm.AlarmType}");

    Console.ResetColor();
};

Console.ReadLine();