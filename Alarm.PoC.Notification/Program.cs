using Alarm.PoC.Notification;
using MassTransit;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<NotificationConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost");

        cfg.ReceiveEndpoint("alarm-notification", e =>
        {
            e.ConfigureConsumer<NotificationConsumer>(context);
        });
    });
});

var app = builder.Build();

Console.WriteLine("Alarm Notification started...");

await app.RunAsync();