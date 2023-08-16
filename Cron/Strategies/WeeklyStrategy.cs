using Microsoft.Win32.TaskScheduler;
using System;

namespace ScheduledTaskAPI.Cron.Strategies
{
    public class WeeklyStrategy : BaseCronStrategy
    {
        public override bool CanHandle(string[] cronParts)
        {
            return cronParts[0] == "0" && cronParts[2] == "*" && cronParts[3] == "*" && IsNumeric(cronParts[4]);
        }

        protected override RepetitionPattern GetRepetitionPattern(string[] cronParts)
        {
            return new RepetitionPattern(TimeSpan.FromDays(7), TimeSpan.FromDays(7));
        }

        private bool IsNumeric(string value)
        {
            return int.TryParse(value, out _);
        }
    }
}
