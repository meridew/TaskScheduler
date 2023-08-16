using Microsoft.Win32.TaskScheduler;
using System;

namespace ScheduledTaskAPI.Cron.Strategies
{
    public class MonthlyStrategy : BaseCronStrategy
    {
        public override bool CanHandle(string[] cronParts)
        {
            return cronParts[0] == "0" && cronParts[3] == "*" && cronParts[4] == "*" && IsNumeric(cronParts[2]);
        }

        protected override RepetitionPattern GetRepetitionPattern(string[] cronParts)
        {
            // Here, we're using 30 days for simplicity.
            // You might want to implement more complex logic to account for shorter months.
            return new RepetitionPattern(TimeSpan.FromDays(30), TimeSpan.FromDays(30));
        }

        private bool IsNumeric(string value)
        {
            return int.TryParse(value, out _);
        }
    }
}
