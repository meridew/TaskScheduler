using Microsoft.Win32.TaskScheduler;

namespace ScheduledTaskAPI.Cron
{
    public abstract class BaseCronStrategy : ICronStrategy
    {
        public Trigger ToTrigger(string[] cronParts, DateTime? start, DateTime? end)
        {
            if (!CanHandle(cronParts))
            {
                throw new ArgumentException("Unsupported cron pattern.");
            }

            var nextOccurrence = start ?? DateTime.Now;

            return new TimeTrigger
            {
                StartBoundary = nextOccurrence,
                Repetition = GetRepetitionPattern(cronParts),
                EndBoundary = end ?? default
            };
        }

        public abstract bool CanHandle(string[] cronParts);
        protected abstract RepetitionPattern GetRepetitionPattern(string[] cronParts);
    }
}
