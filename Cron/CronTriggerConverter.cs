using System;
using Microsoft.Win32.TaskScheduler;

namespace ScheduledTaskAPI.Cron
{
    public class CronTriggerConverter
    {
        private readonly List<ICronStrategy> _strategies;

        public CronTriggerConverter()
        {
            _strategies = new List<ICronStrategy>
            {
                new Strategies.HourlyStrategy(),
                new Strategies.EveryXHoursStrategy(),
                // new Strategies.DailyCronStrategy(),
                // new Strategies.WeeklyCronStrategy(),
                //...
            };
        }

        public Trigger Convert(string cron, DateTime? start = null, DateTime? end = null)
        {
            var parts = cron.Split(' ');

            foreach (var strategy in _strategies)
            {
                if (strategy.CanHandle(parts))
                {
                    return strategy.ToTrigger(parts, start, end);
                }
            }

            throw new ArgumentException($"Unsupported cron pattern: {cron}");
        }
    }
}
