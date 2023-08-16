using Microsoft.Win32.TaskScheduler;
using System;

namespace ScheduledTaskAPI.Cron.Strategies
{
    public class EveryWeekDaysStrategy : BaseCronStrategy
    {
        public override bool CanHandle(string[] cronParts)
        {
            return cronParts[0] == "0" && cronParts[2] == "*" && cronParts[3] == "*" && cronParts[4].Contains("-");
        }

        protected override RepetitionPattern GetRepetitionPattern(string[] cronParts)
        {
            return new RepetitionPattern(TimeSpan.FromDays(1), TimeSpan.FromDays(1));
        }
    }
}
