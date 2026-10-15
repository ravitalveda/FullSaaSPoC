using Alarm.PoC.Contracts;
using MassTransit;
using System.Net.Http.Json;

namespace Alarm.PoC.AIAnalyzer;

public sealed class AlarmAiConsumer : IConsumer<AlarmEvent>
{
    private readonly HttpClient _httpClient;

    public AlarmAiConsumer(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task Consume(ConsumeContext<AlarmEvent> context)
    {
        try
        {
            var alarm = context.Message;

            var prompt = $"""
                You are a Sector Alarm risk assessment assistant.

                Alarm Type: {alarm.AlarmType}
                Device Id: {alarm.DeviceId}
                Customer Id: {alarm.CustomerId}

                Respond in this format:

                Risk Level:
                Confidence:
                Recommendation:
                """;

            var request = new
            {
                model = "phi3",
                prompt,
                stream = false
            };

            var response = await _httpClient.PostAsJsonAsync(
                "http://localhost:11434/api/generate",
                request);

            response.EnsureSuccessStatusCode();

            var result =
                await response.Content.ReadFromJsonAsync<OllamaResponse>();

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine();
            Console.WriteLine("===== AI ANALYSIS =====");
            Console.WriteLine($"Alarm Type : {alarm.AlarmType}");
            Console.WriteLine($"Device Id  : {alarm.DeviceId}");
            Console.WriteLine($"CustomerId : {alarm.CustomerId}");
            Console.WriteLine();
            Console.WriteLine(result?.Response);
            Console.WriteLine("=======================");
            Console.WriteLine();

            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"AI Analysis Failed: {ex.Message}");
            Console.ResetColor();
        }
    }
}