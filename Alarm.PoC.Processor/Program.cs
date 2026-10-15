using Alarm.PoC.Processor;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

ConfigureMessaging(builder.Services);

var app = builder.Build();

Console.WriteLine("Alarm Processor started...");
await app.RunAsync();

static void ConfigureMessaging(IServiceCollection services)
{
    services.AddMassTransit(x =>
    {
        x.AddConsumer<AlarmConsumer>();

        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host("localhost");

            cfg.ReceiveEndpoint("alarm-processor", endpoint =>
            {
                endpoint.ConfigureConsumer<AlarmConsumer>(context);

                endpoint.UseMessageRetry(r =>
                    r.Interval(3, TimeSpan.FromSeconds(5)));
            });
        });
    });
}