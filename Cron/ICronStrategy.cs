using Microsoft.Win32.TaskScheduler;

namespace ScheduledTaskAPI.Cron
{
    public interface ICronStrategy
    {
        bool CanHandle(string[] cronParts);
        Trigger ToTrigger(string[] cronParts, DateTime? start, DateTime? end);
    }
}
