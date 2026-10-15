using Alarm.PoC.AIAnalyzer;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHttpClient();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<AlarmAiConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost");

        cfg.ReceiveEndpoint("alarm-ai", e =>
        {
            e.ConfigureConsumer<AlarmAiConsumer>(context);

            e.UseMessageRetry(r =>
                r.Interval(3, TimeSpan.FromSeconds(5)));
        });
    });
});

var app = builder.Build();

Console.WriteLine("Alarm AI Analyzer started...");

await app.RunAsync();