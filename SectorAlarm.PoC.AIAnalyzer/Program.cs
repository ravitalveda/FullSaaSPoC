using Alarm.AIAnalyzer;
using MassTransit;
using Microsoft.Extensions.Hosting;


namespace SectorAlarm.PoC.AIAnalyzer;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args);

        builder.ConfigureServices(services =>
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<AlarmAiConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost");

                    cfg.ReceiveEndpoint("alarm-ai", e =>
                    {
                        e.ConfigureConsumer<AlarmAiConsumer>(context);
                    });
                });
            });
        });

        var host = builder.Build();

        await host.RunAsync();
    }
}