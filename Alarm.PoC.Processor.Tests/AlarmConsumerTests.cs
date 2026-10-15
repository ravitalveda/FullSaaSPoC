namespace Alarm.PoC.Processor.Tests;

public class AlarmConsumerTests
{
    [Theory]
    [InlineData("Smoke", "CRITICAL")]
    [InlineData("Panic", "CRITICAL")]
    [InlineData("Motion", "WARNING")]
    [InlineData("Temperature", "INFO")]
    [InlineData("Unknown", "INFO")]
    [InlineData("", "INFO")]
    [InlineData(null, "INFO")]
    public void ClassifyAlarm_ReturnsExpectedLevel(string? alarmType, string expectedLevel)
    {
        // Arrange
        var consumer = new AlarmConsumer();

        // Act
        var actualLevel = AlarmConsumer.ClassifyAlarm(alarmType);

        // Assert
        Assert.Equal(expectedLevel, actualLevel);
    }


    [Theory]
    [InlineData("smoke", "CRITICAL")]
    [InlineData("PANIC", "CRITICAL")]
    [InlineData("motion", "WARNING")]
    public void ClassifyAlarm_IsCaseInsensitive(
        string alarmType,
        string expectedLevel)
    {
        // Act
        var actualLevel = AlarmConsumer.ClassifyAlarm(alarmType);

        // Assert
        Assert.Equal(expectedLevel, actualLevel);
    }

}
