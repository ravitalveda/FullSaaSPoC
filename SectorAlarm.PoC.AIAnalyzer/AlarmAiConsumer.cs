using MassTransit;
using SectorAlarm.PoC.AIAnalyzer;
using SectorAlarm.PoC.Contracts;
using System.Text;
using System.Text.Json;

namespace Alarm.AIAnalyzer;

public class AlarmAiConsumer : IConsumer<AlarmEvent>
{
    private readonly HttpClient _httpClient;

    public AlarmAiConsumer()
    {
        _httpClient = new HttpClient();
    }

    public async Task Consume(ConsumeContext<AlarmEvent> context)
    {
        var alarm = context.Message;

        var prompt = $"""
            You are an alarm monitoring AI.

            Analyze the following alarm event:

            Alarm Type: {alarm.AlarmType}
            Device Id: {alarm.DeviceId}
            Customer Id: {alarm.CustomerId}

            Return:
            - Risk Level
            - Confidence Percentage
            - Recommendation

            Keep the response under 3 sentences.
            """;

        var request = new
        {
            model = "phi3",
            prompt,
            stream = false
        };

        var json = JsonSerializer.Serialize(request);

        var response = await _httpClient.PostAsync(
            "http://localhost:11434/api/generate",
            new StringContent(
                json,
                Encoding.UTF8,
                "application/json"));

        response.EnsureSuccessStatusCode();

        var resultJson = await response.Content.ReadAsStringAsync();

        var result =
            JsonSerializer.Deserialize<OllamaResponse>(
                resultJson,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

        Console.ForegroundColor = ConsoleColor.Cyan;

        Console.WriteLine();
        Console.WriteLine("===== AI ANALYSIS =====");
        Console.WriteLine($"Alarm : {alarm.AlarmType}");
        Console.WriteLine(result?.Response);
        Console.WriteLine("=======================");
        Console.WriteLine();

        Console.ResetColor();
    }
}
