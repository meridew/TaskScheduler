using Microsoft.Win32.TaskScheduler;

namespace ScheduledTaskAPI.Cron.Strategies
{
    public class EveryXHoursStrategy : BaseCronStrategy
    {
        public override bool CanHandle(string[] cronParts)
        {
            return cronParts[2] == "*" && cronParts[3] == "*" && cronParts[4] == "*" && IsNumeric(cronParts[0]) && cronParts[1].Contains("/");
        }

        protected override RepetitionPattern GetRepetitionPattern(string[] cronParts)
        {
            var hours = int.Parse(cronParts[1].Split('/')[1]);
            return new RepetitionPattern(TimeSpan.FromHours(hours), TimeSpan.FromHours(hours));
        }

        private bool IsNumeric(string value)
        {
            return int.TryParse(value, out _);
        }
    }
}
