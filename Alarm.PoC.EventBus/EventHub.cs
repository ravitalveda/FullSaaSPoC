using Alarm.PoC.Contracts;

namespace Alarm.PoC.EventBus
{
    public static class EventHub
    {
        public static event Action<AlarmEvent>? AlarmRaised;

        public static void Publish(AlarmEvent alarm)
        {
            AlarmRaised?.Invoke(alarm);
        }
    }
}
