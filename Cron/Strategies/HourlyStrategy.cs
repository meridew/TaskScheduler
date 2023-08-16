using Microsoft.Win32.TaskScheduler;

namespace ScheduledTaskAPI.Cron.Strategies
{
    public class HourlyStrategy : BaseCronStrategy
    {
        public override bool CanHandle(string[] cronParts)
        {
            return cronParts[1] == "*" && cronParts[2] == "*" && cronParts[3] == "*" && cronParts[4] == "*" && IsNumeric(cronParts[0]);
        }

        protected override RepetitionPattern GetRepetitionPattern(string[] cronParts)
        {
            return new RepetitionPattern(TimeSpan.FromHours(1), TimeSpan.FromHours(1));
        }

        private bool IsNumeric(string value)
        {
            return int.TryParse(value, out _);
        }
    }
}
